/**
 * Created on Mar 12, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.be.java.ConnectionFactory;
import de.unika.ipd.grgen.be.java.DefaultConnectionFactory;
import de.unika.ipd.grgen.be.java.SQLBackend;
import de.unika.ipd.grgen.be.sql.PreferencesSQLParameters;
import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.parser.antlr.GRParserEnvironment;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.NullOutputStream;
import de.unika.ipd.grgen.util.report.DebugReporter;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.grgen.util.report.StreamHandler;
import de.unika.ipd.libgr.graph.Graph;
import java.io.File;
import java.io.OutputStream;


/**
 * A libgr test program.
 */
public class Test extends Base implements Sys {

	Unit unit;
	BaseNode root;
	ErrorReporter reporter;
	File modelPath;
	
	private void loadJDBCDrivers() {

		try {
			Class cls = Class.forName("org.postgresql.Driver");
		} catch (ClassNotFoundException e) {
			System.out.println("Cannot load driver.");
			System.exit(1);
		}

	}

	boolean parseInput(String inputFile) {
		boolean res = false;
		
		GRParserEnvironment env = new GRParserEnvironment(this);
		root = env.parse(new File(inputFile));
		res = root != null;

		if(res)
			res = BaseNode.manifestAST(root);

		if(res)
			unit = (Unit) root.checkIR(Unit.class);
		
		debug.report(NOTE, "result: " + res);
		
		return res;
	}

	public ErrorReporter getErrorReporter() {
		return reporter;
	}
	
	public File[] getModelPaths() {
		return new File[] { modelPath };
	}
	
	public boolean backendEmitDebugFiles() {
		return false;
	}

	public JoinedFactory load(String filename) {
		JoinedFactory res = null;
		ConnectionFactory connFactory =
			new DefaultConnectionFactory("jdbc:postgresql:test", "postgres", "");
		
		SQLParameters params = new PreferencesSQLParameters();
		SQLBackend backend = new SQLBackend(params, connFactory);

		if(parseInput(filename)) {
			
			backend.init(unit, this, new File(""));
			backend.generate();
			backend.done();
			
			res = backend;
		}
		
		return res;
	}

	public void run(String filename) {
		modelPath = new File(filename).getAbsoluteFile().getParentFile();
		JoinedFactory factory = load(filename);
	
		Graph g = factory.getGraph("Test", false);
	}
	
	Test() {
		
		StreamHandler handler = new StreamHandler(System.out);
		reporter = new ErrorReporter();
		reporter.addHandler(handler);
		
		DebugReporter deb = new DebugReporter(10);
		
		Base.setReporters(deb, reporter);
		// Base.debug.addHandler(handler);
		loadJDBCDrivers();
	}
	
	public static void main(String[] args) {
		Test prg = new Test();
		
		if(args.length > 0)
			prg.run(args[0]);
	}
	
	public OutputStream createDebugFile(File file) {
		return NullOutputStream.STREAM;
	}
	
	
	
}

