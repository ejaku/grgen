/**
 * Created on Mar 12, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr;

import java.io.FileInputStream;
import java.io.FileNotFoundException;

import antlr.ANTLRException;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.be.java.ConnectionFactory;
import de.unika.ipd.grgen.be.java.DefaultConnectionFactory;
import de.unika.ipd.grgen.be.java.SQLBackend;
import de.unika.ipd.grgen.be.sql.PreferencesSQLParameters;
import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.parser.antlr.GRLexer;
import de.unika.ipd.grgen.parser.antlr.GRParser;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.report.DebugReporter;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.grgen.util.report.StreamHandler;
import de.unika.ipd.libgr.graph.Graph;


/**
 * A libgr test program.
 */
public class Test extends Base {

	Unit unit;
	BaseNode root;
	ErrorReporter reporter;
	
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
		
		debug.entering();
		try {
			GRLexer lex = new GRLexer(new FileInputStream(inputFile));
			GRParser parser = new GRParser(lex);
			
			try {
				parser.setFilename(inputFile);
				parser.init(reporter);
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

		if(res)
			res = BaseNode.manifestAST(root);

		if(res) 
			unit = (Unit) root.checkIR(Unit.class);
		
		debug.report(NOTE, "result: " + res);
		debug.leaving();
		
		return res;
	}
	
	public JoinedFactory load(String filename) {
		JoinedFactory res = null;
		ConnectionFactory connFactory = 
			new DefaultConnectionFactory("jdbc:postgresql:test", "postgres", "");
		
		SQLParameters params = new PreferencesSQLParameters();
		SQLBackend backend = new SQLBackend(params, connFactory);
		
		if(parseInput(filename)) {

			
			backend.init(unit, reporter, "");
			backend.generate();
			backend.done();
			
			res = backend;
		}
		
		return res;
	}

	public void run(String filename) {
		JoinedFactory factory = load(filename);
	
		Graph g = factory.getGraph("Test");
		
		
	}
	
	Test() {
		
		StreamHandler handler = new StreamHandler(System.out);
		reporter = new ErrorReporter();
		reporter.addHandler(handler);
		
		Base.setReporters(new DebugReporter(10), reporter);
		
		Base.debug.addHandler(handler);

		loadJDBCDrivers();
	}
	
	public static void main(String[] args) { 
		Test prg = new Test();
		
		if(args.length > 0)
			prg.run(args[0]);
		
	}
}
