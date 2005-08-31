/**
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import de.unika.ipd.grgen.be.sql.meta.*;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.IDBase;
import de.unika.ipd.grgen.be.sql.SQLGenerator;
import de.unika.ipd.grgen.be.sql.SQLParameters;
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
import java.io.PrintStream;
import java.sql.Connection;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;


/**
 * A Java/SQL backend.
 */
public class SQLBackend extends IDBase
	implements Actions, JoinedFactory, MarkerSourceFactory, Dialect {
	
	/** Map action names to SQLActions. */
	private final Map<String, SQLAction> actions = new HashMap<String, SQLAction>();
	
	/** The SQL code generator. */
	private final SQLGenerator sqlGen;
	
	/** The database context. */
	private Queries queries;
	
	/** The error reporter. */
	private ErrorReporter reporter;
	
	private Sys system;
	
	/** Database parameters. */
	private final SQLParameters params;
	
	/** The connection factory that generates new connections for a graph. */
	private final ConnectionFactory connectionFactory;

	/** Has the {@link #init(Unit, ErrorReporter, String)} method already been called. */
	private boolean initialized = false;
	
	private final MetaFactory factory;
	
	private final DataType idType;
	private final DataType intType;
	private final DataType stringType;
	private final DataType booleanType;
	
	protected static final class MyDataType implements DataType {
		private final String text;
		private final int typeId;
		private final Term init;
		
		MyDataType(String text, int typeId, Term init) {
			this.text = text;
			this.typeId = typeId;
			this.init = init;
		}
		
		/**
		 * Get the SQL representation of the datatype.
		 * @return The SQL representation of the datatype.
		 */
		public String getText() {
			return text;
		}
		
		/**
		 * Print the meta construct.
		 * @param ps The print stream.
		 */
		public void dump(PrintStream ps) {
			ps.print(getText());
		}
		
		/**
		 * Get some special debug info for this object.
		 * This is mostly verbose stuff for dumping.
		 * @return Debug info.
		 */
		public String debugInfo() {
			return getText();
		}
		
		/**
		 * Return the type id for this type.
		 * @return A type id that represents this type.
		 */
		public int classify() {
			return typeId;
		}
		
		/**
		 * Give an expression that is a default initializer
		 * for this type.
		 * @return An expression that represents the default initializer
		 * for an item of this type.
		 */
		public Term initValue() {
			return init;
		}
	}
	
	/**
	 * Get the id datatype.
	 * @return The id datatype.
	 */
	public DataType getIdType() {
		return idType;
	}
	
	/**
	 * Get the integer datatype.
	 * @return The integer datatype.
	 */
	public DataType getIntType() {
		return intType;
	}
	
	/**
	 * Get the string datatype.
	 * @return The string datatype.
	 */
	public DataType getStringType() {
		return stringType;
	}
	
	/**
	 * Get the boolean datatype.
	 * @return The boolean datatype.
	 */
	public DataType getBooleanType() {
		return booleanType;
	}
	
	
	public SQLBackend(SQLParameters params, ConnectionFactory connectionFactory) {
		
		this.params = params;
		this.sqlGen = new SQLGenerator(params, this, this);
		this.connectionFactory = connectionFactory;
		this.factory = new DefaultMetaFactory(this, params, nodeAttrMap, edgeAttrMap);

		idType = new MyDataType("int", DataType.ID, factory.constant(-1));
		intType = new MyDataType("int", DataType.INT, factory.constant(0));
		stringType = new MyDataType("text", DataType.STRING, factory.constant(""));
		booleanType = new MyDataType("int", DataType.BOOLEAN, factory.constant(0));
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
		
		for(Iterator<de.unika.ipd.grgen.ir.Action> it = actionMap.keySet().iterator(); it.hasNext();) {
			MatchingAction a = (MatchingAction) it.next();
			int id = actionMap.get(a).intValue();

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
	public Iterator<SQLAction> actions() {
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
			queries = new DatabaseContext(params, conn, reporter);
		} catch(SQLException e) {
			reporter.error(e.toString());
			e.printStackTrace(System.err);
		}
	}
}

