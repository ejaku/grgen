/*
 * Created on Jul 6, 2003
 */

package de.unika.ipd.grgen;

import jargs.gnu.CmdLineParser;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.Rectangle;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.PrintStream;
import java.util.prefs.Preferences;

import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTree;

import antlr.ANTLRException;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.UnitNode;
import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.BackendException;
import de.unika.ipd.grgen.be.BackendFactory;
import de.unika.ipd.grgen.ir.DumpVisitor;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.parser.antlr.GRLexer;
import de.unika.ipd.grgen.parser.antlr.GRParser;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.GraphDumpVisitor;
import de.unika.ipd.grgen.util.PostWalker;
import de.unika.ipd.grgen.util.PrePostWalker;
import de.unika.ipd.grgen.util.VCGDumper;
import de.unika.ipd.grgen.util.Walkable;
import de.unika.ipd.grgen.util.report.DebugReporter;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.grgen.util.report.Handler;
import de.unika.ipd.grgen.util.report.NullReporter;
import de.unika.ipd.grgen.util.report.Reporter;
import de.unika.ipd.grgen.util.report.StreamHandler;
import de.unika.ipd.grgen.util.report.TreeHandler;

/**
 * Main.java
 *
 *
 * Created: Wed Jul  2 11:22:43 2003
 *
 * @author Sebastian Hack
 * @version 1.0
 */
public class Main extends Base {

	private String[] args;
	private String inputFile;
	private BaseNode root;
	private Unit irUnit;
	private ErrorReporter errorReporter;
	private Reporter debugReporter;
	private Handler debugHandler;
	
	/** enable debugging */
	private boolean debugEnabled;
	
	/** enable ast printing */
	private boolean dumpAST;
	
	/** enable ir dumping */
	private boolean dumpIR;
	
	/** debug filter regular expression */
	private String debugFilter;
	
	/** inverse debug filter regular expression */
	private String invDebugFilter;
	
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
	private String outputPath = ".";
	
	private void printUsage() {
		System.out.println("usage: grgen [options] filename");
		System.out.println("Options are:");
		System.out.println("  -d, --debug                       enable debugging");
		System.out.println("  -a, --dump-ast                    dump the AST");
		System.out.println("  -i, --dump-ir                     dump the intermidiate representation");
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
		if(debugEnabled) {
			if(graphic) {
				debugHandler = new TreeHandler();
				debugPanel = getTreePanel((TreeHandler) debugHandler);
			} else {
				debugHandler = new StreamHandler(System.out);
			}

			DebugReporter dr = new DebugReporter(10);
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
		  CmdLineParser.Option graphicOpt = parser.addBooleanOption('g', "graphic");
		  		  
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
			
			dumpAST = parser.getOptionValue(astDumpOpt) != null;
			dumpIR = parser.getOptionValue(irDumpOpt) != null;
			debugEnabled = parser.getOptionValue(debugOpt) != null;
			graphic = parser.getOptionValue(graphicOpt) != null;
			
			/* deactivate graphic if no debug output */
			if (!debugEnabled)
				graphic = false;

			debugFilter = (String) parser.getOptionValue(debugFilterOpt);
			invDebugFilter = (String) parser.getOptionValue(invDebugFilterOpt);
			backend = (String) parser.getOptionValue(beOpt);
			String s = (String) parser.getOptionValue(optOutputPath); 
			outputPath = s != null ? s : System.getProperty("user.dir");
			
			prefsImport = (String) parser.getOptionValue(prefsImportOpt);
			prefsExport = (String) parser.getOptionValue(prefsExportOpt);
			
			String[] rem = parser.getRemainingArgs();
			if(rem.length == 0) {
				printUsage();
				System.exit(2); 
			}
			else
			  inputFile = rem[0];
  	}
  	catch(CmdLineParser.OptionException e) {
  		System.err.println(e.getMessage()); 
  		printUsage();
			System.exit(2); 		 	
  	}
  }
 
 	private boolean parseInput(String inputFile) {
 		boolean res = false;
 		
 		debug.entering();
		try {
			GRLexer lex = new GRLexer(new FileInputStream(inputFile));
			GRParser parser = new GRParser(lex);

			try {
				parser.setFilename(inputFile);
				parser.init(errorReporter);
				root = parser.text();
				res = !parser.hadError();
			}
			catch(ANTLRException e) {
				System.err.println(e.getMessage());
				System.exit(1);
			}
		}
	  catch(FileNotFoundException e) {
		  System.err.println("input file not found: " + e.getMessage());
		  System.exit(1);
	  }
	  
	  debug.report(NOTE, "result: " + res);
	  debug.leaving();
	  
	  return res; 
 	}
 	
 	private void dumpVCG(Walkable node, GraphDumpVisitor visitor, 
 		String suffix) {

		debug.entering();

		try {
			FileOutputStream fos = 
			  new FileOutputStream(inputFile + "." + suffix + ".vcg");
		
			VCGDumper vcg = new VCGDumper(new PrintStream(fos));
			visitor.setDumper(vcg);
		  PrePostWalker walker = new PostWalker(visitor);
			vcg.begin();
			walker.reset();
			walker.walk(node);								
			vcg.finish();

			fos.close();
		}
		catch(IOException e) {
			System.err.println(e.getMessage());
		}
		
		debug.leaving();
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
      
      be.init(irUnit, error, outputPath);
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
		
	}
 
 	/**
 	 * This is the main driver routine. 
 	 * It pareses the input file, constructs the AST, 
 	 * checks it, constructs the immediate representation and 
 	 * emits the code. 
 	 */
  private void run() {
		parseOptions();
  	init();
  	
  	debug.entering();
  	
  	importPrefs();
  	
  	// Open graphic debug window if desired.
		if(graphic) 
			makeMainFrame();
		
		// parse the input file and exit, if there were errors
		if(!parseInput(inputFile))
			System.exit(1);

		if(!BaseNode.manifestAST(root))
			System.exit(1);

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
		buildIR();
		
		// Dump the IR.
		if(dumpIR) 
			dumpVCG(irUnit, new DumpVisitor(), "ir");
			
		debug.report(NOTE, "finished");
			
		if(graphic && debugTree != null) {
			debugTree.expandRow(0);
			debugTree.expandRow(1);
		}
		
		if(backend != null)
			generateCode();
		
		exportPrefs();
		
		debug.leaving();
			 
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
