/**
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.sql.Connection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

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
	private SQLGenerator sqlGen;
	
	/** The database context. */
	private Queries queries;
	
	/** The error reporter. */
	private ErrorReporter reporter;
	
	/**
	 * Make a new Java/SQL backend.
	 * @param connection The database connection
	 * @param params The SQL parameters.
	 * @param reporter An error reporter.
	 */
	public SQLBackend(Connection connection, SQLParameters params) {
		this.queries = new DatabaseContext(params, connection);
		this.sqlGen = new JavaSQLGenerator(params, this);
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
		for(Iterator it = actionMap.keySet().iterator(); it.hasNext();) {
			MatchingAction a = (MatchingAction) it.next();
			int id = ((Integer) actionMap.get(a)).intValue();

			SQLAction act = new SQLAction(a, this, queries, sqlGen, reporter);
			actions.put(a.getIdent().toString(), act);
		}
	}
	
	/**
	 * @see de.unika.ipd.libgr.actions.Actions#getAction(java.lang.String)
	 */
	public Action get(String name) {
		return (Action) actions.get(name);
	}
	
	/**
	 * @see de.unika.ipd.libgr.actions.Actions#getActions()
	 */
	public Iterator get() {
		return actions.values().iterator();
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.GraphFactory#getGraph(java.lang.String)
	 */
	public Graph getGraph(String name) {
		return new SQLGraph(name, this, queries, reporter);
	}
	
	/**
	 * @see de.unika.ipd.libgr.actions.ActionsFactory#getActions()
	 */
	public Actions getActions() {
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
