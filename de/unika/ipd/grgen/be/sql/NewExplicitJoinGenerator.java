/**
 * Created on May 13, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import de.unika.ipd.grgen.be.sql.meta.*;
import de.unika.ipd.grgen.ir.*;
import java.util.*;

import de.unika.ipd.grgen.be.sql.stmt.AttributeTable;
import de.unika.ipd.grgen.be.sql.stmt.EdgeTable;
import de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory;
import de.unika.ipd.grgen.be.sql.stmt.NodeTable;
import de.unika.ipd.grgen.be.sql.stmt.TypeStatementFactory;


/**
 * A match statement generator using explicit joins.
 */
public class NewExplicitJoinGenerator extends SQLGenerator {
	
	private final String[] edgeCols;
	
	/**
	 * @param parameters
	 * @param constraint
	 */
	public NewExplicitJoinGenerator(SQLParameters parameters, TypeID typeID) {
		
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
		private final Map graphMap = new HashMap();
		
		public void dump(StringBuffer sb) {
			for(Iterator it = edges.iterator(); it.hasNext();) {
				Edge edge = (Edge) it.next();
				Graph g = getGraph(edge);
				Node src = g.getSource(edge);
				Node tgt = g.getTarget(edge);
				boolean reverse = isReverse(edge);
				
				sb.append(src.getIdent() + " " + edge.getIdent() + " " + tgt.getIdent()
										+ " reverse: " + reverse + "\n");
				
			}
		}
		
		public void add(Edge edge, boolean reverse, Graph graph) {
			graphMap.put(edge, graph);
			edges.add(edge);
			if(reverse)
				reverseEdges.add(edge);
		}
		
		public boolean isReverse(Edge edge) {
			return reverseEdges.contains(edge);
		}
		
		public Graph getGraph(Edge edge) {
			return (Graph) graphMap.get(edge);
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
				sp.add(edge, reverse.contains(edge), g);
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
	class CondState {
		
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
			debug.entering();
			
			boolean res = canDeliver(ent, processed);
			debug.report(NOTE, "proc: " + processed +
										 ", used: " + usedEntities + ", can deliver: " + res);
			debug.leaving();
			
			return res ? cond : null;
		}
	}
	
	protected Query makeMatchStatement(MatchingAction act, List matchedNodes, List matchedEdges,
																		 GraphTableFactory tableFactory, TypeStatementFactory factory) {
		List columns = new LinkedList();
		
		debug.entering();
		
		// Build a list with all graphs of this matching action
		LinkedList graphs = new LinkedList();
		graphs.addFirst(act.getPattern());
		for (Iterator iter = act.getNegs(); iter.hasNext(); ) {
			graphs.addLast(iter.next());
		}
		
		JoinSequence seq = new JoinSequence(act, factory, tableFactory);
		
		// Iterate over all these graphs and generate the statement
		for (Iterator iter = graphs.iterator(); iter.hasNext();) {
			Graph graph = (Graph) iter.next();
			boolean graphIsNAC = graph != graphs.getFirst();
			int joinType = graphIsNAC ? Join.LEFT_OUTER : Join.INNER;
			
			SearchPath[] paths = computeSearchPaths(graph);

		
			for(int i = 0; i < paths.length; i++) {
				StringBuffer sb = new StringBuffer();
				paths[i].dump(sb);
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
				
				seq.addPath(path);
				
				// Mark the currently processed path as processed.
				done[selectedPath] = true;
				
				// Unselect the current path to select a new one.
				selectedPath = -1;
				
				for(int i = 0; i < paths.length; i++) {
					if(!done[i] ) {
						assert !paths[i].edges.isEmpty() : "path must contain an element";
						selectedPath = i;
						Edge firstEdge = (Edge) paths[i].edges.get(0);
						Node start = graph.getSource(firstEdge);
						
						if(seq.hasBeenProcessed(start))
							break;
					}
				}
			}
			
			// Also add the edges that have not beed considered yet.
			// These edges do not occurr in the DFS tree spanned by visitNode()
			// but their existence, incidence situation and type must be checked.
			// So process them here.
			Collection restEdges = graph.getEdges(new HashSet());
			restEdges.removeAll(seq.getProcessed());
			
			for(Iterator it = restEdges.iterator(); it.hasNext();) {
				Edge edge = (Edge) it.next();
				seq.addEdgeJoin(graph, edge, false, joinType);
			}
			
			
			// Now, put all single nodes to the query.
			// The single nodes must be the nodes which have not yet been processed.
			Collection singleNodes = graph.getNodes(new HashSet());
			singleNodes.removeAll(seq.getProcessed());
			
			for(Iterator it = singleNodes.iterator(); it.hasNext();) {
				Node n = (Node) it.next();
				assert graph.isSingle(n) : "node must be single here!";
				
				seq.addNodeJoin(n, joinType);
			}
			
			// If the current graph is the pattern (and not a NAC) of the action
			// add the columns to the query statement.
			if (!graphIsNAC) {
				Collection proc = seq.getProcessed();
				// One for the nodes.
				for(Iterator it = proc.iterator(); it.hasNext();) {
					Object obj = it.next();
					if(obj instanceof Node) {
						Node node = (Node) obj;
						matchedNodes.add(node);
						columns.add(tableFactory.nodeTable(node).colId());
					}
				}
				
				// One for the edges.
				for(Iterator it = graph.getEdges(matchedEdges).iterator(); it.hasNext();) {
					Object obj = it.next();
					if(obj instanceof Node) {
						Edge edge = (Edge) obj;
						columns.add(tableFactory.edgeTable(edge).colId());
					}
				}
			}
		}
		
		// Generate all "x = NULL" conditions of graph elements
		// used in the sets   N_i - l(L)
		Term nacConds = getNacConds(act, factory, tableFactory);
		
		// TODO Implement right.
		//seq.scheduleCond(nacConds, new LinkedList());
		
		// If there were pending conditions make a simple query using these conditions.
		// Else build an explicit query, since all conditions are put in the joins.
		Query result = seq.produceQuery();
		
		debug.leaving();
		
		return result;
	}
	
	private Term getNacConds(MatchingAction act, TypeStatementFactory factory, GraphTableFactory tableFactory) {
		//The nodes and edges of the pattern part
		Collection patNodes = new HashSet();
		Collection patEdges = new HashSet();
		act.getPattern().getNodes(patNodes);
		act.getPattern().getEdges(patEdges);
		
		//For all negative parts
		Term nacCond = null;
		for (Iterator it = act.getNegs(); it.hasNext(); ) {
			Graph neg = (Graph) it.next();
			
			//Get the elements to generate for
			Collection negNodes = new HashSet();
			neg.getNodes(negNodes);
			negNodes.removeAll(patNodes);
			
			Collection negEdges = new HashSet();
			neg.getEdges(negEdges);
			negEdges.removeAll(patEdges);
			
			//Now generate the subterm for one negative part
			Term sub = null;
			//for all nodes
			for (Iterator iter = negNodes.iterator(); iter.hasNext(); )	{
				Term eqNull = factory.expression(Opcodes.EQ,
																				 factory.expression(tableFactory.nodeTable((Node) iter.next()).colId()),
																				 factory.constantNull());
				if (sub == null)
					sub = eqNull;
				else
					sub = factory.addExpression(Opcodes.OR, sub, eqNull);
			}
			//for all edges
			for (Iterator iter = negEdges.iterator(); iter.hasNext(); )	{
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
	
	private class JoinSequence {
		
		private final Map conditions = new HashMap();
		private final Map processed = new HashMap();
		private final List entities = new LinkedList();
		private final List joins = new LinkedList();
		private final Set occurInCond = new HashSet();
		private final Map joinCondDeps = new HashMap();
		
		private final TypeStatementFactory factory;
		private final GraphTableFactory tableFactory;
		private final Term defCond;
		private final MatchingAction act;
		
		private Relation currJoin = null;
		private int currId = 0;
		
		JoinSequence(MatchingAction act, TypeStatementFactory factory,
								 GraphTableFactory tableFactory) {
			
			this.act = act;
			this.factory = factory;
			this.tableFactory = tableFactory;
			this.defCond = factory.constant(true);
			
			buildConds();
		}
		
		/**
		 * Build the SQL terms for the condition section of the action.
		 * This method fills the <code>cinditions</code> map, mapping
		 * the produced SQL term to collection of the entities occurring
		 * in the term.
		 * Furthermore, the <code>occurInCond</code> set is filled
		 * with all entities that have conditions in the cond part.
		 */
		private void buildConds() {
			
			for(Iterator it = act.getCondition().get(); it.hasNext();) {
				List usedEntities = new LinkedList();
				
				Expression expr = (Expression) it.next();
				Term term = genExprSQL(expr, factory, tableFactory, usedEntities);
				
				scheduleCond(term, usedEntities);
				occurInCond.addAll(usedEntities);
			}
		}
		
		/**
		 * Check if an entity has been added to the join
		 * sequence.
		 * @param ent The entity to check for.
		 * @return true, if the entity has already been added
		 * to the join sequence, false if not.
		 */
		private boolean hasBeenProcessed(Entity ent) {
			return processed.containsKey(ent);
		}
		
		/**
		 * Mark an entity as processed.
		 * @param ent The entity.
		 */
		private void markProcessed(Entity ent) {
			processed.put(ent, new Integer(currId++));
		}
		
		/**
		 * Get all processed entities.
		 * @return A collection containing all processed entities.
		 */
		private Collection getProcessed() {
			return processed.keySet();
		}
		
		/**
		 * Get the id of an entity.
		 * Being marked processed, an entity gets an id. These
		 * ids start at 0 and are handed out sequentially since
		 * they are used in bit sets later on.
		 */
		private int getId(Entity ent) {
			assert processed.containsKey(ent) : "entity must have been processed yet";
			return ((Integer) processed.get(ent)).intValue();
		}
		
		private int getJoinMethod(Graph g) {
			// TODO Implement this correctly
			return Join.INNER;
		}
	
		/**
		 * Add a condition term to the join condition dependency map.
		 *
		 * A condition tree may depend on several entities. Since
		 * the condition can only be evaluated when all the entities
		 * it depends on have already been added to the join
		 * sequence.
		 * Therefore each condition is mapped to a bit set which flags
		 * all entities this condition depends on. (The bit is the
		 * id assigned by {@link #markProcessed(Entity)}.
		 *
		 * @note This method is for internal use only.
		 * @param cond The condition tree.
		 * @param ents All ents the condition depends on.
		 */
		private void addCondDep(Term cond, Collection ents) {
			BitSet deps = new BitSet();
			
			for(Iterator it = ents.iterator(); it.hasNext();) {
				Entity ent = (Entity) it.next();
				deps.set(getId(ent));
			}
			
			joinCondDeps.put(cond, deps);
		}

		/**
		 * Add a condition tree to the list of scheduled conditions.
		 * As a parameter you have to pass a collection of all
		 * entities the condition depends on.
		 *
		 */
		public void scheduleCond(Term cond, Collection ents) {
			if(conditions.containsKey(cond)) {
				Collection deps = (Collection) conditions.get(cond);
				deps.addAll(ents);
			} else
				conditions.put(cond, ents);
		}
		
		public void scheduleCond(Term cond, Entity ent) {
			if(conditions.containsKey(cond)) {
				Collection deps = (Collection) conditions.get(cond);
				deps.add(ent);
			} else {
				Collection deps = new HashSet();
				deps.add(ent);
				conditions.put(cond, deps);
			}
		}

		/**
		 * Deliver a condition tree.
		 *
		 * This method checks which condition is ready to be included
		 * as a join condition. Thus, it checks, if all entities given
		 * by the dependence set of the condition are contained in
		 * the set <code>processed</code>.
		 * The terms which fulfil this condition are ANDed together,
		 * deleted from the <code>joinCondDeps</code> map and
		 * returned.
		 *
		 * @param processed The set which contains all entities that
		 * are available in the join sequence at this moment.
		 * @return The condition term to be deployed.
		 */
		private Term deliverConditions(BitSet processed) {
			Term res = null;
			BitSet work = new BitSet(currId);
			
			System.out.println("processed: " + processed);
			for(Iterator it = joinCondDeps.keySet().iterator(); it.hasNext();) {
				Term term = (Term) it.next();
				BitSet dep = (BitSet) joinCondDeps.get(term);

				System.out.print("  dep: " + dep);
				
				work.clear();
				work.or(dep);
				work.and(processed);
				work.xor(dep);
				
				System.out.println(" -> " + work);
				if(work.nextSetBit(0) == -1) {
					res = factory.addExpression(Opcodes.AND, res, term);
					it.remove();
				}
			}
			
			return res;
		}
		
		/**
		 * Make the condition term that is to be added to a node join.
		 * @param node The node.
		 */
		private void addNodeJoinCond(Node node) {
			Collection dep = new LinkedList();
			NodeTable nodeTable = tableFactory.nodeTable(node);
			
			dep.add(node);
			// Make type constraints
			Term res = factory.isA(nodeTable, node.getNodeType(), typeID);
			
			// Make the clauses guaranteeing injectiveness
			for(Iterator it = processed.keySet().iterator(); it.hasNext();) {
				Object obj = it.next();
				if(obj instanceof Node) {
					Node curr = (Node) obj;
					NodeTable currTable = tableFactory.nodeTable(curr);
					
					if(!node.isHomomorphic(curr)) {
						res = factory.addExpression(Opcodes.AND, res,
																				factory.expression(Opcodes.NE,
																													 factory.expression(nodeTable.colId()),
																													 factory.expression(currTable.colId())));
						dep.add(curr);
					}
				}
			}

			scheduleCond(res, dep);
		}
		
		
		private void addEdgeJoinCond(Graph g, Edge edge, boolean swapped) {
			Node firstNode = g.getEnd(edge, !swapped);
			Node secondNode = g.getEnd(edge, swapped);
			
			EdgeTable table = tableFactory.edgeTable(edge);
			NodeTable firstTable = tableFactory.nodeTable(firstNode);
			NodeTable secondTable = tableFactory.nodeTable(secondNode);
			
			// Add condition about the connectivity of the edge to the first node.
			Term srcCond =
				factory.expression(Opcodes.EQ,
													 factory.expression(firstTable.colId()),
													 factory.expression(table.colEndId(!swapped)));
			
			// Also add conditions restricting the edge type here.
			Term typeCond = factory.isA(table, edge.getEdgeType(), typeID);
			
			// Put the incidence conditions into secondNodeCond
			Term tgtCond = factory.expression(Opcodes.EQ,
																				factory.expression(secondTable.colId()),
																				factory.expression(table.colEndId(swapped)));
			
			scheduleCond(typeCond, edge);
			scheduleCond(srcCond, firstNode);
			scheduleCond(srcCond, edge);
			scheduleCond(tgtCond, secondNode);
			scheduleCond(tgtCond, edge);
		}
		
		private void addJoin(Entity ent, Relation rel, int kind) {
			assert !(currJoin == null) || (rel instanceof Table)
				: "if this is the first join, the relation must be a table";

			Relation left = currJoin != null ? currJoin
				: tableFactory.neutralTable();
			
			currJoin = factory.join(kind, left, rel, defCond);
			
			joins.add(currJoin);
			entities.add(ent);
		}
		
		private void addNodeJoin(Node node, int joinMethod) {
			if(!hasBeenProcessed(node)) {
				addJoin(node, tableFactory.nodeTable(node), joinMethod);
				addNodeJoinCond(node);
				
				if(occurInCond.contains(node)) {
					addJoin(node, tableFactory.nodeAttrTable(node), joinMethod);
				}
				
				markProcessed(node);
			}
		}
		
		private void addEdgeJoin(Graph g, Edge edge, boolean swapped, int joinMethod) {
			if(!hasBeenProcessed(edge)) {
				addJoin(edge, tableFactory.edgeTable(edge), joinMethod);
				addEdgeJoinCond(g, edge, swapped);
				if(occurInCond.contains(edge)) {
					addJoin(edge, tableFactory.edgeAttrTable(edge), joinMethod);
				}
				
				markProcessed(edge);
			}
		}
		
		/**
		 * Add a search path to the join sequence.
		 * @param sp The search path,
		 */
		void addPath(SearchPath sp) {
			for(Iterator it = sp.edges.iterator(); it.hasNext(); ) {
				Edge edge = (Edge) it.next();
				Graph g = sp.getGraph(edge);
				int joinMethod = getJoinMethod(g);
				
				// If the edge is reverse the selector selects the nodes
				// backwards.
				boolean reverse = sp.isReverse(edge);
				Node firstNode = g.getEnd(edge, !reverse);
				Node secondNode = g.getEnd(edge, reverse);
				
				addNodeJoin(firstNode, joinMethod);
				addEdgeJoin(g, edge, reverse, joinMethod);
				addNodeJoin(secondNode, joinMethod);
			}
		}
		
		Query produceQuery() {
			
			debug.entering();
			
			// First, add all terms from the cond part of the rule to
			// the voind cond dependency map.
			for(Iterator it = conditions.keySet().iterator(); it.hasNext();) {
				Term term = (Term) it.next();
				Collection ents = (Collection) conditions.get(term);
				addCondDep(term, ents);
			}
			
			System.out.println("processed: " + processed);
			System.out.println("deps: " + joinCondDeps);

			// Add all scheduled conditions to their
			// respective joins.
			BitSet processedEntities = new BitSet(currId);
			
			for(Iterator it = entities.iterator(), jt = joins.iterator();
					it.hasNext() && jt.hasNext();) {
				
				Entity ent = (Entity) it.next();
				Object obj = jt.next();

				processedEntities.set(getId(ent));
				Term conds = deliverConditions(processedEntities);
				
				System.out.println("current: " + processedEntities);
				System.out.println("conds: " + (conds != null ? true : false));
				
				if(conds != null) {
					assert obj instanceof Join : "There must be a join";
					Join join = (Join) obj;
					join.setCondition(conds);
				}
				
			}
			
			// Build the column list
			Collection proc = processed.keySet();
			List columns = new LinkedList();
			
			// One for the nodes.
			for(Iterator it = proc.iterator(); it.hasNext();) {
				Object obj = it.next();
				if(obj instanceof Node) {
					Node node = (Node) obj;
					columns.add(tableFactory.nodeTable(node).colId());
				}
			}
			
			// One for the edges.
			for(Iterator it = proc.iterator(); it.hasNext();) {
				Object obj = it.next();
				if(obj instanceof Edge) {
					Edge edge = (Edge) obj;
					columns.add(tableFactory.edgeTable(edge).colId());
				}
			}

			assert processed.size() > 1 : "Small queries not yet supported";
			Query result = factory.explicitQuery(columns, currJoin);
			
			debug.leaving();
			
			return result;
		}

	}
}

