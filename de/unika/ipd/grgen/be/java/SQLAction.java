/**
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import de.unika.ipd.grgen.be.rewrite.RewriteHandler;
import de.unika.ipd.grgen.be.rewrite.SPORewriteGenerator;
import de.unika.ipd.grgen.be.sql.SQLGenerator;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.libgr.actions.Action;
import de.unika.ipd.libgr.actions.Matches;
import de.unika.ipd.libgr.graph.Edge;
import de.unika.ipd.libgr.graph.Graph;
import de.unika.ipd.libgr.graph.Node;


/**
 * A SQL action.
 */
public class SQLAction implements Action, RewriteHandler {

	/** 
	 * Matches found by an SQL action.
	 */
	private class SQLMatches implements Matches {

		/** The result set of the query. */
		private ResultSet result;
		
		/** Number of matches in the result set. */
		private int numberOfMatches = 0;
		
		SQLMatches(ResultSet result) {
			this.result = result;
			
			try {
				if(result.last()) {
					numberOfMatches = result.getRow();
					result.first();
				}
			} catch(SQLException e) {
				// TODO error handling.
			}
			
		}
		
		/**
		 * @see de.unika.ipd.libgr.actions.Matches#count()
		 */
		public int count() {
			return numberOfMatches;
		}

		/**
		 * @see de.unika.ipd.libgr.actions.Matches#getEdges(int)
		 */
		public Edge[] getEdges(int whichOne) {
			return null;
		}

		/**
		 * @see de.unika.ipd.libgr.actions.Matches#getNodes(int)
		 */
		public Node[] getNodes(int whichOne) {
			return null;
		}
	}
	
	private Map nodeIndexMap = new HashMap();
	
	private Map edgeIndexMap = new HashMap();
	
	/** List of nodes in the match (ordered). */
	private List nodes = new LinkedList();

	/** List of edges in the match (ordered). */
	private List edges = new LinkedList();
	
	private MatchingAction action;
	private String stmtString;
	private PreparedStatement stmt; 
	private Queries queries;
	
	SQLAction(MatchingAction action, SQLGenerator gen, Queries queries) {
		this.action = action;

		List nodes = new LinkedList();
		List edges = new LinkedList();
		stmtString = gen.genMatchStatement(action, nodes, edges);
		
		int i = 0;
		for(Iterator it = nodes.iterator(); it.hasNext(); i++)
			nodeIndexMap.put(it.next(), new Integer(i));
		
		i = 0;
		for(Iterator it = edges.iterator(); it.hasNext(); i++)
			edgeIndexMap.put(it.next(), new Integer(i));
		
		try {
			stmt = queries.getConnection().prepareStatement(stmtString);
		} catch(SQLException e) {
			// TODO Error handling
		}
	}
	
	/**
	 * @see de.unika.ipd.libgr.actions.Action#apply(de.unika.ipd.libgr.graph.Graph)
	 */
	public Matches apply(Graph graph) {
		assert stmt != null : "Must prepare the statement before";
		
		try {
			ResultSet set = stmt.executeQuery();
			return new SQLMatches(set);
		} catch(SQLException e) {
		}

		return null;
	}

	/**
	 * @see de.unika.ipd.libgr.actions.Action#finish(de.unika.ipd.libgr.actions.Match)
	 */
	public void finish(Matches m, int which) {

	}
	
	/**
	 * @see de.unika.ipd.libgr.Named#getName()
	 */
	public String getName() {
		return action.getIdent().toString();
	}

	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#changeNodeTypes(java.util.Map)
	 */
	public void changeNodeTypes(Map nodeTypeMap) {
	}

	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteEdges(java.util.Collection)
	 */
	public void deleteEdges(Collection edges) {
	}

	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteEdgesOfNodes(java.util.Collection)
	 */
	public void deleteEdgesOfNodes(Collection nodes) {
	}

	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteNodes(java.util.Collection)
	 */
	public void deleteNodes(Collection nodes) {
	}

	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#finish()
	 */
	public void finish() {
	}

	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#insertEdges(java.util.Collection)
	 */
	public void insertEdges(Collection edges) {
	}

	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#insertNodes(java.util.Collection)
	 */
	public void insertNodes(Collection nodes) {
	}

	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#start(de.unika.ipd.grgen.ir.Rule, java.lang.Class)
	 */
	public void start(Rule rule) {
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#getRequiredRewriteGenerator()
	 */
	public Class getRequiredRewriteGenerator() {
		return SPORewriteGenerator.class;
	}
}
