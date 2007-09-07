/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen;

import de.unika.ipd.grgen.util.*;
import de.unika.ipd.grgen.util.report.*;
import java.io.*;
import javax.swing.*;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.UnitNode;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendException;
import de.unika.ipd.grgen.be.BackendFactory;
import de.unika.ipd.grgen.ir.Dumper;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.parser.antlr.GRParserEnvironment;
import jargs.gnu.CmdLineParser;
import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.Rectangle;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.util.Collection;
import java.util.LinkedList;
import java.util.prefs.Preferences;

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
	private File inputFile;
	private BaseNode root;
	private Unit irUnit;
	private ErrorReporter errorReporter;
	private Reporter debugReporter;
	private Handler debugHandler;
	
	/** backend emit debug files. */
	private boolean backendEmitDebugFiles;
	
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
		return (File[]) modelPaths.toArray(new File[modelPaths.size()]);
	}
	
	public ErrorReporter getErrorReporter() {
		return errorReporter;
	}
	
	private void printUsage() {
		System.out.println("usage: grgen [options] filename");
		System.out.println("Options are:");
		System.out.println("  -n, --new-technology              enable immature features");
		System.out.println("  -t, --timing                      print some timing stats");
		System.out.println("  -d, --debug                       enable debugging");
		System.out.println("  -a, --dump-ast                    dump the AST");
		System.out.println("  -i, --dump-ir                     dump the intermidiate representation");
		System.out.println("  -j, --dump-ir-rules               dump each ir rule in a seperate file");
		System.out.println("  -B, --backend-files               allow the backend to emit some debug files");
		System.out.println("  -g, --graphic                     opens a graphical debug window");
		System.out.println("  -b, --backend=BE                  select backend BE");
		System.out.println("  -f, --debug-filter=REGEX          only debug messages matching this filter will be displayd");
		System.out.println("  -F, --inverse-debug-filter=REGEX  only debug messages not matching this filter will be displayd");
		System.out.println("  -p, --prefs=FILE                  import preferences from FILE");
		System.out.println("  -x, --prefs-export=FILE           export preferences to FILE");
		System.out.println("  -o, --output=DIRECTORY            write generated files to DIRECTORY");
	}
	
	private JPanel getTreePanel(TreeHandler treeHandler) {
		debugTree = new JTree(treeHandler);
		debugTree.setEditable(false);
		
		JPanel panel = new JPanel();
		
		JScrollPane scrollPane = new JScrollPane(debugTree);
		panel.setLayout(new BorderLayout());
		panel.setPreferredSize(new Dimension(800, 600));
		panel.add(scrollPane, BorderLayout.CENTER);
		
		return panel;
	}
	
	private JPanel getTablePanel(TableHandler tableHandler) {
		JComponent table = new JTable(tableHandler);
		JPanel panel = new JPanel();
		
		JScrollPane scrollPane = new JScrollPane(table);
		panel.setLayout(new BorderLayout());
		panel.setPreferredSize(new Dimension(800, 600));
		panel.add(scrollPane, BorderLayout.CENTER);
		
		return panel;
	}
	
	
	private void editPreferences() {
		
	}
	
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
			CmdLineParser.Option ntOpt = parser.addBooleanOption('n', "new-technology");
			CmdLineParser.Option timeOpt = parser.addBooleanOption('t', "timing");
			CmdLineParser.Option backendDebugOpt = parser.addBooleanOption('B', "backend-files");
			
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
			if(dumpOutputToFile!=null)
			{
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
			// enableNT = parser.getOptionValue(ntOpt) != null;
			printTiming = parser.getOptionValue(timeOpt) != null;
			backendEmitDebugFiles = parser.getOptionValue(backendDebugOpt) != null;
			
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
			
			String[] rem = parser.getRemainingArgs();
			if(rem.length == 0) {
				printUsage();
				System.exit(2);
			}
			else {
				inputFile = new File(rem[0]);
				if(rem[0].indexOf('/') != -1 || rem[0].indexOf('\\') != -1)
					sourcePath = inputFile.getAbsoluteFile().getParentFile();
				else
					sourcePath = new File(".");
				debugPath = new File(sourcePath, inputFile.getName() + "_debug");
				modelPaths.add(sourcePath);
			}
		}
		catch(CmdLineParser.OptionException e) {
			System.err.println(e.getMessage());
			printUsage();
			System.exit(2);
		}
	}
	
	public boolean backendEmitDebugFiles() {
		return backendEmitDebugFiles;
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
	
	private boolean parseInput(File inputFile) {
		boolean res = false;
		
		GRParserEnvironment env = new GRParserEnvironment(this);
		root = env.parseActions(inputFile);
		res = !env.hadError();
		
		debug.report(NOTE, "result: " + res);
		return res;
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
		irUnit = ((UnitNode) root).getUnit();
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
		} catch(IllegalAccessException e) {
			System.err.println("no rights to create backend class: " + backend);
		} catch(InstantiationException e) {
			System.err.println("cannot create backend class: " + backend);
		}	catch(BackendException e) {
			System.err.println("backend factory error: " + e.getMessage());
		}
		
		if (ErrorReporter.getErrorCount() > 0) {

			if (ErrorReporter.getErrorCount() == 1)
				System.err.println("There was " + ErrorReporter.getErrorCount() + " error(s)");
			else
				System.err.println("There were " + ErrorReporter.getErrorCount() + " error(s)");

			System.exit(-1);
		}
		else if (ErrorReporter.getWarnCount() > 0){
			if (ErrorReporter.getWarnCount() == 1)
				System.err.println("There was " + ErrorReporter.getWarnCount() + " warning(s)");
			else
				System.err.println("There were " + ErrorReporter.getWarnCount() + " warning(s)");
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
		
		// parse the input file and exit, if there were errors
		if(!parseInput(inputFile))
			System.exit(1);
		
		parse += System.currentTimeMillis();
		manifest = -System.currentTimeMillis();
		
		if(!BaseNode.manifestAST(root)) {
			if(dumpAST)
				dumpVCG(root, new GraphDumpVisitor(), "error-ast");
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
		
		// Construct the Immediate representation.
		buildIR = -System.currentTimeMillis();
		buildIR();
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
		
		debug.report(NOTE, "finished");
		
		if(graphic && debugTree != null) {
			debugTree.expandRow(0);
			debugTree.expandRow(1);
		}
		
		codeGen = -System.currentTimeMillis();
		if(backend != null)
			generateCode();
		codeGen += System.currentTimeMillis();
		
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



