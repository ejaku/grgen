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
import de.unika.ipd.grgen.be.sql.TypeID;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.libgr.actions.Action;
import de.unika.ipd.libgr.actions.Match;
import de.unika.ipd.libgr.actions.Matches;
import de.unika.ipd.libgr.graph.Edge;
import de.unika.ipd.libgr.graph.Graph;
import de.unika.ipd.libgr.graph.Node;


/**
 * A SQL action.
 */
class SQLAction implements Action, RewriteHandler {

	private static final Match INVALID_MATCH = new Match() {
		public boolean isValid() {
			return false;
		}
		
		public Map getNodes() {
			return null;
		}
		
		public Map getEdges() {
			return null;
		}
	};

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
		
		public Match get(int whichOne) {
			try {
				int cols = result.getMetaData().getColumnCount();
				int[] res = new int[cols];
				
				result.absolute(whichOne);
				
				for(int i = 0; i < cols; i++) 
					res[i] = result.getInt(i);
			
				return new SQLMatch(res);
			} catch(SQLException e) {
				// TODO error handling.
				return INVALID_MATCH;
			}
		}
		
		/**
		 * @see de.unika.ipd.libgr.actions.Matches#count()
		 */
		public int count() {
			return numberOfMatches;
		}
	}
	
	private class SQLMatch implements Match {
		
		private int[] ids;
		
		SQLMatch(int[] ids) {
			this.ids = ids;
		}
		
		/**
		 * @see de.unika.ipd.libgr.actions.Match#getEdges()
		 */
		public Map getEdges() {
			// TODO Auto-generated method stub
			return null;
		}
		
		/**
		 * @see de.unika.ipd.libgr.actions.Match#getNodes()
		 */
		public Map getNodes() {
			// TODO Auto-generated method stub
			return null;
		}
		
		/**
		 * @see de.unika.ipd.libgr.actions.Match#isValid()
		 */
		public boolean isValid() {
			return true;
		}
	}

	/**
	 * Data needed for a rewrite step.
	 */
	private abstract class RewriteStep {

		private int stmtId;
		
		protected RewriteStep(int stmtId) {
			this.stmtId = stmtId;
		}

		abstract int[] prepare(SQLMatch match);
		
		final void apply(SQLMatch m) {
			int[] prepared = prepare(m);
			queries.execUpdate(stmtId, prepared);
		}
	}
	
	/**
	 * A rewrite step which is given an index to the match table.
	 */
	private class IndexStep extends RewriteStep {
		
		private int index;
		
		IndexStep(int stmt, int index) {
			super(stmt);
			this.index = index;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.java.SQLAction.RewriteStep#prepare(de.unika.ipd.grgen.be.java.SQLMatch)
		 */
		int[] prepare(SQLMatch match) {
			return new int[] { match.ids[index] };
		}
	}

	/**
	 * A rewrite step which changes the type of a node in the database.
	 */
	private class ChangeNodeTypeStep extends RewriteStep {
		
		private final int nodeIndex;
		private final int typeId;
		
		ChangeNodeTypeStep(int nodeIndex, int typeId) {
			super(Queries.CHANGE_NODE_TYPE);
			this.nodeIndex = nodeIndex;
			this.typeId = typeId;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.java.SQLAction.RewriteStep#prepare(de.unika.ipd.grgen.be.java.SQLMatch, int[])
		 */
		int[] prepare(SQLMatch match) {
			int[] res = new int[] { match.ids[nodeIndex], typeId };
			return res;
		}
	}

	/** Map each node in the match to its column number in the match table. */
	private Map nodeIndexMap = new HashMap();
	
	/** Map each edge in the match to its column number in the match table. */
	private Map edgeIndexMap = new HashMap();
	
	/** The IR matching action this action implements. */
	private MatchingAction action;
	
	/** The matching statement. */
	private PreparedStatement stmt;
	
	/** Database context. */
	private Queries queries;
	
	/** Someone who gives IDs for types. */
	private TypeID typeId;
	
	/** An error reporter. */
	private ErrorReporter reporter;
	
	/** The rewrite steps to take. */
	List rewriteSteps = new LinkedList();
	
	SQLAction(MatchingAction action, TypeID typeId, Queries queries, SQLGenerator generator,
			ErrorReporter reporter) {
		this.action = action;
		this.queries = queries;
		this.typeId = typeId;
		this.reporter = reporter;

		// Generate the SQL match statement.
		List nodes = new LinkedList();
		List edges = new LinkedList();
		String stmtString = generator.genMatchStatement(action, nodes, edges);
		
		// Put the matched nodes and edges in map with their index in the match.
		int i = 0;
		for(Iterator it = nodes.iterator(); it.hasNext(); i++)
			nodeIndexMap.put(it.next(), new Integer(i));
		
		for(Iterator it = edges.iterator(); it.hasNext(); i++)
			edgeIndexMap.put(it.next(), new Integer(i));
		
		Base.debug.report(Base.NOTE, "prepareing statement: " + stmtString);
		
		try {
			stmt = queries.getConnection().prepareStatement(stmtString);
		} catch(SQLException e) {
			reporter.error("could not prepare statement");
		}
	}
	
	private int getIndex(Node n) {
		return ((Integer) nodeIndexMap.get(n)).intValue();
	}
	
	private int getIndex(Edge n) {
		return ((Integer) edgeIndexMap.get(n)).intValue();
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
	public void finish(Match match) {
		assert match instanceof SQLMatch : "need to have an SQL match";

		SQLMatch m = (SQLMatch) match;
		for(Iterator it = rewriteSteps.iterator(); it.hasNext();) {
			RewriteStep step = (RewriteStep) it.next();
			step.apply(m);
		}

	}
	
	/**
	 * @see de.unika.ipd.libgr.Named#getName()
	 */
	public String getName() {
		return action.getIdent().toString();
	}

	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#insertNodes(java.util.Collection)
	 */
	public void insertNodes(Collection nodes) {
		for(Iterator it = nodes.iterator(); it.hasNext();) {
			Node n = (Node) it.next();
			rewriteSteps.add(new IndexStep(Queries.ADD_NODE, getIndex(n)));
		}
	}

	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#changeNodeTypes(java.util.Map)
	 */
	public void changeNodeTypes(Map nodeTypeMap) {
		for(Iterator it = nodeTypeMap.keySet().iterator(); it.hasNext();) {
			Node n = (Node) it.next();
			int typeId = ((Integer) nodeTypeMap.get(n)).intValue();
			rewriteSteps.add(new ChangeNodeTypeStep(getIndex(n), typeId));
		}
	}

	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteEdges(java.util.Collection)
	 */
	public void deleteEdges(Collection edges) {
		for(Iterator it = edges.iterator(); it.hasNext();) {
			Edge e = (Edge) it.next();
			rewriteSteps.add(new IndexStep(Queries.REMOVE_EDGE, getIndex(e)));
		}
	}

	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteEdgesOfNodes(java.util.Collection)
	 */
	public void deleteEdgesOfNodes(Collection nodes) {
		for(Iterator it = nodes.iterator(); it.hasNext();) {
			Edge e = (Edge) it.next();
			rewriteSteps.add(new IndexStep(Queries.ADD_EDGE, getIndex(e)));
		}
	}

	/**
	 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteNodes(java.util.Collection)
	 */
	public void deleteNodes(Collection nodes) {
		for(Iterator it = nodes.iterator(); it.hasNext();) {
			Edge e = (Edge) it.next();
			rewriteSteps.add(new IndexStep(Queries.REMOVE_NODE, getIndex(e)));
		}
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
