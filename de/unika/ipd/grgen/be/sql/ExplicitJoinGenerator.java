/**
 * Created on Mar 31, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import java.util.Arrays;
import java.util.Collection;
import java.util.Comparator;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.be.sql.meta.Column;
import de.unika.ipd.grgen.be.sql.meta.Join;
import de.unika.ipd.grgen.be.sql.meta.Opcodes;
import de.unika.ipd.grgen.be.sql.meta.Query;
import de.unika.ipd.grgen.be.sql.meta.Relation;
import de.unika.ipd.grgen.be.sql.meta.Term;
import de.unika.ipd.grgen.be.sql.stmt.AttributeTable;
import de.unika.ipd.grgen.be.sql.stmt.EdgeTable;
import de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory;
import de.unika.ipd.grgen.be.sql.stmt.NodeTable;
import de.unika.ipd.grgen.be.sql.stmt.TypeStatementFactory;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;


/**
 * A match statement generator using explicit joins.
 */
public class ExplicitJoinGenerator extends SQLGenerator {

	private final String[] edgeCols;
	
	/**
	 * @param parameters
	 * @param constraint
	 */
	public ExplicitJoinGenerator(SQLParameters parameters, TypeID typeID) {
		
		super(parameters, typeID);
		
		edgeCols = new String[] {
				parameters.getColEdgesSrcId(),
				parameters.getColEdgesTgtId()
		};
	}

	private static abstract class GraphItemComparator implements Comparator {
		
		protected int compareTypes(InheritanceType inh1, InheritanceType inh2) {
			int dist1 = inh1.getMaxDist();
			int dist2 = inh2.getMaxDist();
			
			int result = (dist1 > dist2 ? -1 : 0) + (dist1 < dist2 ? 1 : 0); 

//			debug.report(NOTE, inh1.getIdent() + "(" + dist1 + ") cmp " 
//					+ inh2.getIdent() + "(" + dist2 + "): " + result);	

			return result; 
		}
		
	}
	
	private static class NodeComparator extends GraphItemComparator {
		
		private Graph graph;
		
		NodeComparator(Graph graph) {
			this.graph = graph;
		}
		
		public int compare(Object o1, Object o2) throws ClassCastException {
			if(o1 instanceof Node && o2 instanceof Node) {
				Node n1 = (Node) o1;
				Node n2 = (Node) o2;

//				debug.entering();
//				debug.report(NOTE, "" + n1.getIdent() + " cmp " + n2.getIdent());
					int res = compareTypes(n1.getNodeType(), n2.getNodeType());
//				debug.leaving();
				
				return res;
			} else
				throw new ClassCastException();
		}
		
		public boolean equals(Object o1) {
			return false;
		}
	};
	
	
	
	private static class SearchPath {
		
		private final List edges = new LinkedList(); 
		private final Set reverseEdges = new HashSet();
		
		private static final String[] heads = new String[] { " -", " <-" };
		private static final String[] tails = new String[] { "-> ", "- " };
		
		public void dumpOld(StringBuffer sb, Graph g) {

			if(!edges.isEmpty()) {
				Edge first = (Edge) edges.get(0);
				Node curr = isReverse(first) ? g.getTarget(first) : g.getSource(first);
				
				sb.append(curr.getIdent());
				
				for(Iterator it = edges.iterator(); it.hasNext();) {
					Edge edge = (Edge) it.next();
					boolean reverse = isReverse(edge);
					int ind = reverse ? 1 : 0;
					Node node = reverse ? g.getSource(edge) : g.getTarget(edge);
					
					sb.append(heads[ind] + edge.getIdent() 
							+ tails[ind] + node.getIdent());
				}
			}
		}
		
		public void dump(StringBuffer sb, Graph g) {
			for(Iterator it = edges.iterator(); it.hasNext();) {
				Edge edge = (Edge) it.next();
				Node src = g.getSource(edge);
				Node tgt = g.getTarget(edge);
				boolean reverse = isReverse(edge);
				
				sb.append(src.getIdent() + " " + edge.getIdent() + " " + tgt.getIdent() 
						+ " reverse: " + reverse + "\n");
			}
		}
		
		public void add(Edge edge, boolean reverse) {
			edges.add(edge);
			if(reverse)
				reverseEdges.add(edge);
		}
		
		public boolean isReverse(Edge edge) {
			return reverseEdges.contains(edge);
		}
	}
	
	private static class VisitContext {
		final Set visited = new HashSet();
		final List paths = new LinkedList(); 
		
		Graph graph;
		Comparator comparator;
		
		VisitContext(Graph graph, Comparator comparator) {
			this.graph = graph;
			this.comparator = comparator;
		}
	}
	
	private void visitNode(VisitContext ctx, SearchPath path, Node start) {
		
		debug.entering();
		
		if(ctx.visited.contains(start))
			return;
		
		ctx.visited.add(start);

		debug.report(NOTE, "start: " + start);
		
		int index = 0;
		Collection visited = ctx.visited;
		Graph g = ctx.graph;
		Map edges = new HashMap();
		List nodes = new LinkedList();
		Set reverse = new HashSet();
		
		for(Iterator it = g.getOutgoing(start); it.hasNext();) {
			Edge edge = (Edge) it.next();
			Node tgt = g.getTarget(edge);
			
			if(!visited.contains(tgt)) {
				nodes.add(tgt);
				edges.put(tgt, edge);
				index++;
			}
		}

		for(Iterator it = g.getIncoming(start); it.hasNext();) {
			Edge edge = (Edge) it.next();
			Node src = g.getSource(edge);
			
			if(!visited.contains(src)) {
				reverse.add(edge);
				nodes.add(src);
				edges.put(src, edge);
				index++;
			}
		}

		Node[] nodeArr = (Node[]) nodes.toArray(new Node[nodes.size()]);
		Arrays.sort(nodeArr, ctx.comparator);
		
		for(int i = 0; i < nodeArr.length; i++) {
			Node n = nodeArr[i];
			debug.report(NOTE, "index " + i + ": " + n);
			
			if(!ctx.visited.contains(n)) {
				SearchPath sp = path;
				
				if(i > 0) {
					sp = new SearchPath();
					ctx.paths.add(sp);
				}
				
				Edge edge = (Edge) edges.get(n);
				sp.add(edge, reverse.contains(edge));
				visitNode(ctx, sp, n);
			}	
		}
		
		debug.leaving();
	}
	
	private SearchPath[] computeSearchPaths(Graph pattern) {

		Collection rest = pattern.getNodes(new HashSet());
		Iterator edgeIterator = pattern.getEdges();
		Comparator comparator = new NodeComparator(pattern);

		debug.entering();
		
		if(rest.isEmpty() || !edgeIterator.hasNext())
			return new SearchPath[0];			
		
		VisitContext ctx = new VisitContext(pattern, comparator);

		do {
			debug.report(NOTE, "rest: " + rest);
			
			Node cheapest = getCheapest(rest.iterator(), comparator);
			SearchPath path = new SearchPath();
			ctx.paths.add(path);
			
			debug.report(NOTE, "cheapest: " + cheapest);
			
			visitNode(ctx, path, cheapest);
			rest.removeAll(ctx.visited);
		} while(!rest.isEmpty());
			
		debug.leaving();
		
		return (SearchPath[]) ctx.paths.toArray(new SearchPath[ctx.paths.size()]);
	}
	
	/**
	 * Get the node with the most specific type.
	 * @param nodes The node collection to get the most specific from.
	 * @return The node with the most specific node type in <code>nodes</code>.
	 * If the iterator did not contain any nodes <code>null</code> is returned. 
	 */
	private Node getCheapest(Iterator nodes, Comparator comp) {
		Node cheapest = null;
		
		debug.entering();
		
		while(nodes.hasNext()) {
			Node curr = (Node) nodes.next();

			boolean setNewCheapest = cheapest == null || comp.compare(curr, cheapest) <= 0;
			cheapest = setNewCheapest ? curr : cheapest;
		}
		
		if(cheapest != null) {
			NodeType nt = cheapest.getNodeType();
			debug.report(NOTE, "cheapest node: " + cheapest.getIdent() 
					+ ", type: " + nt.getIdent());
		}
		
		debug.leaving();
		
		return cheapest;
	}
	
	/**
	 * An auxillary class to treat conds.
	 */
	static class CondState {
		
		Term cond;
		Set usedEntities;
		
		/**
		 * Make a new cond state.
		 * @param cond The SQL term expressing this conditional expression. 
		 * @param usedEntities All entities that are referenced in the cond expression. 
		 */
		CondState(Term cond, Set usedEntities) {
			this.cond = cond;
			this.usedEntities = usedEntities;
		}

		private boolean canDeliver(Entity ent, Collection processed) {
			return usedEntities.contains(ent) && processed.containsAll(usedEntities);
		}
		
		/**
		 * Get the cond expression, if all entites occurring in the expression
		 * are in the processed set.
		 * @param processed The processed set.
		 * @return A SQL term expressing the IR expression, if all entities referenced in
		 * the expression are in the processed set. 
		 */
		Term getCond(Entity ent, Collection processed) {
			return canDeliver(ent, usedEntities) ? cond : null;
		}
	}

	/**
	 * Stuff needed to generate SQL statements in this generator.
	 */
	static class StmtContext {
		final Graph graph;
		final TypeStatementFactory factory;
		final GraphTableFactory tableFactory;
		
		
		final Collection processedAll = new HashSet();
		final Collection processedNodes = new LinkedList();
		final Collection processedEdges = new LinkedList();
		
		/** Holds all entities appearing in cond statements. */
		final Collection conds = new HashSet();
		
		/** All CondStates. */
		final Collection allCondEntities = new HashSet();
		
		/** The current join. */
		Relation currJoin = null;

		StmtContext(Graph graph, TypeStatementFactory factory, GraphTableFactory tableFactory) {
			this.graph = graph;
			this.factory = factory;
			this.tableFactory = tableFactory;
		}
		
		boolean hasAttribute(Entity ent) {
			return allCondEntities.contains(ent);
		}
		
		Term getCond(Entity ent) {
			for(Iterator it = conds.iterator(); it.hasNext();) {
				CondState cs = (CondState) it.next();
				Term term = cs.getCond(ent, processedAll);
				
				if(term != null) {
					it.remove();
					return term;
				}
			}
			
			return null;
		}
		
		void markProcessed(Node node) {
			processedAll.add(node);
			processedNodes.add(node);
		}
		
		void markProcessed(Edge edge) {
			processedAll.add(edge);
			processedEdges.add(edge);
		}

		boolean hasBeenProcessed(Entity ent) {
			return processedAll.contains(ent);
		}
	}
	
	private Term makeNodeJoinCond(Node node, StmtContext ctx) {
		TypeStatementFactory factory = ctx.factory;
		GraphTableFactory tableFactory = ctx.tableFactory;
		
		NodeTable nodeTable = tableFactory.nodeTable(node);

		// Make type constraints
		Term res = factory.isA(node, tableFactory, typeID);
		
		// Make the clauses guaranteeing injectiveness
		for(Iterator it = ctx.processedNodes.iterator(); it.hasNext();) {
			Node curr = (Node) it.next();
			NodeTable currTable = tableFactory.nodeTable(curr);
			
			if(!node.isHomomorphic(curr))
				res = factory.addExpression(Opcodes.AND, res, 
						factory.expression(Opcodes.NE, factory.expression(nodeTable.colId()),
								factory.expression(currTable.colId())));
		}
		
		// Make probable attributes
		Term cond = ctx.getCond(node);
		
		if(cond != null) {
			AttributeTable attrTable = tableFactory.nodeAttrTable(node);
			Column col = attrTable.colId();
			
			ctx.currJoin = factory.join(Join.INNER, ctx.currJoin, attrTable, 
					factory.expression(Opcodes.AND, 
							factory.expression(Opcodes.EQ, 
									factory.expression(nodeTable.colId()),
									factory.expression(col)),
									cond));
							
		}
		
		return res;
	}
	
	private void makeEdgeJoin(Edge edge, boolean reverse, StmtContext ctx) {
		if(!ctx.hasBeenProcessed(edge) && !edge.isNegated()) {
			
			TypeStatementFactory factory = ctx.factory;
			GraphTableFactory tableFactory = ctx.tableFactory;
			
			Term cond = null;
			
			debug.entering();
			
			Node[] nodes = new Node[] { 
					(Node) ctx.graph.getSource(edge),
					(Node) ctx.graph.getTarget(edge)					
			};
			
			// If the edge is reverse the selector selects the nodes 
			// backwards. 
			int first = reverse ? 1 : 0;
			int second = 1 - first;
			boolean firstNodeIsSource = !reverse;
			
			Node firstNode = nodes[first];
			Node secondNode = nodes[second];
			EdgeTable edgeTable = tableFactory.edgeTable(edge);
			
			debug.report(NOTE, "join: " + firstNode + ", " + edge + ", " + secondNode);
			
			boolean genFirst = !ctx.hasBeenProcessed(firstNode); 
			
			if(genFirst) {
				ctx.currJoin = tableFactory.nodeTable(firstNode);
				cond = makeNodeJoinCond(firstNode, ctx);
				ctx.markProcessed(firstNode);
			}
			
			// Add condition about the connectivity of the edge to the first node.
			cond = factory.addExpression(Opcodes.AND, cond, 
					factory.expression(Opcodes.EQ, 
							factory.expression(tableFactory.nodeTable(firstNode).colId()),
							factory.expression(edgeTable.colEndId(firstNodeIsSource))));
			
			// Also add conditions restricting the edge type here.
			cond = factory.addExpression(Opcodes.AND, cond,
				factory.isA(edge, tableFactory, typeID));

			// Mark the edge as processed.
			ctx.markProcessed(edge);
			
			// Put the incidence conditions into secondNodeCond
			Term secondNodeCond = factory.expression(Opcodes.EQ,
					factory.expression(tableFactory.nodeTable(secondNode).colId()),
					factory.expression(tableFactory.edgeTable(edge).colEndId(!firstNodeIsSource)));

			boolean addNodeJoin = false;

			// If this node occurrs for the first time.
			if(!ctx.hasBeenProcessed(secondNode)) {
				
				// Add type conditions to incidence conditions
				secondNodeCond = factory.expression(Opcodes.AND, secondNodeCond, 
						makeNodeJoinCond(secondNode, ctx));
						
				ctx.markProcessed(secondNode);

				// Mark, that the join for the second node has to be inserted at last. 
				addNodeJoin = true;
			
			// If the node appeared before.
			} else {
			
				// Put the incidence conditions into the edge join, too.
				cond = factory.addExpression(Opcodes.AND, cond, secondNodeCond); 
			}
			
			// Add the edge join.
			ctx.currJoin = factory.join(Join.INNER, ctx.currJoin, tableFactory.edgeTable(edge), cond);
					

			// If this edge can take conditions, add them now.
			// This implies joining over the edge attribute table.
			Term attrCond = ctx.getCond(edge);
			if(attrCond != null) {
				AttributeTable attrTable = tableFactory.edgeAttrTable(edge);
				attrCond = factory.expression(Opcodes.AND,
					factory.expression(Opcodes.EQ, 
						factory.expression(attrTable.colId()), 
						factory.expression(edgeTable.colId())),
						attrCond);
						
				ctx.currJoin = factory.join(Join.INNER, ctx.currJoin, attrTable, attrCond);
			}
			
			// At last, add the join over the node table, if determined.
			if(addNodeJoin) {
				ctx.currJoin = factory.join(Join.INNER, ctx.currJoin, tableFactory.nodeTable(secondNode), 
					secondNodeCond);
			}


			debug.leaving();
		}
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLGenerator#genMatchStatement(de.unika.ipd.grgen.ir.MatchingAction, java.util.List, java.util.List)
	 */
	protected Query makeMatchStatement(MatchingAction act, List matchedNodes,
			List matchedEdges, GraphTableFactory tableFactory, TypeStatementFactory factory) {

		debug.entering();
		
		Graph graph = act.getPattern();
		SearchPath[] paths = computeSearchPaths(graph);
		
		for(int i = 0; i < paths.length; i++) {
			StringBuffer sb = new StringBuffer();
			paths[i].dump(sb, graph);
			debug.report(NOTE, sb.toString());
		}

		StmtContext stmtCtx = new StmtContext(graph, factory, tableFactory);
		
		// Generate all conditions.
		for(Iterator it = act.getCondition().get(); it.hasNext();) {
			Set usedColumns = new HashSet();
			Expression cond = (Expression) it.next();
			Term term = genExprSQL(cond, factory, tableFactory, usedColumns);
			
			// Add the cond states and the entities which have attributes 
			// to the statement context struct.
			stmtCtx.conds.add(new CondState(term, usedColumns));
			stmtCtx.allCondEntities.addAll(usedColumns);
		}
		
		debug.report(NOTE, "entities with attribs: " + stmtCtx.allCondEntities);
	
		int pathsProcessed = 0;
		int selectedPath = 0;
		boolean[] done = new boolean[paths.length];
		
		// Build joins until all paths are covered
		// This is not streight forward since all paths after the first (which is
		// given) are selected dependent on what has been joined already. In other
		// words: Avoid joining two non-connected paths.
		while(selectedPath >= 0 && paths.length > 0) {
			SearchPath path = paths[selectedPath];
			
			// If a path has no edges, it resulted form a single node
			// (see the Graph class for a definition of a single node).
			// This path is marked ok, since single nodes are processed 
			// later on.
			if(!path.edges.isEmpty()) {
				for(Iterator it = path.edges.iterator(); it.hasNext();) {
					Edge edge = (Edge) it.next();
					makeEdgeJoin(edge, path.isReverse(edge), stmtCtx);
				}
			}			

			// Mark the currently processed path as processed.
			done[selectedPath] = true;
			
			// Unselect the current path to select a new one.
			selectedPath = -1;
			
			// TODO Test unconnected graphs here!
			for(int i = 0; i < paths.length; i++) {
				if(!done[i]) {
					selectedPath = i;
					Edge firstEdge = (Edge) paths[i].edges.get(0);
					Node start = (Node) graph.getSource(firstEdge);
					
					if(stmtCtx.hasBeenProcessed(start))
						break;
				}
			}
		}
		
		// Also add the edges that have not beed considered yet.
		// These edges do not occurr in the DFS tree spanned by visitNode()
		// but their existence, incidence situation and type must be checked.
		// So process them here.
		Collection restEdges = graph.getEdges(new HashSet());
		restEdges.removeAll(stmtCtx.processedEdges);
		
		for(Iterator it = restEdges.iterator(); it.hasNext();) {
			Edge edge = (Edge) it.next();
			makeEdgeJoin(edge, false, stmtCtx);
		}

		Term pendingConds = null;

		// Now, put all single nodes to the query.
		// The single nodes must be the nodes which have not yet been processed.
		Collection singleNodes = graph.getNodes(new HashSet());
		singleNodes.removeAll(stmtCtx.processedNodes);
		for(Iterator it = singleNodes.iterator(); it.hasNext();) {
			Node n = (Node) it.next();
			assert graph.isSingle(n) : "node must be single here!";
			
			// If this the first node at all (no node and no edges have been processed at all)
			// The join degenerates to a table. The conditions are sored in pendingConds and
			// are added to the conditions of next join encountered or to the conditions
			// of the query, if no other follows.
			if(stmtCtx.currJoin == null) {
				stmtCtx.currJoin = tableFactory.nodeTable(n);
				pendingConds = makeNodeJoinCond(n, stmtCtx);
			} else {
				stmtCtx.currJoin = factory.join(Join.INNER, stmtCtx.currJoin, 
					tableFactory.nodeTable(n), factory.addExpression(Opcodes.AND, pendingConds,
						makeNodeJoinCond(n, stmtCtx)));
				
				pendingConds = null;
			}
			
			stmtCtx.markProcessed(n);
		}
		
		// TODO Now we have to process the negated edges.
		for(Iterator it = graph.getNegatedEdges().iterator(); it.hasNext();) {
			Edge edge = (Edge) it.next();
			EdgeTable edgeTable = tableFactory.edgeTable(edge);
			assert stmtCtx.currJoin != null : "There can be no edge without a node";
			
			Node src = graph.getSource(edge);
			Node tgt = graph.getTarget(edge);
			
			// Add condition about the connectivity of the edge to the first node.
			Term cond = factory.expression(Opcodes.EQ, 
				factory.expression(tableFactory.nodeTable(src).colId()),
				factory.expression(edgeTable.colSrcId()));
			
			// Also add conditions restricting the edge type here.
			cond = factory.addExpression(Opcodes.AND, cond,
				factory.isA(edge, tableFactory, typeID));

			// Put the incidence conditions into secondNodeCond
			cond = factory.addExpression(Opcodes.AND, cond,
				factory.expression(Opcodes.EQ,	
					factory.expression(tableFactory.nodeTable(tgt).colId()),
					factory.expression(tableFactory.edgeTable(edge).colTgtId())));

			// Add the condition, that the edge id must be NULL (we use an outer join)
			// in order to represent the negated edge.
			cond = factory.addExpression(Opcodes.AND, cond,
				factory.expression(Opcodes.EQ,
					factory.expression(edgeTable.colId()),
					factory.constantNull()));
			
			// Add the join
			stmtCtx.currJoin = factory.join(Join.RIGHT_OUTER, stmtCtx.currJoin, edgeTable, cond);
		}
		
		// At last add the columns to the query statement.
		List columns = new LinkedList();

		// One for the nodes.
		for(Iterator it = stmtCtx.processedNodes.iterator(); it.hasNext();) {
			Node node = (Node) it.next();
			matchedNodes.add(node);
			columns.add(tableFactory.nodeTable(node).colId());
		}

		// One for the edges.
		for(Iterator it = graph.getEdges(matchedEdges).iterator(); it.hasNext();) {
			Edge edge = (Edge) it.next();
			columns.add(tableFactory.edgeTable(edge).colId());
		}

		// If there were pending conditions make a simple query using these conditions.
		// Else build an explicit query, since all conditions are put in the joins.
		Query result;
		if(pendingConds == null)
			result = factory.explicitQuery(columns, stmtCtx.currJoin);
		else {
			List relations = new LinkedList();
			relations.add(stmtCtx.currJoin);
			result = factory.simpleQuery(columns, relations, pendingConds);
		}

		debug.leaving();
		return result;
	}
}
