/**
 * Created on Mar 12, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

import antlr.ANTLRException;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.be.java.SQLBackend;
import de.unika.ipd.grgen.be.sql.PreferencesSQLParameters;
import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.parser.antlr.GRLexer;
import de.unika.ipd.grgen.parser.antlr.GRParser;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.report.ErrorReporter;


/**
 * A libgr test program.
 */
public class Test extends Base {

	Unit unit;
	BaseNode root;
	ErrorReporter reporter;
	
	private Connection makeConnection(String user, String passwd) {
		try {
			
			Class postgresDriver = Class.forName("org.postgresql.Driver");
		} catch (ClassNotFoundException e) {
			System.out.println("could not load postgres driver");
			System.exit(1);
		}

		Connection conn = null;
		
		try {
			conn = DriverManager.getConnection("jdbc:postgresql:database", user, passwd);
		} catch (SQLException e1) {
			System.out.println("could not get a connection");
			System.exit(1);
		}

		return conn;
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
		
		debug.report(NOTE, "result: " + res);
		debug.leaving();
		
		if(res) 
			unit = (Unit) root.checkIR(Unit.class);
		
		return res;
	}
	
	public JoinedFactory load(String filename) {
		JoinedFactory res = null;
		Connection conn = makeConnection("postgres", "");
		
		SQLParameters params = new PreferencesSQLParameters();
		SQLBackend backend = new SQLBackend(conn, params);
		
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
		
	}
	
	Test() {
		reporter = new ErrorReporter();
	}
	
	public static void main(String[] args) { 
		
		Test prg = new Test();
		
		if(args.length > 0)
			prg.run(args[0]);
		
	}
}
