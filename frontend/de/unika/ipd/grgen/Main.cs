/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>
namespace de.unika.ipd.grgen
{

	using System;
	using System.Diagnostics;
	using System.IO;

	using CmdLineParser = com.sanityinc.jargs.CmdLineParser;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using UnitNode = de.unika.ipd.grgen.ast.UnitNode;
	using ModelNode = de.unika.ipd.grgen.ast.model.decl.ModelNode;
	using Backend = de.unika.ipd.grgen.be.Backend;
	using BackendFactory = de.unika.ipd.grgen.be.BackendFactory;
	using Dumper = de.unika.ipd.grgen.ir.Dumper;
	using Unit = de.unika.ipd.grgen.ir.Unit;
	using GRParserEnvironment = de.unika.ipd.grgen.parser.antlr.GRParserEnvironment;
	using Base = de.unika.ipd.grgen.util.Base;
	using GraphDumpVisitor = de.unika.ipd.grgen.util.GraphDumpVisitor;
	using GraphDumperFactory = de.unika.ipd.grgen.util.GraphDumperFactory;
	using PostWalker = de.unika.ipd.grgen.util.PostWalker;
	using PrePostWalker = de.unika.ipd.grgen.util.PrePostWalker;
	using VCGDumper = de.unika.ipd.grgen.util.VCGDumper;
	using VCGDumperFactory = de.unika.ipd.grgen.util.VCGDumperFactory;
	using Walkable = de.unika.ipd.grgen.util.Walkable;
	using XMLDumper = de.unika.ipd.grgen.util.XMLDumper;
	using DebugReporter = de.unika.ipd.grgen.util.report.DebugReporter;
	using ErrorReporter = de.unika.ipd.grgen.util.report.ErrorReporter;
	using Handler = de.unika.ipd.grgen.util.report.Handler;
	using NullReporter = de.unika.ipd.grgen.util.report.NullReporter;
	using Reporter = de.unika.ipd.grgen.util.report.Reporter;
	using StreamHandler = de.unika.ipd.grgen.util.report.StreamHandler;

	/// <summary>
	/// Main.java
	/// Created: Wed Jul  2 11:22:43 2003
	/// </summary>
	public class Main : Base, Sys
	{
		private string[] args;
		private string[] inputFileNames;
		private UnitNode root;
		private Unit irUnit;
		private ErrorReporter errorReporter;
		private Reporter debugReporter;
		private Handler debugHandler;

		private bool noEvents;
		private bool noDebugEvents;

		private bool enableDebug;

		private bool emitProfiling;

		/// <summary>
		/// enable ast printing </summary>
		private bool dumpAST;

		/// <summary>
		/// enable ir dumping </summary>
		private bool dumpIR;

		/// <summary>
		/// enable seperate rule dumping. </summary>
		private bool dumpRules;

		/// <summary>
		/// Print timing information. </summary>
		private bool printTiming;

		/// <summary>
		/// debug filter regular expression </summary>
		private string debugFilter;

		/// <summary>
		/// inverse debug filter regular expression </summary>
		private string invDebugFilter;

		/// <summary>
		/// dump System.err and System.out to a file. </summary>
		private string dumpOutputToFile;

		/// <summary>
		/// Backend to use. </summary>
		private string backend;

		/// <summary>
		/// Output path. </summary>
		private DirectoryInfo outputPath = new DirectoryInfo(".");

		/// <summary>
		/// The path to the source files. </summary>
		private DirectoryInfo sourcePath;

		private DirectoryInfo debugPath;

		/// <summary>
		/// A file containing a path where the graph model can be searched. </summary>
		private DirectoryInfo modelPath = null;

		public virtual DirectoryInfo ModelPath
		{
			get
			{
				return modelPath;
			}
		}

		public virtual ErrorReporter ErrorReporter
		{
			get
			{
				return errorReporter;
			}
		}

		private static void PrintUsage()
		{
			Console.WriteLine("usage: grgen [options] filenames");
			Console.WriteLine("filenames may consist of one .grg and multiple .gm files");
			Console.WriteLine("Options are:");
			//System.out.println("  -n, --new-technology              enable immature features");
			Console.WriteLine("  -t, --timing                      print some timing stats");
			Console.WriteLine("  -d, --debug                       enable debugging");
			Console.WriteLine("  -r  --profile                     emit profiling instrumentation");
			Console.WriteLine("  -a, --dump-ast                    dump the AST");
			Console.WriteLine("  -i, --dump-ir                     dump the intermidiate representation");
			Console.WriteLine("  -j, --dump-ir-rules               dump each ir rule in a seperate file");
			Console.WriteLine("  -b, --backend=BE                  select backend BE");
			Console.WriteLine("  -f, --debug-filter=REGEX          only debug messages matching this filter will be displayd");
			Console.WriteLine("  -F, --inverse-debug-filter=REGEX  only debug messages not matching this filter will be displayd");
			Console.WriteLine("  -o, --output=DIRECTORY            write generated files to DIRECTORY");
			Console.WriteLine("  -v, --noactionevents              the generated code may not fire action events");
			Console.WriteLine("  -e, --noattributeevents           the generated code may not fire attribute change events");
		}

		protected internal virtual void SystemExit(int status)
		{
			Environment.Exit(status);
		}

		private void Init()
		{
			// Debugging has an empty reporter if the flag is not set
			if(enableDebug)
			{
				debugHandler = new StreamHandler(new PrintStream(Console.Out));

				DebugReporter dr = new DebugReporter(15);
				dr.AddHandler(debugHandler);
				if(!string.ReferenceEquals(debugFilter, null))
					dr.Filter = debugFilter;

				if(!string.ReferenceEquals(invDebugFilter, null))
				{
					dr.Filter = invDebugFilter;
					dr.FilterInclusive = false;
				}
				debugReporter = dr;
			}
			else
				debugReporter = new NullReporter();

			// Main error reporter
			errorReporter = new ErrorReporter();
			errorReporter.AddHandler(new StreamHandler(new PrintStream(Console.Error)));

			Base.SetReporters(debugReporter, errorReporter);
		}

		private void ParseOptions()
		{
			try
			{
				CmdLineParser parser = new CmdLineParser();
				CmdLineParser.Option<bool> debugOpt = parser.AddBooleanOption('d', "debug");
				CmdLineParser.Option<bool> profOpt = parser.AddBooleanOption('r', "profile");
				CmdLineParser.Option<bool> astDumpOpt = parser.AddBooleanOption('a', "dump-ast");
				CmdLineParser.Option<bool> irDumpOpt = parser.AddBooleanOption('i', "dump-ir");
				CmdLineParser.Option<bool> ruleDumpOpt = parser.AddBooleanOption('j', "dump-ir-rules");
				CmdLineParser.Option<bool> timeOpt = parser.AddBooleanOption('t', "timing");
				CmdLineParser.Option<bool> noEventsOpt = parser.AddBooleanOption('e', "noevents");
				CmdLineParser.Option<bool> noDebugEventsOpt = parser.AddBooleanOption('v', "nodebugevents");

				CmdLineParser.Option<string> dumpOutputToFileOpt = parser.AddStringOption('c', "dump-output-to-file");
				CmdLineParser.Option<string> beOpt = parser.AddStringOption('b', "backend");
				CmdLineParser.Option<string> debugFilterOpt = parser.AddStringOption('f', "debug-filter");
				CmdLineParser.Option<string> invDebugFilterOpt = parser.AddStringOption('F', "inverse-debug-filter");
				CmdLineParser.Option<string> optOutputPath = parser.AddStringOption('o', "output");

				parser.Parse(args);

				dumpOutputToFile = (string)parser.GetOptionValue(dumpOutputToFileOpt);
				if(!string.ReferenceEquals(dumpOutputToFile, null))
				{
					try
					{
						StreamWriter dumpOutputStream = new StreamWriter(new FileStream(dumpOutputToFile, FileMode.Create, FileAccess.Write));
						Console.SetError(dumpOutputStream);
						Console.SetOut(dumpOutputStream);
					}
					catch(FileNotFoundException e)
					{
						Console.WriteLine(e.ToString());
						Console.Write(e.StackTrace);
					}
				}

				dumpAST = parser.GetOptionValue(astDumpOpt) != null;
				dumpIR = parser.GetOptionValue(irDumpOpt) != null;
				dumpRules = parser.GetOptionValue(ruleDumpOpt) != null;
				enableDebug = parser.GetOptionValue(debugOpt) != null;
				emitProfiling = parser.GetOptionValue(profOpt) != null;
				printTiming = parser.GetOptionValue(timeOpt) != null;
				noEvents = parser.GetOptionValue(noEventsOpt) != null;
				noDebugEvents = parser.GetOptionValue(noDebugEventsOpt) != null;

				debugFilter = (string)parser.GetOptionValue(debugFilterOpt);
				invDebugFilter = (string)parser.GetOptionValue(invDebugFilterOpt);
				backend = (string)parser.GetOptionValue(beOpt);
				string s = (string)parser.GetOptionValue(optOutputPath);
				outputPath = new DirectoryInfo(!string.ReferenceEquals(s, null) ? s : /*System.GetProperty("user.dir")*/".");

				inputFileNames = parser.RemainingArgs;
				if(inputFileNames.Length == 0)
				{
					PrintUsage();
					SystemExit(2);
				}
			}
			catch(CmdLineParser.OptionException e)
			{
				Console.Error.WriteLine(e.Message);
				PrintUsage();
				SystemExit(2);
			}
		}

		public virtual bool MayFireEvents()
		{
			return !noEvents;
		}

		public virtual bool MayFireDebugEvents()
		{
			return !noDebugEvents && !noEvents;
		}

		public virtual bool EmitProfilingInstrumentation()
		{
			return emitProfiling;
		}

		public virtual Stream CreateDebugFile(FileInfo file)
		{
			FileAndDirectoryHelper.Mkdirs(debugPath);
			FileInfo debFile = FileAndDirectoryHelper.GetFileInfo(debugPath, file.Name);
			try
			{
				return new FileStream(debFile.FullName, FileMode.Create, FileAccess.Write);
			}
			catch(FileNotFoundException)
			{
				errorReporter.Error("Cannot open debug file " + debFile.FullName + ".");
				return Stream.Null;
			}
		}

		private bool ParseInput()
		{
			bool res = false;
			bool setDebugPath = true; // use the first processed filename for the debug path

			GRParserEnvironment env = new GRParserEnvironment(this);

			// First process the .grg file, if one was specified
			foreach(string inputFileName in inputFileNames)
			{
				FileInfo inputFile = new FileInfo(inputFileName);
				string ext = GetFileExt(inputFileName);
				if(ext.Equals("grg"))
				{
					if(root != null)
					{
						error.Error("Only one .grg file may be specified.");
						SystemExit(-1);
					}
					InitPaths(inputFileName, inputFile, setDebugPath);
					setDebugPath = false;

					root = env.ParseActions(inputFile);
				}
				else if(!ext.Equals("gm"))
				{
					error.Error("Input file with unknown extension: \"" + ext + "\".");
					SystemExit(-1);
				}
			}

			// No .grg file given?
			if(root == null)
				root = new UnitNode("NoGRGFileGiven", inputFileNames[0],
						env.StdModel, new CollectNode<ModelNode>(),
						new CollectNode<IdentNode>(), new CollectNode<IdentNode>(),
						new CollectNode<IdentNode>(), new CollectNode<IdentNode>(),
						new CollectNode<IdentNode>(), new CollectNode<IdentNode>(),
						new CollectNode<IdentNode>(),
						new CollectNode<IdentNode>(), new CollectNode<IdentNode>(),
						new CollectNode<IdentNode>(), new CollectNode<IdentNode>());

			// Now all .gm files
			foreach(string inputFileName in inputFileNames)
			{
				FileInfo inputFile = new FileInfo(inputFileName);
				if(GetFileExt(inputFileName).Equals("gm"))
				{
					InitPaths(inputFileName, inputFile, setDebugPath);
					setDebugPath = false;

					ModelNode model = env.ParseModel(inputFile);
					root.AddModel(model);
				}
			}
			res = !env.HadError();

			// Close main scope and fixup definitions
			env.CurrScope.LeaveScope();

			debug.Report(NOTE, "result: " + res);
			return res;
		}

		private string GetFileExt(string filename)
		{
			int lastDot = filename.LastIndexOf('.');
			int lastDirSep = filename.LastIndexOf(Path.DirectorySeparatorChar);
			if(lastDot == -1 || lastDirSep != -1 && lastDot < lastDirSep)
			{
				error.Error("The input file \"" + filename + "\" is lacking the name extension.");
				SystemExit(-1);
			}
			return filename.Substring(lastDot + 1).ToLower();
		}

		private void InitPaths(string inputFileName, FileInfo inputFile, bool setDebugPath) // TODO: rename to InitAuxPaths
		{
			// assume: inputFile is created from the inputFileName
			if(inputFileName.IndexOf('/') != -1 || inputFileName.IndexOf('\\') != -1)
				sourcePath = inputFile.Directory;
			else
				sourcePath = new DirectoryInfo(".");
			if(setDebugPath)
				debugPath = FileAndDirectoryHelper.GetDirectoryInfo(sourcePath, inputFile.Name + "_debug");
			modelPath = sourcePath;
		}

		private void DumpVCG(Walkable node, GraphDumpVisitor visitor, string name)
		{
			FileInfo file = new FileInfo(name + ".vcg");
			try
			{
				using(Stream os = CreateDebugFile(file))
				{
					using(PrintStream ps = new PrintStream(os))
					{
						VCGDumper vcg = new VCGDumper(ps);
						visitor.Dumper = vcg;
						PrePostWalker walker = new PostWalker(visitor);
						vcg.Begin();
						walker.Reset();
						walker.Walk(node);
						vcg.Finish();
					}
				}
			}
			catch(IOException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
		}

		private void BuildIR()
		{
			irUnit = root.IRUnit;
		}

		private void GenerateCode()
		{
			Debug.Assert(!string.ReferenceEquals(backend, null), "backend must be set to generate code.");

			try
			{
				BackendFactory creator = (BackendFactory)System.Activator.CreateInstance(Type.GetType(backend));
				Backend be = creator.Backend;

				be.Init(irUnit, this, outputPath);
				be.Generate();
				be.Done();
			}
			catch(ClassNotFoundException)
			{
				Console.Error.WriteLine("cannot locate backend class: " + backend);
				SystemExit(-1);
			}
			catch(IllegalAccessException)
			{
				Console.Error.WriteLine("no rights to create backend class: " + backend);
				SystemExit(-1);
			}
			catch(InstantiationException)
			{
				Console.Error.WriteLine("cannot create backend class: " + backend);
				SystemExit(-1);
			}
			catch(Exception e)
			{
				Console.Error.WriteLine("unexpected exception occurred:");
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				SystemExit(-1);
			}

			if(ErrorReporter.ErrorCount > 0)
			{
				if(ErrorReporter.ErrorCount == 1)
					Console.Error.WriteLine("There was " + ErrorReporter.ErrorCount + " error");
				else
					Console.Error.WriteLine("There were " + ErrorReporter.ErrorCount + " errors");

				SystemExit(-1);
			}
			else if(ErrorReporter.WarnCount > 0)
			{
				if(ErrorReporter.WarnCount == 1)
					Console.Error.WriteLine("There was " + ErrorReporter.WarnCount + " warning");
				else
					Console.Error.WriteLine("There were " + ErrorReporter.WarnCount + " warnings");
			}
		}

		/// <summary>
		/// This is the main driver routine.
		/// It pareses the input file, constructs the AST,
		/// checks it, constructs the immediate representation and
		/// emits the code.
		/// </summary>
		protected internal virtual void Run()
		{
			long startUp, parse, manifest, buildIR, codeGen;

			startUp = -DateTimeHelper.CurrentUnixTimeMillis();

			ParseOptions();
			Init();

			debug.Report(NOTE, "working directory: " + /*System.GetProperty("user.dir")*/new DirectoryInfo(".").FullName);

			startUp += DateTimeHelper.CurrentUnixTimeMillis();
			parse = -DateTimeHelper.CurrentUnixTimeMillis();

			debug.Report(NOTE, "### Parse Input ###");
			// parse the input file and exit, if there were errors
			if(!ParseInput())
			{
				debug.Report(NOTE, "### ERROR in Parse Input. Exiting! ###");
				SystemExit(1);
			}

			parse += DateTimeHelper.CurrentUnixTimeMillis();
			manifest = -DateTimeHelper.CurrentUnixTimeMillis();

			debug.Report(NOTE, "### Manifest AST ###");
			if(!BaseNode.ManifestAST(root))
			{
				if(dumpAST)
					DumpVCG(root, new GraphDumpVisitor(), "error-ast");
				debug.Report(NOTE, "### ERROR in Manifest AST. Exiting! ###");
				if(ErrorReporter.ErrorCount == 0)
					error.Error("An unknown error occurred in \"Manifest AST\".");
				SystemExit(1);
			}

			manifest += DateTimeHelper.CurrentUnixTimeMillis();

			// Dump the rewritten AST.
			if(dumpAST)
				DumpVCG(root, new GraphDumpVisitor(), "ast");

			/*
			 // Do identifier resolution (Rewrites the AST)
			 if(!BaseNode.resolveAST(root))
			 systemExit(2);
		
			 // Dump the rewritten AST.
			 if(dumpAST)
			 dumpVCG(root, new GraphDumpVisitor(), "ast");
		
			 // Check the AST for consistency.
			 if(!BaseNode.checkAST(root))
			 systemExit(1);
			 */

			debug.Report(NOTE, "### Build IR ###");
			// Construct the Intermediate representation.
			buildIR = -DateTimeHelper.CurrentUnixTimeMillis();
			BuildIR();
			root = null; // throw away AST not needed any more -> reduce memory requirements
			irUnit.PostPatchIR();
			irUnit.CheckForEmptyPatternsInIterateds();
			irUnit.CheckForEmptySubpatternRecursions();
			irUnit.CheckForNeverSucceedingSubpatternRecursions();
			irUnit.CheckForMultipleRetypes();
			irUnit.CheckForMultipleDeletesOrRetypes();
			irUnit.TransmitExecUsageToRules();
			irUnit.SetDependencyLevelOfInterElementDependencies();
			irUnit.ResolvePatternLockedModifier();
			irUnit.EnsureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern();
			irUnit.CheckForRhsElementsUsedOnLhs();
			irUnit.CheckForParallelizedModelIfParallelizedActionExists();
			buildIR += DateTimeHelper.CurrentUnixTimeMillis();

			GraphDumperFactory factory = new VCGDumperFactory(this);
			Dumper dumper = new Dumper(factory, true);

			// Dump the IR.
			if(dumpIR)
			{
				dumper.DumpComplete(irUnit, "ir");

				if(dumpRules)
					dumper.Dump(irUnit);

				try
				{
					using(Stream os = CreateDebugFile(new FileInfo("ir.xml")))
					{
						using(PrintStream ps = new PrintStream(os))
						{
							XMLDumper xmlDumper = new XMLDumper(ps);
							xmlDumper.Dump(irUnit);
							ps.Flush();
						}
					}
				}
				catch(IOException e)
				{
					Console.WriteLine(e.ToString());
					Console.Write(e.StackTrace);
				}
			}

			if(ErrorReporter.ErrorCount > 0)
			{
				debug.Report(NOTE, "### ERROR during IR build. Exiting! ###");
				SystemExit(1);
			}

			debug.Report(NOTE, "### Generate Code ###");
			codeGen = -DateTimeHelper.CurrentUnixTimeMillis();
			if(!string.ReferenceEquals(backend, null))
				GenerateCode();
			codeGen += DateTimeHelper.CurrentUnixTimeMillis();

			debug.Report(NOTE, "### done. ###");

			if(printTiming)
			{
				Console.WriteLine("timing information (millis):");
				Console.WriteLine("start up: " + startUp);
				Console.WriteLine("parse:    " + parse);
				Console.WriteLine("manifest: " + manifest);
				Console.WriteLine("build IR: " + buildIR);
				Console.WriteLine("code gen: " + codeGen);
			}
		}

		protected internal Main(string[] args)
		{
			this.args = args;
		}

		protected internal static void StaticInit()
		{
			string packageName = typeof(Main).Assembly.GetName().Name;
			// used to initialize prefs/preferences, kept as a hook for now, TODO: remove
		}

		public static void Main(string[] args)
		{
			StaticInit();
			Main main = new Main(args);
			main.Run();
		}
	}

}
