/**
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.sql.Connection;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import de.unika.ipd.grgen.be.sql.SQLFormatter;
import de.unika.ipd.grgen.be.sql.SQLGenerator;
import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.libgr.JoinedFactory;
import de.unika.ipd.libgr.actions.Action;
import de.unika.ipd.libgr.actions.Actions;
import de.unika.ipd.libgr.graph.Graph;


/**
 * A Java/SQL backend.
 */
public class SQLBackend extends JavaIdBackend implements Actions, JoinedFactory {
	
	/** Map action names to SQLActions. */
	private Map actions = new HashMap();
	
	/** The SQL code generator. */
	private final SQLGenerator sqlGen;
	
	/** The SQL code formatter. */
	private final SQLFormatter sqlFormatter;
	
	/** The database context. */
	private Queries queries;
	
	/** The error reporter. */
	private ErrorReporter reporter;
	
	/** Database parameters. */
	private SQLParameters params;
	
	/** The connection factory that generates new connections for a graph. */
	private ConnectionFactory connectionFactory;

	/** Has the {@link #init(Unit, ErrorReporter, String)} method already been called. */
	private boolean initialized = false;
	
	
	public SQLBackend(SQLParameters params, ConnectionFactory connectionFactory) {
		this.params = params;
		this.sqlFormatter = new JavaSQLFormatter(params, this);
		this.sqlGen = new SQLGenerator(params, sqlFormatter, this);
		this.connectionFactory = connectionFactory;
	}
	
	private final void assertInitialized() {
		assert initialized : "the init method must be called first";
	}

	/**
	 * @see de.unika.ipd.grgen.be.Backend#done()
	 */
	public void done() {
	}

	/**
	 * @see de.unika.ipd.grgen.be.Backend#generate()
	 */
	public void generate() {
		assertInitialized();
		
		debug.entering();
		
		for(Iterator it = actionMap.keySet().iterator(); it.hasNext();) {
			MatchingAction a = (MatchingAction) it.next();
			int id = ((Integer) actionMap.get(a)).intValue();

			SQLAction act = new SQLAction(a, this, queries, sqlGen, reporter);
			actions.put(a.getIdent().toString(), act);
			
			debug.report(NOTE, "action: " + a.getIdent());
		}
		
		debug.leaving();
	}
	
	/**
	 * @see de.unika.ipd.libgr.actions.Actions#getAction(java.lang.String)
	 */
	public Action get(String name) {
		assertInitialized();
		return (Action) actions.get(name);
	}
	
	/**
	 * @see de.unika.ipd.libgr.actions.Actions#getActions()
	 */
	public Iterator get() {
		assertInitialized();
		return actions.values().iterator();
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.GraphFactory#getGraph(java.lang.String)
	 */
	public Graph getGraph(String name) {
		assertInitialized();
		
		Graph res = null;
		
		try {
			Connection conn = connectionFactory.connect();
			Queries queries = new DatabaseContext(name, params, conn, reporter);

			res = new SQLGraph(name, this, queries, reporter);
		} catch(SQLException e) {
			reporter.error("could not make database connection: " + e.toString());
		}
		
		return res;
	}
	
	/**
	 * @see de.unika.ipd.libgr.actions.ActionsFactory#getActions()
	 */
	public Actions getActions() {
		assertInitialized();
		return this;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.Backend#init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.util.report.ErrorReporter, java.lang.String)
	 */
	public void init(Unit unit, ErrorReporter reporter, String outputPath) {
		super.init(unit, reporter, outputPath);
		this.reporter = reporter;
	}
}
