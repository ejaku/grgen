/**
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.IDBase;
import de.unika.ipd.grgen.be.sql.SQLGenerator;
import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.be.sql.meta.DataType;
import de.unika.ipd.grgen.be.sql.meta.MarkerSource;
import de.unika.ipd.grgen.be.sql.meta.MarkerSourceFactory;
import de.unika.ipd.grgen.be.sql.meta.MetaFactory;
import de.unika.ipd.grgen.be.sql.stmt.DefaultMarkerSource;
import de.unika.ipd.grgen.be.sql.stmt.DefaultMetaFactory;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.libgr.JoinedFactory;
import de.unika.ipd.libgr.actions.Action;
import de.unika.ipd.libgr.actions.Actions;
import de.unika.ipd.libgr.graph.Graph;
import java.io.File;
import java.sql.Connection;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;


/**
 * A Java/SQL backend.
 */
public class SQLBackend extends IDBase
	implements Actions, JoinedFactory, MarkerSourceFactory {
	
	/** Map action names to SQLActions. */
	private Map actions = new HashMap();
	
	/** The SQL code generator. */
	private final SQLGenerator sqlGen;
	
	/** The database context. */
	private Queries queries;
	
	/** The error reporter. */
	private ErrorReporter reporter;
	
	private Sys system;
	
	/** Database parameters. */
	private SQLParameters params;
	
	/** The connection factory that generates new connections for a graph. */
	private ConnectionFactory connectionFactory;

	/** Has the {@link #init(Unit, ErrorReporter, String)} method already been called. */
	private boolean initialized = false;
	
	private final MetaFactory factory;
	
	public SQLBackend(SQLParameters params, ConnectionFactory connectionFactory) {
		
		this.params = params;
		this.sqlGen = new SQLGenerator(params, this, this);
		this.connectionFactory = connectionFactory;
		
		// TODO Do this right!!
		this.factory = new DefaultMetaFactory(null, params, nodeAttrMap, edgeAttrMap);
	}
	
	private static final class JavaMarkerSource extends DefaultMarkerSource {
		public String nextMarkerString(DataType type) {
			return "?";
		}
	};
	
	public MarkerSource getMarkerSource() {
		return new JavaMarkerSource();
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
		
		for(Iterator it = actionMap.keySet().iterator(); it.hasNext();) {
			MatchingAction a = (MatchingAction) it.next();
			int id = ((Integer) actionMap.get(a)).intValue();

			SQLAction act = new SQLAction(system, a, this, queries, sqlGen, factory);
			actions.put(a.getIdent().toString(), act);
			
			debug.report(NOTE, "action: " + a.getIdent());
		}
	}
	
	/**
	 * @see de.unika.ipd.libgr.actions.Actions#getAction(java.lang.String)
	 */
	public Action getAction(String name) {
		assertInitialized();
		return (Action) actions.get(name);
	}
	
	/**
	 * @see de.unika.ipd.libgr.actions.Actions#getActions()
	 */
	public Iterator actions() {
		assertInitialized();
		return actions.values().iterator();
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.GraphFactory#getGraph(java.lang.String)
	 */
	public Graph getGraph(String name, boolean create) {
		assertInitialized();
		return new SQLGraph(name, this, queries, reporter, create);
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
	public void init(Unit unit, Sys system, File outputPath) {
		makeTypes(unit);
		this.system = system;
		this.reporter = system.getErrorReporter();

		try {
			Connection conn = connectionFactory.connect();
			queries = new DatabaseContext("Test", params, conn, reporter);
		} catch(SQLException e) {
			reporter.error(e.toString());
			e.printStackTrace(System.err);
		}
	}
}

