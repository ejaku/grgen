/**
 * Created on Mar 8, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;

import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.libgr.IntegerId;
import de.unika.ipd.libgr.graph.id.IDGraphModel;
import de.unika.ipd.libgr.graph.id.IDTypeModel;


/**
 * An SQL graph model.
 */
public abstract class SQLGraphModel implements IDGraphModel {

	/** The type model. */
	private IDTypeModel typeModel;

	/** An error reporter. */
	private ErrorReporter reporter;
	
	/** A query object. */
	private Queries queries;
	
	/** JDBC prepared statements for statement IDs. */
	protected PreparedStatement[] stmts = new PreparedStatement[Queries.COUNT];
	
	/**
	 * Get the statement for a statement ID.
	 * @param id The statment ID.
	 * @return The corresponding prepared statement.
	 */
	private PreparedStatement getStmt(int id) {
		if(id >= 0 && id < Queries.COUNT) 
			return stmts[id];
		else {
			reporter.error("Invalid statement ID: " + id);
			return null;
		}
	}
	
	/**
	 * Execute a prepared query.
	 * The elements in the <code>ids</code> array can be instance of {@link Integer}
	 * or {@link IntegerId}.
	 * @param stmtID The prepared statement.
	 * @param ids An array with integers or integer ids.
	 * @return The result set of the query.
	 */
	private ResultSet exec(int stmtID, int[] ids) {
		PreparedStatement stmt = getStmt(stmtID);
		
		for(int i = 0; i < ids.length; i++) {
			
			try {
				stmt.setInt(i, ids[i]);
			} catch (SQLException e) {
				reporter.error(e.toString());
			}
		}
		
		try {
			return stmt.executeQuery();
		} catch (SQLException e) {
			reporter.error(e.toString());
		}

		return null;
	}
	
	/**
	 * Execute a prepared query.
	 * @param stmtID The statement ID.
	 * @param id An object instance of {@link Integer} or {@link IntegerId}. 
	 * @return The result set of the query.
	 */
	private ResultSet exec(int stmtID, int id) {
		return exec(stmtID, new int[] { id });
	}
	
	/**
	 * Execute a update/insert/delete statement.
	 * @param stmtId The statement ID.
	 * @param params Parameters.
	 * @return Number of modified columns.
	 */
	private int execUpdate(int stmtId, int[] params) {
		PreparedStatement stmt = getStmt(stmtId);
		
		for(int i = 0; i < params.length; i++) {
			try {
				stmt.setInt(i, params[i]);
			} catch(SQLException e) {
				reporter.error(e.toString());
			}
		}

		try {
			return stmt.executeUpdate();
		} catch(SQLException e) {
			reporter.error(e.toString());
		}
		
		return 0;
	}
	
	private int execUpdate(int stmtId, int params) {
		return execUpdate(stmtId, new int[] { params });
	}
	
	/**
	 * Consume a result set.
	 * Must be called for each query which does not use its result to free
	 * database resources. 
	 * @param res The result set of the query.
	 * @return The number of rows in the result set.
	 */
	private int consume(ResultSet res) {
		int rows = 0;
		
		try {
			for(rows = 0; res.next(); rows++);
		} catch (SQLException e) {
			reporter.error("cannot consume query result");
		}
		
		return rows;
	}
	
	/**
	 * Return an ID from a result set.
	 * This method should be applied for result sets that return one row and one
	 * column containing an integer.
	 * @param res The result set.
	 * @return The ID.
	 */
	private int getId(ResultSet res) {
		int id = -1;
		
		try {
			for(int i = 0; res.next(); i++) {
				if(i == 0)
					id = res.getInt(0);
				else
					reporter.error("Query returned more than one row");
			}
		} catch(SQLException e) {
			reporter.error("Could not retrieve data");
		}
		
		return id;
	}
	
	/**
	 * Get all IDs in the result set.
	 * @param res The result set.
	 * @return An iterator iterating over {@link Integer} objects containing
	 * the IDs. The set may also be empty.
	 */
	private int[] getIds(ResultSet res) {
		
		try {
			Collection ids = new LinkedList();

			while(res.next())
				ids.add(new Integer(res.getInt(0)));
				
			int[] result = new int[ids.size()];
			int i = 0;
			for(Iterator it = ids.iterator(); it.hasNext(); i++)
				result[i] = ((Integer) it.next()).intValue();
			
			return result;
			
		} catch(SQLException e) {
			reporter.error("Could not retrieve data");
			return null;
		}
	}

	public void init(ErrorReporter reporter, IDTypeModel typeModel, Queries queries) {
		this.typeModel = typeModel;
		this.queries = queries;
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDGraphModel#add(int, int, int)
	 */
	public int add(int edgeType, int srcNode, int tgtNode) {
		return getId(exec(Queries.ADD_EDGE, new int[] { edgeType, srcNode, tgtNode }));
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDGraphModel#add(int)
	 */
	public int add(int nodeType) {
		return getId(exec(Queries.ADD_NODE, nodeType));
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDGraphModel#getIncoming(int)
	 */
	public int[] getIncoming(int node) {
		return getIds(exec(Queries.NODE_INCOMING, node));
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDGraphModel#getOutgoing(int)
	 */
	public int[] getOutgoing(int node) {
		return getIds(exec(Queries.NODE_OUTGOING, node));
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDGraphModel#getSource(int)
	 */
	public int getSource(int edge) {
		return getId(exec(Queries.EDGE_SOURCE, edge));
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDGraphModel#getTarget(int)
	 */
	public int getTarget(int edge) {
		return getId(exec(Queries.EDGE_TARGET, edge));
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDGraphModel#getTypeOfEdge(int)
	 */
	public int getTypeOfEdge(int edge) {
		return getId(exec(Queries.EDGE_GET_TYPE, edge));
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDGraphModel#getTypeOfNode(int)
	 */
	public int getTypeOfNode(int node) {
		return getId(exec(Queries.NODE_GET_TYPE, node));
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDGraphModel#removeEdge(int)
	 */
	public boolean removeEdge(int edge) {
		return execUpdate(Queries.REMOVE_EDGE, edge) != 0;
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDGraphModel#removeNode(int)
	 */
	public boolean removeNode(int node) {
		return execUpdate(Queries.REMOVE_NODE, node) != 0;
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDTypeModel#edgeTypeIsA(int, int)
	 */
	public boolean edgeTypeIsA(int e1, int e2) {
		return typeModel.edgeTypeIsA(e1, e2);
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDTypeModel#getEdgeRootType()
	 */
	public int getEdgeRootType() {
		return typeModel.getEdgeRootType();
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDTypeModel#getEdgeTypeSuperTypes(int)
	 */
	public int[] getEdgeTypeSuperTypes(int edge) {
		return typeModel.getEdgeTypeSuperTypes(edge);
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDTypeModel#getNodeRootType()
	 */
	public int getNodeRootType() {
		return typeModel.getNodeRootType();
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDTypeModel#getNodeTypeSuperTypes(int)
	 */
	public int[] getNodeTypeSuperTypes(int node) {
		return typeModel.getNodeTypeSuperTypes(node);
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDTypeModel#nodeTypeIsA(int, int)
	 */
	public boolean nodeTypeIsA(int n1, int n2) {
		return typeModel.nodeTypeIsA(n1, n2);
	}
}
