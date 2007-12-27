/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * Created on Mar 31, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;
import de.unika.ipd.grgen.be.sql.meta.*;
import de.unika.ipd.grgen.ir.*;
import java.util.*;

import de.unika.ipd.grgen.be.TypeID;
import de.unika.ipd.grgen.be.sql.stmt.AttributeTable;
import de.unika.ipd.grgen.be.sql.stmt.EdgeTable;
import de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory;
import de.unika.ipd.grgen.be.sql.stmt.NodeTable;
import de.unika.ipd.grgen.be.sql.stmt.TypeStatementFactory;


/**
 * A match statement generator using explicit joins.
 */
public class ExplicitJoinGenerator extends SQLGenerator {
	
	private final String[] edgeCols;
	
	public ExplicitJoinGenerator(SQLParameters parameters,
								 MarkerSourceFactory msf,
								 TypeID typeID) {
		
		super(parameters, msf, typeID);
		
		edgeCols = new String[] {
			parameters.getColEdgesSrcId(),
				parameters.getColEdgesTgtId()
		};
	}
	
	private abstract static class GraphItemComparator implements Comparator {
		
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
				//
				
				return res;
			} else
				throw new ClassCastException();
		}
		
		public boolean equals(Object o1) {
			return false;
		}
	};
	
	
	
	private static class SearchPath {
		
		private final List<Edge> edges = new LinkedList<Edge>();
		private final Set<Edge> reverseEdges = new HashSet<Edge>();
		
		private static final String[] heads = new String[] { " -", " <-" };
		private static final String[] tails = new String[] { "-> ", "- " };
		
		public void dumpOld(StringBuffer sb, Graph g) {
			
			if(!edges.isEmpty()) {
				Edge first = edges.get(0);
				Node curr = isReverse(first) ? g.getTarget(first) : g.getSource(first);
				
				sb.append(curr.getIdent());
				
				for(Iterator<Edge> it = edges.iterator(); it.hasNext();) {
					Edge edge = it.next();
					boolean reverse = isReverse(edge);
					int ind = reverse ? 1 : 0;
					Node node = reverse ? g.getSource(edge) : g.getTarget(edge);
					
					sb.append(heads[ind] + edge.getIdent()
								  + tails[ind] + node.getIdent());
				}
			}
		}
		
		public void dump(StringBuffer sb, Graph g) {
			for(Iterator<Edge> it = edges.iterator(); it.hasNext();) {
				Edge edge = it.next();
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
		final Set<Node> visited = new HashSet<Node>();
		final List<ExplicitJoinGenerator.SearchPath> paths = new LinkedList<ExplicitJoinGenerator.SearchPath>();
		
		Graph graph;
		Comparator<Node> comparator;
		
		VisitContext(Graph graph, Comparator<Node> comparator) {
			this.graph = graph;
			this.comparator = comparator;
		}
	}
	
	private void visitNode(VisitContext ctx, SearchPath path, Node start) {
		
		if(ctx.visited.contains(start))
			return;
		
		ctx.visited.add(start);
		
		debug.report(NOTE, "start: " + start);
		
		int index = 0;
		Collection<Node> visited = ctx.visited;
		Graph g = ctx.graph;
		Map<Node, Edge> edges = new HashMap<Node, Edge>();
		List<Node> nodes = new LinkedList<Node>();
		Set<Edge> reverse = new HashSet<Edge>();
		
		for(Edge edge : g.getOutgoing(start)) {
			Node tgt = g.getTarget(edge);
			
			if(!visited.contains(tgt)) {
				nodes.add(tgt);
				edges.put(tgt, edge);
				index++;
			}
		}
		
		for(Edge edge : g.getIncoming(start)) {
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
				
				Edge edge = edges.get(n);
				sp.add(edge, reverse.contains(edge));
				visitNode(ctx, sp, n);
			}
		}
	}
	
	private SearchPath[] computeSearchPaths(Graph pattern) {
		
		Collection<IR> rest = new HashSet<IR>(pattern.getNodes());
		Iterator<Edge> edgeIterator = pattern.getEdges().iterator();
		Comparator<Node> comparator = new NodeComparator(pattern);
		
		debug.report(NOTE, "all nodes" + rest);
		
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
			
			if(path.edges.isEmpty()) ctx.paths.remove(path);
			
			rest.removeAll(ctx.visited);
		} while(!rest.isEmpty());
		
		return (SearchPath[]) ctx.paths.toArray(new SearchPath[ctx.paths.size()]);
	}
	
	/**
	 * Get the node with the most specific type.
	 * @param nodes The node collection to get the most specific from.
	 * @return The node with the most specific node type in <code>nodes</code>.
	 * If the iterator did not contain any nodes <code>null</code> is returned.
	 */
	private Node getCheapest(Iterator<IR> nodes, Comparator<Node> comp) {
		Node cheapest = null;
		
		
		
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
		
		
		
		return cheapest;
	}
	
	/**
	 * An auxillary class to treat conds.
	 */
	class CondState {
		
		Term cond;
		Set<IR> usedEntities;
		
		/**
		 * Make a new cond state.
		 * @param cond The SQL term expressing this conditional expression.
		 * @param usedEntities All entities that are referenced in the cond expression.
		 */
		CondState(Term cond, Set<IR> usedEntities) {
			this.cond = cond;
			this.usedEntities = usedEntities;
			
		}
		
		private boolean canDeliver(Entity ent, Collection<GraphEntity> processed) {
			return usedEntities.contains(ent) && processed.containsAll(usedEntities);
		}
		
		/**
		 * Get the cond expression, if all entites occurring in the expression
		 * are in the processed set.
		 * @param processed The processed set.
		 * @return A SQL term expressing the IR expression, if all entities referenced in
		 * the expression are in the processed set.
		 */
		Term getCond(Entity ent, Collection<GraphEntity> processed) {
			
			
			boolean res = canDeliver(ent, processed);
			debug.report(NOTE, "proc: " + processed + ", used: " + usedEntities
							 + ", can deliver: " + res);
			
			
			return res ? cond : null;
		}
	}
	
	/**
	 * Stuff needed to generate SQL statements in this generator.
	 */
	static class StmtContext {
		Graph graph;
		final TypeStatementFactory factory;
		final GraphTableFactory tableFactory;
		
		
		final Collection<GraphEntity> processedAll = new HashSet<GraphEntity>();
		final Collection<Node> processedNodes = new LinkedList<Node>();
		final Collection<Edge> processedEdges = new LinkedList<Edge>();
		
		/** Holds all entities appearing in cond statements. */
		final Collection<ExplicitJoinGenerator.CondState> conds = new HashSet<ExplicitJoinGenerator.CondState>();
		
		/** All CondStates. */
		final Collection<IR> allCondEntities = new HashSet<IR>();
		
		/** The current join. */
		Relation currJoin = null;
		
		StmtContext(Graph graph, TypeStatementFactory factory, GraphTableFactory tableFactory) {
			this.graph = graph;
			this.factory = factory;
			this.tableFactory = tableFactory;
		}
		
		//TODO this will be obsolete
		StmtContext(Graph graph, TypeStatementFactory factory, GraphTableFactory tableFactory, List<Node> excludeNodes) {
			this.graph = graph;
			this.factory = factory;
			this.tableFactory = tableFactory;
			this.processedNodes.addAll(excludeNodes);
			this.processedAll.addAll(excludeNodes);
		}
		
		void setGraph(Graph g) {
			graph = g;
		}
		
		boolean hasAttribute(Entity ent) {
			return allCondEntities.contains(ent);
		}
		
		Term getCond(Entity ent) {
			for(Iterator<ExplicitJoinGenerator.CondState> it = conds.iterator(); it.hasNext();) {
				CondState cs = it.next();
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
	
	/**
	 * Make the condition term that is to be added to a node join.
	 * @param node The node.
	 * @param ctx The statement generation context.
	 * @return The condition term.
	 */
	private Term makeNodeJoinCond(Node node, StmtContext ctx) {
		TypeStatementFactory factory = ctx.factory;
		GraphTableFactory tableFactory = ctx.tableFactory;
		
		NodeTable nodeTable = tableFactory.nodeTable(node);
		
		// Make type constraints
		Term res = factory.isA(nodeTable, node, true, typeID);
		
		// Make the clauses guaranteeing injectiveness
		for(Iterator<Node> it = ctx.processedNodes.iterator(); it.hasNext();) {
			Node curr = it.next();
			NodeTable currTable = tableFactory.nodeTable(curr);
			
			if (ctx.graph instanceof PatternGraph) {
				PatternGraph pattern = (PatternGraph) ctx.graph;

				if(!pattern.isHomomorphic(node, curr))
					res = factory.addExpression(Opcodes.AND, res,
												factory.expression(Opcodes.NE, factory.expression(nodeTable.colId()),
																   factory.expression(currTable.colId())));
			}
		}
		
		return res;
	}
	
	/**
	 * Add the node attribute table join to the current statement if neccessary.
	 * @param node The node.
	 * @param ctx The statement generation context.
	 */
	private void addNodeAttrJoin(Node node, StmtContext ctx, boolean doNegJoin) {
		StatementFactory factory = ctx.factory;
		NodeTable nodeTable = ctx.tableFactory.nodeTable(node);
		int joinType = doNegJoin ? Join.LEFT_OUTER : Join.INNER;
		
		// Make probable attributes
		Term cond = ctx.getCond(node);
		
		if(cond != null) {
			AttributeTable attrTable = ctx.tableFactory.nodeAttrTable(node);
			Column col = attrTable.colId();
			
			ctx.currJoin = factory.join(joinType, ctx.currJoin, attrTable,
										factory.expression(Opcodes.AND,
														   factory.expression(Opcodes.EQ,
																			  factory.expression(nodeTable.colId()),
																			  factory.expression(col)),
														   cond));
		}
	}
	
	/**
	 * Make the neccessary joins for an edge.
	 * This method additionally constructs the joins neccessary for nodes.
	 * @param edge The edge.
	 * @param reverse true, if the edge's direction is reversed.
	 * @param ctx The statement generation context.
	 */
	private void makeEdgeJoin(Edge edge, boolean reverse, StmtContext ctx, boolean doNegJoin) {
		if(!ctx.hasBeenProcessed(edge)) {
			int joinType = doNegJoin ? Join.LEFT_OUTER : Join.INNER;
			
			TypeStatementFactory factory = ctx.factory;
			GraphTableFactory tableFactory = ctx.tableFactory;
			
			Term cond = null;
			
			
			
			Node[] nodes = new Node[] {
				ctx.graph.getSource(edge),
					ctx.graph.getTarget(edge)
			};
			
			// If the edge is reverse the selector selects the nodes backwards.
			int first = reverse ? 1 : 0;
			int second = 1 - first;
			boolean firstNodeIsSource = !reverse;
			
			Node firstNode = nodes[first];
			Node secondNode = nodes[second];
			EdgeTable edgeTable = tableFactory.edgeTable(edge);
			
			debug.report(NOTE, "join: " + firstNode + ", " + edge + ", " + secondNode);
			
			boolean genFirst = !ctx.hasBeenProcessed(firstNode);
			
			if(genFirst && ctx.currJoin == null) {
				ctx.currJoin = tableFactory.nodeTable(firstNode);
				cond = makeNodeJoinCond(firstNode, ctx);
				addNodeAttrJoin(firstNode, ctx, doNegJoin);
				ctx.markProcessed(firstNode);
			}
			
			// Add condition about the connectivity of the edge to the first node.
			cond = factory.addExpression(Opcodes.AND, cond,
										 factory.expression(Opcodes.EQ,
															factory.expression(tableFactory.nodeTable(firstNode).colId()),
															factory.expression(edgeTable.colEndId(firstNodeIsSource))));
			
			// Also add conditions restricting the edge type here.
			cond = factory.addExpression(Opcodes.AND, cond,
										 factory.isA(edgeTable, edge, false, typeID));
			
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
			ctx.currJoin = factory.join(joinType, ctx.currJoin, tableFactory.edgeTable(edge), cond);
			
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
				
				ctx.currJoin = factory.join(joinType, ctx.currJoin, attrTable, attrCond);
			}
			
			// At last, add the join over the node table, if determined.
			if(addNodeJoin) {
				ctx.currJoin = factory.join(joinType, ctx.currJoin, tableFactory.nodeTable(secondNode),
											secondNodeCond);
				addNodeAttrJoin(secondNode, ctx, doNegJoin);
			}
			
			
			
		}
	}
	
	protected Query makeMatchStatement(MatchCtx ctx) {
		
		MatchingAction act = ctx.action;
		TypeStatementFactory factory = ctx.factory;
		GraphTableFactory tableFactory = ctx.factory;
		List<IR> matchedNodes = ctx.matchedNodes;
		List<IR> matchedEdges = ctx.matchedEdges;
		
		//Build a list with all graphs of this matching action
		LinkedList<Graph> graphs = new LinkedList<Graph>();
		graphs.addFirst(act.getPattern());
		graphs.addAll(act.getNegs());
		
		Term pendingConds = null;
		List<Column> columns = new LinkedList<Column>();
		StmtContext stmtCtx = new StmtContext(act.getPattern(), factory, tableFactory);
		
		// Generate all conditions.
		for(Iterator<Expression> it = act.getPattern().getConditions().iterator(); it.hasNext();) {
			Set<IR> usedColumns = new HashSet<IR>();
			Expression cond = it.next();
			
			// Note that the genExprSQL method records all entities appearing as owners
			// in qualification (see ir.Qualification) expressions in the usedColumns set.
			Term term = genExprSQL(cond, ctx.factory, usedColumns);
			
			// Add the cond states and the entities which have attributes
			// to the statement context struct.
			stmtCtx.conds.add(new CondState(term, usedColumns));
			
			debug.report(NOTE, "cond state with: " + usedColumns);
			
			// The allCondEntities set is used for checking if an entity occurrs in a
			// condition term
			stmtCtx.allCondEntities.addAll(usedColumns);
		}
		debug.report(NOTE, "entities with attribs: " + stmtCtx.allCondEntities);
		
		Collection<IR> restEdges = new HashSet<IR>();
		Collection<IR> singleNodes = new HashSet<IR>();
		
		//Iterate over all these graphs and generate the statement
		for (Iterator<Graph> iter = graphs.iterator(); iter.hasNext();) {
			Graph graph = iter.next();
			boolean graphIsNAC = (graph != graphs.getFirst()) ? true : false;
			int joinType = graphIsNAC ? Join.LEFT_OUTER : Join.INNER;
			
			stmtCtx.setGraph(graph);
			
			SearchPath[] paths = computeSearchPaths(graph);
			
			for(int i = 0; i < paths.length; i++) {
				StringBuffer sb = new StringBuffer();
				paths[i].dump(sb, graph);
				debug.report(NOTE, "path " + i);
				debug.report(NOTE, sb.toString());
			}
			
			//int pathsProcessed = 0;
			int selectedPath = 0;
			boolean[] done = new boolean[paths.length];
			
			// Build joins until all paths are covered
			// This is not streight forward since all paths after the first (which is
			// given) are selected dependent on what has been joined already. In other
			// words: Avoid joining two non-connected paths.
			while(selectedPath >= 0 && paths.length > 0) {
				SearchPath path = paths[selectedPath];
				assert !path.edges.isEmpty() : "path must contain an element";
				
				for(Iterator<Edge> it = path.edges.iterator(); it.hasNext();) {
					Edge edge = it.next();
					makeEdgeJoin(edge, path.isReverse(edge), stmtCtx, graphIsNAC);
				}
				
				// Mark the currently processed path as processed.
				done[selectedPath] = true;
				
				// Unselect the current path to select a new one.
				selectedPath = -1;
				
				for(int i = 0; i < paths.length; i++) {
					if(!done[i] ) {
						assert !paths[i].edges.isEmpty() : "path must contain an element";
						selectedPath = i;
						Edge firstEdge = paths[i].edges.get(0);
						Node start = graph.getSource(firstEdge);
						
						if(stmtCtx.hasBeenProcessed(start))
							break;
					}
				}
			}
			
			// Also add the edges that have not beed considered yet.
			// These edges do not occurr in the DFS tree spanned by visitNode()
			// but their existence, incidence situation and type must be checked.
			// So process them here.
			restEdges.clear();
			restEdges.addAll(graph.getEdges());
			restEdges.removeAll(stmtCtx.processedEdges);
			
			for(Iterator<IR> it = restEdges.iterator(); it.hasNext();) {
				Edge edge = (Edge) it.next();
				makeEdgeJoin(edge, false, stmtCtx, graphIsNAC);
			}
			
			
			// Now, put all single nodes to the query.
			// The single nodes must be the nodes which have not yet been processed.
			singleNodes.clear();
			singleNodes.addAll(graph.getNodes());
			singleNodes.removeAll(stmtCtx.processedNodes);
			
			for(Iterator<IR> it = singleNodes.iterator(); it.hasNext();) {
				Node n = (Node) it.next();
				//TODO or not TODO! assert graph.isSingle(n) : "node must be single here!";
				
				// If this the first node at all (no node and no edges have been processed at all)
				// The join degenerates to a table. The conditions are stored in pendingConds and
				// are added to the conditions of next join encountered or to the conditions
				// of the query, if no other follows.
				if(stmtCtx.currJoin == null) {
					stmtCtx.currJoin = tableFactory.nodeTable(n);
					pendingConds = makeNodeJoinCond(n, stmtCtx);
				} else {
					stmtCtx.currJoin = factory.join(joinType, stmtCtx.currJoin,
													tableFactory.nodeTable(n), factory.addExpression(Opcodes.AND, pendingConds,
																									 makeNodeJoinCond(n, stmtCtx)));
					
					pendingConds = null;
				}
				
				stmtCtx.markProcessed(n);
			}
			
			// If the current graph is the pattern (and not a NAC) of the action
			// add the columns to the query statement.
			if (!graphIsNAC) {
				// One for the nodes.
				for(Iterator<Node> it = stmtCtx.processedNodes.iterator(); it.hasNext();) {
					Node node = it.next();
					matchedNodes.add(node);
					columns.add(tableFactory.nodeTable(node).colId());
				}
				
				matchedEdges.clear();
				matchedEdges.addAll(graph.getEdges());
				
				// One for the edges.
				for(Iterator<IR> it = matchedEdges.iterator(); it.hasNext();) {
					Edge edge = (Edge) it.next();
					columns.add(tableFactory.edgeTable(edge).colId());
				}
			}
			
		}
		
		//Generate all "x == null" conditions of graph elements used in the sets   N_i - l(L)
		Term nacConds = getNacConds(act, factory, tableFactory);
		
		if (nacConds != null) {
			if (pendingConds == null)
				pendingConds = nacConds;
			else
				pendingConds = factory.addExpression(Opcodes.AND, pendingConds, nacConds);
		}
		
		// If there were pending conditions make a simple query using these conditions.
		// Else build an explicit query, since all conditions are put in the joins.
		Query result;
		if(pendingConds == null)
			result = factory.explicitQuery(true, columns, stmtCtx.currJoin, null, null, StatementFactory.NO_LIMIT);
		else {
			List<Relation> relations = new LinkedList<Relation>();
			relations.add(stmtCtx.currJoin);
			result = factory.simpleQuery(columns, relations, pendingConds,
										 StatementFactory.NO_LIMIT);
		}
		
		
		
		return result;
	}
	
	private Term getNacConds(MatchingAction act, TypeStatementFactory factory, GraphTableFactory tableFactory) {
		//The nodes and edges of the pattern part
		Collection<Node> patNodes = act.getPattern().getNodes();
		Collection<Edge> patEdges = act.getPattern().getEdges();
		Collection<IR> negNodes = new HashSet<IR>();
		Collection<IR> negEdges = new HashSet<IR>();
		
		//For all negative parts
		Term nacCond = null;
		for (Graph neg : act.getNegs()) {
			//Get the elements to generate for
			negNodes.clear();
			negNodes.addAll(neg.getNodes());
			negNodes.removeAll(patNodes);
			
			negEdges.clear();
			negEdges.addAll(neg.getEdges());
			negEdges.removeAll(patEdges);
			
			//Now generate the subterm for one negative part
			Term sub = null;
			//for all nodes
			for (Iterator<IR> iter = negNodes.iterator(); iter.hasNext(); )	{
				Term eqNull = factory.expression(Opcodes.EQ,
												 factory.expression(tableFactory.nodeTable((Node) iter.next()).colId()),
												 factory.constantNull());
				if (sub == null)
					sub = eqNull;
				else
					sub = factory.addExpression(Opcodes.OR, sub, eqNull);
			}
			//for all edges
			for (Iterator<IR> iter = negEdges.iterator(); iter.hasNext(); )	{
				Term eqNull = factory.expression(Opcodes.EQ,
												 factory.expression(tableFactory.edgeTable((Edge) iter.next()).colId()),
												 factory.constantNull());
				if (sub == null)
					sub = eqNull;
				else
					sub = factory.addExpression(Opcodes.OR, sub, eqNull);
			}
			
			//Now add the subterm for one negative part to the complete termm
			if (nacCond == null)
				nacCond = sub;
			else
				nacCond = factory.addExpression(Opcodes.AND, nacCond, sub);
		}
		return nacCond;
	}
	
}
