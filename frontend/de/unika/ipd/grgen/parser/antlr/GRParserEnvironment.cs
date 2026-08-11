/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.parser.antlr
{

	using System;
	using System.Collections.Generic;

	using org.antlr.runtime;

	using Sys = de.unika.ipd.grgen.Sys;
	using de.unika.ipd.grgen.ast;
	using ModelNode = de.unika.ipd.grgen.ast.model.decl.ModelNode;
	using ParserEnvironment = de.unika.ipd.grgen.parser.ParserEnvironment;

	/// <summary>
	/// Ease the ANTLR parser calling
	/// </summary>
	public class GRParserEnvironment : ParserEnvironment
	{
		private bool hadError_ = false;
		private Stack<SubunitInclude> includes = new Stack<SubunitInclude>();
		private HashSet<string> filesOnStack = new HashSet<string>();
		private HashSet<string> modelsOnStack = new HashSet<string>();
		private Dictionary<string, ModelNode> models = new Dictionary<string, ModelNode>();

		/// <summary>
		/// The base directory of the specification or null for the current directory </summary>
		private File baseDir = null;

		private string filename;

		public GRParserEnvironment(Sys sys)
			: base(sys)
		{
		}

		public override void PushFile(Lexer lexer, File file)
		{
			if(baseDir != null && !file.IsAbsolute())
				file = new File(baseDir, file.GetPath());

			string filePath = file.GetPath();
			if(filesOnStack.Contains(filePath))
			{
				Console.Error.WriteLine("GrGen: [ERROR at " + Filename + ":" + lexer.GetLine()
						+ "," + lexer.GetCharPositionInLine() + "] found circular include with file \""
						+ filePath + "\"");
				Environment.Exit(1);
			}
			filesOnStack.Add(filePath);

			try
			{
				// save current lexer's state
				CharStream input = lexer.GetCharStream();
				int marker = input.Mark();
				includes.Push(new SubunitInclude(input, marker));

				// switch on new input stream
				ANTLRFileStream stream = new ANTLRFileStream(file.GetPath());
				lexer.SetCharStream(stream);
				lexer.Reset();
				filename = file.GetPath();
			}
			catch(IOException)
			{
				Console.Error.WriteLine("GrGen: [ERROR at " + Filename + ":" + lexer.GetLine()
						+ "," + lexer.GetCharPositionInLine() + "] included file could not be found: \""
						+ filePath + "\"");
				Environment.Exit(1);
			}
		}

		public override bool PopFile(Lexer lexer)
		{
			// We've got EOF on an include (not a model using or the initial parser).
			if(includes.Count > 1 && includes.Peek().charStream != null)
			{
				filesOnStack.Remove(lexer.GetSourceName());

				SubunitInclude include = includes.Pop();
				lexer.SetCharStream(include.charStream);
				lexer.GetCharStream().Rewind(include.marking);
				filename = lexer.GetCharStream().GetSourceName();
				return true;
			}

			return false;
		}

		public override string Filename
		{
			get
			{
				return filename;
			}
		}

		public override UnitNode ParseActions(File inputFile)
		{
			UnitNode root = null;

			baseDir = inputFile.GetParentFile();

			try
			{
				ANTLRFileStream stream = new ANTLRFileStream(inputFile.GetPath());
				GrGenLexer lexer = new GrGenLexer(stream);
				lexer.SetEnv(this);
				CommonTokenStream tokenStream = new CommonTokenStream(lexer);
				GrGenParser parser = new GrGenParser(tokenStream);
				includes.Push(new SubunitInclude(parser));
				filename = inputFile.GetPath();

				try
				{
					parser.SetEnv(this);
					root = parser.TextActions();
					hadError_ = hadError_ || parser.HadError();
				}
				catch(RecognitionException e)
				{
					e.PrintStackTrace(System.err);
					Console.Error.WriteLine("parser exception: " + e.Message);
					Environment.Exit(1);
				}

				includes.Pop();
			}
			catch(IOException e)
			{
				Console.Error.WriteLine("input file not found: " + e.Message);
				Environment.Exit(1);
			}

			return root;
		}

		public override ModelNode ParseModel(File inputFile)
		{
			ModelNode root = null;

			string filePath = inputFile.GetAbsolutePath();
			if(modelsOnStack.Contains(filePath))
			{
				Console.Error.WriteLine("GrGen: [ERROR at " + Filename + /*":" + curlexer.getLine()
						+ "," + curlexer.getCharPositionInLine() +*/ "] found circular model usage with file \""
						+ filePath + "\"");
				Environment.Exit(1);
			}

			root = models[filePath];
			if(root != null)
				return root;

			modelsOnStack.Add(filePath);

			try
			{
				ANTLRFileStream stream = new ANTLRFileStream(inputFile.GetPath());
				GrGenLexer lexer = new GrGenLexer(stream);
				lexer.SetEnv(this);
				CommonTokenStream tokenStream = new CommonTokenStream(lexer);
				GrGenParser parser = new GrGenParser(tokenStream);
				includes.Push(new SubunitInclude(parser));
				string oldFilename = filename;
				filename = inputFile.GetPath();

				try
				{
					parser.SetEnv(this);
					root = parser.TextTypes();
					hadError_ = hadError_ || parser.HadError();
				}
				catch(RecognitionException e)
				{
					e.PrintStackTrace(System.err);
					Console.Error.WriteLine("parser exception: " + e.Message);
					Environment.Exit(1);
				}

				filename = oldFilename;

				includes.Pop();
			}
			catch(IOException e)
			{
				Console.Error.WriteLine("cannot load graph model: " + e.Message);
				Environment.Exit(1);
			}

			modelsOnStack.Remove(filePath);

			models[filePath] = root;

			return root;
		}

		public override bool HadError()
		{
			return hadError_;
		}
	}

}
