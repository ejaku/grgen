/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.Rectangle;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.OutputStream;
import java.io.PrintStream;
import java.util.Collection;
import java.util.LinkedList;
import java.util.prefs.Preferences;

import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JComponent;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.JTree;

import jargs.gnu.CmdLineParser;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.ModelNode;
import de.unika.ipd.grgen.ast.UnitNode;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendFactory;
import de.unika.ipd.grgen.ir.Dumper;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.parser.antlr.GRParserEnvironment;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.GraphDumpVisitor;
import de.unika.ipd.grgen.util.GraphDumperFactory;
import de.unika.ipd.grgen.util.NullOutputStream;
import de.unika.ipd.grgen.util.PostWalker;
import de.unika.ipd.grgen.util.PrePostWalker;
import de.unika.ipd.grgen.util.VCGDumper;
import de.unika.ipd.grgen.util.VCGDumperFactory;
import de.unika.ipd.grgen.util.Walkable;
import de.unika.ipd.grgen.util.XMLDumper;
import de.unika.ipd.grgen.util.report.DebugReporter;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.grgen.util.report.Handler;
import de.unika.ipd.grgen.util.report.NullReporter;
import de.unika.ipd.grgen.util.report.Reporter;
import de.unika.ipd.grgen.util.report.StreamHandler;
import de.unika.ipd.grgen.util.report.TableHandler;


/**
 * Main.java
 *
 *
 * Created: Wed Jul  2 11:22:43 2003
 *
 * @author Sebastian Hack
 * @version 1.0
 */
public class Main extends Base implements Sys {

	private String[] args;
	private String[] inputFileNames;
	private UnitNode root;
	private Unit irUnit;
	private ErrorReporter errorReporter;
	private Reporter debugReporter;
	private Handler debugHandler;

	private boolean noEvents;

	private boolean enableDebug;

	/** enable ast printing */
	private boolean dumpAST;

	/** enable ir dumping */
	private boolean dumpIR;

	/** enable seperate rule dumping. */
	private boolean dumpRules;

	/** Print timing information. */
	private boolean printTiming;

	/** debug filter regular expression */
	private String debugFilter;

	/** inverse debug filter regular expression */
	private String invDebugFilter;

	/** dump System.err and System.out to a file. */
	private String dumpOutputToFile;

	/** Backend to use. */
	private String backend;

	/** support graphic output (meaning a 2d UI) */
	private boolean graphic;

	/** Debug tree view for graphic output */
	private JPanel debugPanel;

	/** Debug JTree for graphic output */
	private JTree debugTree;

	/** The preferences for the grgen program */
	private Preferences prefs;

	/** Export filename for preferences (null, if export is not wanted). */
	private String prefsExport;

	/** Import filename for preferences (null, if import is not wanted). */
	private String prefsImport;

	/** Output path. */
	private File outputPath = new File(".");

	/** The path to the source files. */
	private File sourcePath;

	private File debugPath;

	/** A list of files containing paths where the graph model can be searched. */
	private Collection<File> modelPaths = new LinkedList<File>();

	public File[] getModelPaths() {
		return modelPaths.toArray(new File[modelPaths.size()]);
	}

	public ErrorReporter getErrorReporter() {
		return errorReporter;
	}

	private void printUsage() {
		System.out.println("usage: grgen [options] filenames");
		System.out.println("filenames may consist of one .grg and multiple .gm files");
		System.out.println("Options are:");
		//System.out.println("  -n, --new-technology              enable immature features");
		System.out.println("  -t, --timing                      print some timing stats");
		System.out.println("  -d, --debug                       enable debugging");
		System.out.println("  -a, --dump-ast                    dump the AST");
		System.out.println("  -i, --dump-ir                     dump the intermidiate representation");
		System.out.println("  -j, --dump-ir-rules               dump each ir rule in a seperate file");
		System.out.println("  -g, --graphic                     opens a graphical debug window");
		System.out.println("  -b, --backend=BE                  select backend BE");
		System.out.println("  -f, --debug-filter=REGEX          only debug messages matching this filter will be displayd");
		System.out.println("  -F, --inverse-debug-filter=REGEX  only debug messages not matching this filter will be displayd");
		System.out.println("  -p, --prefs=FILE                  import preferences from FILE");
		System.out.println("  -x, --prefs-export=FILE           export preferences to FILE");
		System.out.println("  -o, --output=DIRECTORY            write generated files to DIRECTORY");
		System.out.println("  -e, --noevents                    the generated code may not fire any events");
	}

	// TODO use or remove it
	/*private JPanel getTreePanel(TreeHandler treeHandler) {
	 debugTree = new JTree(treeHandler);
	 debugTree.setEditable(false);

	 JPanel panel = new JPanel();

	 JScrollPane scrollPane = new JScrollPane(debugTree);
	 panel.setLayout(new BorderLayout());
	 panel.setPreferredSize(new Dimension(800, 600));
	 panel.add(scrollPane, BorderLayout.CENTER);

	 return panel;
	 }*/

	private JPanel getTablePanel(TableHandler tableHandler) {
		JComponent table = new JTable(tableHandler);
		JPanel panel = new JPanel();

		JScrollPane scrollPane = new JScrollPane(table);
		panel.setLayout(new BorderLayout());
		panel.setPreferredSize(new Dimension(800, 600));
		panel.add(scrollPane, BorderLayout.CENTER);

		return panel;
	}


	// TODO use or remove it
	/*private void editPreferences() {

	 }*/

	private JFrame makeMainFrame() {
		JPanel panel = new JPanel();
		panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));
		if (debugPanel != null)
			panel.add(debugPanel);
		//panel.add(Box.createRigidArea(new Dimension(0, 20)));

		JPanel buttonPanel = new JPanel();
		buttonPanel.setLayout(new BoxLayout(buttonPanel, BoxLayout.X_AXIS));
		buttonPanel.setBorder(BorderFactory.createEmptyBorder(10,0,10,0));

		JButton expandButton = new JButton("Expand All");
		expandButton.addActionListener(new ActionListener() {
					public void actionPerformed(ActionEvent e) {
						for(int i = 0; i < debugTree.getRowCount(); i++)
							debugTree.expandRow(i);
					}
				});


		JButton exitButton = new JButton("Exit");
		exitButton.addActionListener(new ActionListener() {
					public void actionPerformed(ActionEvent e) {
						System.exit(0);
					}
				});

		buttonPanel.add(expandButton);
		buttonPanel.add(exitButton);

		panel.add(buttonPanel);

		JFrame frame = new JFrame("GrGen");
		frame.setContentPane(panel);

		frame.addWindowListener(new WindowAdapter() {
					public void windowClosing(WindowEvent e) {
						System.exit(0);
					}
				});

		frame.pack();

		Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
		Rectangle bounds = frame.getBounds();
		frame.setLocation((dim.width - bounds.width) / 2,
							  (dim.height - bounds.height) / 2);

		frame.setVisible(true);

		return frame;
	}

	private void init() {

		prefs = Preferences.userNodeForPackage(getClass());

		// Debugging has an empty reporter if the flag is not set
		if(enableDebug) {
			if(graphic) {
				debugHandler = new TableHandler();
				debugPanel = getTablePanel((TableHandler) debugHandler);
			} else {
				debugHandler = new StreamHandler(System.out);
			}

			DebugReporter dr = new DebugReporter(15);
			dr.addHandler(debugHandler);
			if(debugFilter != null)
				dr.setFilter(debugFilter);

			if(invDebugFilter != null) {
				dr.setFilter(invDebugFilter);
				dr.setFilterInclusive(false);
			}
			debugReporter = dr;
		}
		else
			debugReporter = new NullReporter();


		// Main error reporter
		errorReporter = new ErrorReporter();
		errorReporter.addHandler(new StreamHandler(System.err));

		Base.setReporters(debugReporter, errorReporter);
	}

	private void parseOptions() {
		try {
			CmdLineParser parser = new CmdLineParser();
			CmdLineParser.Option debugOpt = parser.addBooleanOption('d', "debug");
			CmdLineParser.Option astDumpOpt = parser.addBooleanOption('a', "dump-ast");
			CmdLineParser.Option irDumpOpt = parser.addBooleanOption('i', "dump-ir");
			CmdLineParser.Option ruleDumpOpt = parser.addBooleanOption('j', "dump-ir-rules");
			CmdLineParser.Option graphicOpt = parser.addBooleanOption('g', "graphic");
			CmdLineParser.Option timeOpt = parser.addBooleanOption('t', "timing");
			CmdLineParser.Option noEventsOpt = parser.addBooleanOption('e', "noevents");

			CmdLineParser.Option dumpOutputToFileOpt =
				parser.addStringOption('c', "dump-output-to-file");
			CmdLineParser.Option beOpt =
				parser.addStringOption('b', "backend");
			CmdLineParser.Option debugFilterOpt =
				parser.addStringOption('f', "debug-filter");
			CmdLineParser.Option invDebugFilterOpt =
				parser.addStringOption('F', "inverse-debug-filter");
			CmdLineParser.Option prefsImportOpt =
				parser.addStringOption('p', "prefs");
			CmdLineParser.Option prefsExportOpt =
				parser.addStringOption('x', "prefs-export");
			CmdLineParser.Option optOutputPath =
				parser.addStringOption('o', "output");

			parser.parse(args);

			dumpOutputToFile = (String) parser.getOptionValue(dumpOutputToFileOpt);
			if(dumpOutputToFile!=null) {
				try {
					PrintStream dumpOutputStream = new PrintStream(new FileOutputStream(dumpOutputToFile));
					System.setErr(dumpOutputStream);
					System.setOut(dumpOutputStream);
				} catch (FileNotFoundException e) {
					e.printStackTrace();
				}
			}

			dumpAST = parser.getOptionValue(astDumpOpt) != null;
			dumpIR = parser.getOptionValue(irDumpOpt) != null;
			dumpRules = parser.getOptionValue(ruleDumpOpt) != null;
			enableDebug = parser.getOptionValue(debugOpt) != null;
			graphic = parser.getOptionValue(graphicOpt) != null;
			printTiming = parser.getOptionValue(timeOpt) != null;
			noEvents = parser.getOptionValue(noEventsOpt) != null;

			/* deactivate graphic if no debug output */
			if (!enableDebug)
				graphic = false;

			debugFilter = (String) parser.getOptionValue(debugFilterOpt);
			invDebugFilter = (String) parser.getOptionValue(invDebugFilterOpt);
			backend = (String) parser.getOptionValue(beOpt);
			String s = (String) parser.getOptionValue(optOutputPath);
			outputPath = new File(s != null ? s: System.getProperty("user.dir"));

			prefsImport = (String) parser.getOptionValue(prefsImportOpt);
			prefsExport = (String) parser.getOptionValue(prefsExportOpt);

			inputFileNames = parser.getRemainingArgs();
			if(inputFileNames.length == 0) {
				printUsage();
				System.exit(2);
			}
		}
		catch(CmdLineParser.OptionException e) {
			System.err.println(e.getMessage());
			printUsage();
			System.exit(2);
		}
	}

	public boolean mayFireEvents() {
		return !noEvents;
	}

	public OutputStream createDebugFile(File file) {
		debugPath.mkdirs();
		File debFile = new File(debugPath, file.getName());
		try {
			return new BufferedOutputStream(new FileOutputStream(debFile));
		} catch (FileNotFoundException e) {
			errorReporter.error("cannot open debug file " + debFile.getPath());
			return NullOutputStream.STREAM;
		}
	}

	private boolean parseInput() {
		boolean res = false;
		boolean setDebugPath = true;	// use the first processed filename for the debug path

		GRParserEnvironment env = new GRParserEnvironment(this);

		// First process the .grg file, if one was specified
		for(String inputFileName : inputFileNames)
		{
			File inputFile = new File(inputFileName);
			String ext = getFileExt(inputFileName);
			if(ext.equals("grg")) {
				if(root != null) {
					error.error("Only one .grg file may be specified!");
					System.exit(-1);
				}
				initPaths(inputFileName, inputFile, setDebugPath);
				setDebugPath = false;

				root = env.parseActions(inputFile);
			}
			else if(!ext.equals("gm")) {
				error.error("Input file with unknown extension: '" + ext + "'");
				System.exit(-1);
			}
		}

		// No .grg file given?
		if(root == null) {
			root = new UnitNode("NoGRGFileGiven", inputFileNames[0], env.getStdModel(),
					new CollectNode<ModelNode>(), new CollectNode<IdentNode>(),
					new CollectNode<IdentNode>());
		}

		// Now all .gm files
		for(String inputFileName : inputFileNames)
		{
			File inputFile = new File(inputFileName);
			if(getFileExt(inputFileName).equals("gm")) {
				initPaths(inputFileName, inputFile, setDebugPath);
				setDebugPath = false;

				ModelNode model = env.parseModel(inputFile);
				root.addModel(model);
			}
		}
		res = !env.hadError();

		// Close main scope and fixup definitions
		env.getCurrScope().leaveScope();

		debug.report(NOTE, "result: " + res);
		return res;
	}

	private String getFileExt(String filename) {
		int lastDot = filename.lastIndexOf('.');
		int lastDirSep = filename.lastIndexOf(File.separatorChar);
		if(lastDot == -1 || lastDirSep != -1 && lastDot < lastDirSep) {
			error.error("The input file \"" + filename + "\" has no extension!");
			System.exit(-1);
		}
		return filename.substring(lastDot + 1).toLowerCase();
	}

	private void initPaths(String inputFileName, File inputFile, boolean setDebugPath) {
		if(inputFileName.indexOf('/') != -1 || inputFileName.indexOf('\\') != -1)
			sourcePath = inputFile.getAbsoluteFile().getParentFile();
		else
			sourcePath = new File(".");
		if(setDebugPath)
			debugPath = new File(sourcePath, inputFile.getName() + "_debug");
		modelPaths.clear();
		modelPaths.add(sourcePath);
	}

	private void dumpVCG(Walkable node, GraphDumpVisitor visitor,
						 String suffix) {

		File file = new File(suffix + ".vcg");
		OutputStream os = createDebugFile(file);

		VCGDumper vcg = new VCGDumper(new PrintStream(os));
		visitor.setDumper(vcg);
		PrePostWalker walker = new PostWalker(visitor);
		vcg.begin();
		walker.reset();
		walker.walk(node);
		vcg.finish();
	}


	private void buildIR() {
		irUnit = root.getUnit();
	}

	private void generateCode() {
		assert backend != null : "backend must be set to generate code.";

		try {
			BackendFactory creator =
				(BackendFactory) Class.forName(backend).newInstance();
			Backend be = creator.getBackend();

			be.init(irUnit, this, outputPath);
			be.generate();
			be.done();
		} catch(ClassNotFoundException e) {
			System.err.println("cannot locate backend class: " + backend);
			System.exit(-1);
		} catch(IllegalAccessException e) {
			System.err.println("no rights to create backend class: " + backend);
			System.exit(-1);
		} catch(InstantiationException e) {
			System.err.println("cannot create backend class: " + backend);
			System.exit(-1);
		} catch(Throwable e) {
			System.err.println("unexpected exception occurred:");
			e.printStackTrace();
			System.exit(-1);
		}


		if (ErrorReporter.getErrorCount() > 0) {
			if (ErrorReporter.getErrorCount() == 1)
				System.err.println("There was " + ErrorReporter.getErrorCount() + " error");
			else
				System.err.println("There were " + ErrorReporter.getErrorCount() + " errors");

			System.exit(-1);
		}
		else if (ErrorReporter.getWarnCount() > 0){
			if (ErrorReporter.getWarnCount() == 1)
				System.err.println("There was " + ErrorReporter.getWarnCount() + " warning");
			else
				System.err.println("There were " + ErrorReporter.getWarnCount() + " warnings");
		}
	}

	/**
	 * This is the main driver routine.
	 * It pareses the input file, constructs the AST,
	 * checks it, constructs the immediate representation and
	 * emits the code.
	 */
	private void run() {
		long startUp, parse, manifest, buildIR, codeGen;

		startUp = -System.currentTimeMillis();

		parseOptions();
		init();

		importPrefs();

		// Open graphic debug window if desired.
		if(graphic)
			makeMainFrame();

		debug.report(NOTE, "working directory: " + System.getProperty("user.dir"));

		startUp += System.currentTimeMillis();
		parse = -System.currentTimeMillis();

		debug.report(NOTE, "### Parse Input ###");
		// parse the input file and exit, if there were errors
		if(!parseInput()) {
			debug.report(NOTE, "### ERROR in Parse Input. Exiting! ###");
			System.exit(1);
		}

		parse += System.currentTimeMillis();
		manifest = -System.currentTimeMillis();

		debug.report(NOTE, "### Manifest AST ###");
		if(!BaseNode.manifestAST(root)) {
			if(dumpAST)
				dumpVCG(root, new GraphDumpVisitor(), "error-ast");
			debug.report(NOTE, "### ERROR in Manifest AST. Exiting! ###");
			if(ErrorReporter.getErrorCount() == 0)
				error.error("Unknown error occurred in \"Manifest AST\"!");
			System.exit(1);
		}

		manifest += System.currentTimeMillis();

		// Dump the rewritten AST.
		if(dumpAST)
			dumpVCG(root, new GraphDumpVisitor(), "ast");

		/*
		 // Do identifier resolution (Rewrites the AST)
		 if(!BaseNode.resolveAST(root))
		 System.exit(2);

		 // Dump the rewritten AST.
		 if(dumpAST)
		 dumpVCG(root, new GraphDumpVisitor(), "ast");

		 // Check the AST for consistency.
		 if(!BaseNode.checkAST(root))
		 System.exit(1);
		 */

		debug.report(NOTE, "### Build IR ###");
		// Construct the Intermediate representation.
		buildIR = -System.currentTimeMillis();
		buildIR();
		root = null; // throw away AST not needed any more -> reduce memory requirements
		irUnit.checkForEmptyPatternsInIterateds();
		irUnit.checkForEmptySubpatternRecursions();
		irUnit.checkForNeverSucceedingSubpatternRecursions();
		irUnit.checkForMultipleRetypes();
		irUnit.checkForMultipleDeletesOrRetypes();
		irUnit.transmitExecUsageToRules();
		irUnit.setDependencyLevelByStorageMapAccess();
		irUnit.resolvePatternLockedModifier();
		irUnit.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern();
		irUnit.checkForRhsElementsUsedOnLhs();
		buildIR += System.currentTimeMillis();

		GraphDumperFactory factory = new VCGDumperFactory(this);
		Dumper dumper = new Dumper(factory, true);

		// Dump the IR.
		if(dumpIR) {
			dumper.dumpComplete(irUnit, "ir");

			if(dumpRules)
				dumper.dump(irUnit);


			OutputStream os = createDebugFile(new File("ir.xml"));
			PrintStream ps = new PrintStream(os);
			XMLDumper xmlDumper = new XMLDumper(ps);
			xmlDumper.dump(irUnit);
			ps.flush();
			ps.close();
		}

		if(graphic && debugTree != null) {
			debugTree.expandRow(0);
			debugTree.expandRow(1);
		}

		if(ErrorReporter.getErrorCount()>0) {
			debug.report(NOTE, "### ERROR during IR build. Exiting! ###");
			System.exit(1);
		}


		debug.report(NOTE, "### Generate Code ###");
		codeGen = -System.currentTimeMillis();
		if(backend != null)
			generateCode();
		codeGen += System.currentTimeMillis();

		debug.report(NOTE, "### done. ###");

		if(printTiming) {
			System.out.println("timing information (millis):");
			System.out.println("start up: " + startUp);
			System.out.println("parse:    " + parse);
			System.out.println("manifest: " + manifest);
			System.out.println("build IR: " + buildIR);
			System.out.println("code gen: " + codeGen);
		}

		exportPrefs();
	}

	/**
	 * Export the preferences.
	 */
	private void exportPrefs() {
		if(prefsExport != null) {
			try {
				FileOutputStream fos = new FileOutputStream(prefsExport);
				prefs.exportSubtree(fos);
			} catch (Exception e) {
				System.err.println(e.getMessage());
			}
		}
	}

	/**
	 * Import the preferences.
	 */
	private void importPrefs() {
		if(prefsImport != null) {
			try {
				FileInputStream fis = new FileInputStream(prefsImport);
				Preferences.importPreferences(fis);
			} catch(Exception e) {
				System.err.println(e.getMessage());
			}
		}
	}

	private Main(String[] args) {
		this.args = args;
	}

	private static void staticInit() {
		String packageName = Main.class.getPackage().getName();

		// Please use my preferences implementation.
		System.setProperty("java.util.prefs.PreferencesFactory",
						   packageName + ".util.MyPreferencesFactory");

	}

	public static void main(String[] args) {
		staticInit();
		Main main = new Main(args);
		main.run();
	}
}

