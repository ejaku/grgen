/**
 * Created on May 13, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import de.unika.ipd.grgen.be.sql.meta.*;
import de.unika.ipd.grgen.be.sql.stmt.*;
import de.unika.ipd.grgen.ir.*;
import java.util.*;


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
				
				int res = compareTypes(n1.getNodeType(), n2.getNodeType());
				
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
		private final boolean isNAC;
		
		public SearchPath(boolean isNAC) {
			this.isNAC = isNAC;
		}
		
		public boolean isNAC() {
			return isNAC;
		}
		
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
	
	private void visitNode(VisitContext ctx, SearchPath path,
												 Node start, boolean isNAC) {
		
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
					sp = new SearchPath(isNAC);
					ctx.paths.add(sp);
				}
				
				Edge edge = (Edge) edges.get(n);
				sp.add(edge, reverse.contains(edge), g);
				visitNode(ctx, sp, n, isNAC);
			}
		}
		
		debug.leaving();
	}
	
	private SearchPath[] computeSearchPaths(Graph pattern, boolean graphIsNAC) {
		
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
			SearchPath path = new SearchPath(graphIsNAC);
			ctx.paths.add(path);
			
			debug.report(NOTE, "cheapest: " + cheapest);
			
			visitNode(ctx, path, cheapest, graphIsNAC);
			
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
	
	protected Query makeMatchStatement(MatchingAction act, List matchedNodes, List matchedEdges,
																		 GraphTableFactory tableFactory, TypeStatementFactory factory) {
		debug.entering();
		
		Map neutralMap = new HashMap();
		
		// Build a list with all graphs of this matching action
		LinkedList graphs = new LinkedList();
		graphs.addFirst(act.getPattern());
		for (Iterator iter = act.getNegs(); iter.hasNext(); ) {
			graphs.addLast(iter.next());
		}
		
		JoinSequence seq = new JoinSequence(act, factory, tableFactory,
																				matchedNodes, matchedEdges);
		
		int graphNumber = 0;
		
		// Iterate over all these graphs and generate the statement
		for (Iterator iter = graphs.iterator(); iter.hasNext(); graphNumber++) {
			Graph graph = (Graph) iter.next();
			boolean graphIsNAC = graph != graphs.getFirst();
			int joinType = graphIsNAC ? Join.LEFT_OUTER : Join.INNER;
			
			SearchPath[] paths = computeSearchPaths(graph, graphIsNAC);
			
			
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
				
				seq.addPath(path, joinType);
				
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
			restEdges.removeAll(seq.getProcessedEntities());
			
			for(Iterator it = restEdges.iterator(); it.hasNext();) {
				Edge edge = (Edge) it.next();
				seq.addEdgeJoin(graph, edge, false, joinType, graphIsNAC);
			}
			
			
			// Now, put all single nodes to the query.
			// The single nodes must be the nodes which have not yet been processed.
			Collection singleNodes = graph.getNodes(new HashSet());
			singleNodes.removeAll(seq.getProcessedEntities());
			
			for(Iterator it = singleNodes.iterator(); it.hasNext();) {
				Node n = (Node) it.next();
				seq.addNodeJoin(n, joinType, graphIsNAC);
			}
			
			if(graphIsNAC) {
				Table neutral = tableFactory.neutralTable("neutral" + graphNumber);
				neutralMap.put(graph, neutral);
				seq.addJoin(neutral, Join.INNER);
			}
		}
		
		// Generate all "x = NULL" conditions of graph elements
		// used in the sets   N_i - l(L)
		addNacConds(act, neutralMap, factory, tableFactory, seq);
		
		// If there were pending conditions make a simple query using these conditions.
		// Else build an explicit query, since all conditions are put in the joins.
		Query result = seq.produceQuery();
		
		debug.leaving();
		
		return result;
	}
	
	private void addNacConds(MatchingAction act, Map neutralMap,
													 TypeStatementFactory factory,
													 GraphTableFactory tableFactory, JoinSequence seq) {
		
		// The nodes and edges of the pattern part
		Collection patNodes = act.getPattern().getNodes(new HashSet());
		Collection patEdges = act.getPattern().getEdges(new HashSet());
		
		int graphNum = 0;
		
		// For all negative parts
		for (Iterator it = act.getNegs(); it.hasNext(); graphNum++) {
			Graph neg = (Graph) it.next();
			
			// Get the elements to generate for
			Collection negNodes = new HashSet();
			neg.getNodes(negNodes);
			negNodes.removeAll(patNodes);
			
			Collection negEdges = new HashSet();
			neg.getEdges(negEdges);
			negEdges.removeAll(patEdges);
			
			// Now generate the subterm for one negative part
			Term sub = null;
			
			Collection deps = new HashSet();
			
			// For all nodes
			for (Iterator iter = negNodes.iterator(); iter.hasNext(); )	{
				Node n = (Node) iter.next();
				NodeTable nodeTable = tableFactory.nodeTable(n);
				
				Term eqNull = factory.expression(Opcodes.ISNULL,
																				 factory.expression(nodeTable.colId()));
				sub = factory.addExpression(Opcodes.OR, sub, eqNull);
				deps.add(nodeTable);
			}
			
			//for all edges
			for (Iterator iter = negEdges.iterator(); iter.hasNext(); )	{
				Edge e = (Edge) iter.next();
				EdgeTable edgeTable = tableFactory.edgeTable(e);
				
				Term eqNull = factory.expression(Opcodes.ISNULL,
																				 factory.expression(edgeTable.colId()));
				sub = factory.addExpression(Opcodes.OR, sub, eqNull);
				deps.add(edgeTable);
			}

			Table neutral = (Table) neutralMap.get(neg);
			deps.add(neutral);
			seq.scheduleCond(sub, deps);
		}
	}
	
	/**
	 * Handles a sequence if join.
	 *
	 * Basically, this class builds statements from added search paths.
	 * The tables are joined in the order of the search paths passed
	 * via {@link #addPath(SearchPath)}. The basic conditions are built
	 * upon adding a search path. The conditions are put aside and are
	 * note integrated in the joins until {@link produceQuery()} is ivoked.
	 * Then, a dependency analysis is performed and the retained conditions
	 * are put at the right join. You can also add on conditions at each
	 * time before {@link #produceQuery()} is invoked using the
	 * {@link scheduleCond(Term, Relation)} method. Just pass the SQL
	 * term stating the condition and the relation the condition depends
	 * on. If the condition depends on more than one relation, invoke
	 * this method successively with the same condition.
	 */
	private class JoinSequence {
		
		private final Map conditions = new HashMap();
		
		/**
		 * Record all processed tables and map them
		 * to ascending integers.
		 */
		private final Map processedTables = new HashMap();
		
		/**
		 * Also record all processed nodes and edges.
		 */
		private final Set processedEntities = new HashSet();
		
		/**
		 * Keep track of all joins added. This is a list,
		 * since order is important.
		 */
		private final List joins = new LinkedList();
		
		/**
		 * Record all nodes and edges that appear in rule
		 * conditions.
		 */
		private final Set occurInCond = new HashSet();
		
		/**
		 * Record join condition dependecies.
		 * Firstly, terms are mapped to sets of relations here.
		 * {@link #produceQuery} replaces the sets of relations with
		 * bit sets consisting of the IDs of the relations.
		 */
		private final Map joinCondDeps = new HashMap();
		
		/** The statement factory. */
		private final TypeStatementFactory factory;
		
		/** The table factory. */
		private final GraphTableFactory tableFactory;
		
		/** The default condition of a join. */
		private final Term defCond;
		
		/** The action the join sequence is produced for. */
		private final MatchingAction act;
		
		/** The current join. */
		private Relation currJoin = null;
		
		/** The current relation number. */
		private int currId = 0;
		
		private List matchedNodes;
		
		private List matchedEdges;
		
		JoinSequence(MatchingAction act, TypeStatementFactory factory,
								 GraphTableFactory tableFactory,
								 List matchedNodes, List matchedEdges) {
			
			this.act = act;
			this.factory = factory;
			this.tableFactory = tableFactory;
			this.defCond = factory.constant(true);
			this.matchedNodes = matchedNodes;
			this.matchedEdges = matchedEdges;
			
			buildConds();
		}
		
		/**
		 * Build the SQL terms for the condition section of the action.
		 * This method fills the <code>conditions</code> map, mapping
		 * the produced SQL term to collection of the relations
		 * corresponding to the used entities in the term.
		 * Furthermore, the <code>occurInCond</code> set is filled
		 * with all entities that have conditions in the cond part.
		 * @note Just for class internal use.
		 */
		private void buildConds() {
			
			for(Iterator it = act.getCondition().get(); it.hasNext();) {
				List usedEntities = new LinkedList();
				
				Expression expr = (Expression) it.next();
				Term term = genExprSQL(expr, factory, tableFactory, usedEntities);
				
				for(Iterator et = usedEntities.iterator(); et.hasNext();) {
					Object obj = et.next();
					
					if(obj instanceof Node)
						scheduleCond(term, tableFactory.nodeAttrTable((Node) obj));
					else if(obj instanceof Edge)
						scheduleCond(term, tableFactory.edgeAttrTable((Edge) obj));
					
				}
				
				occurInCond.addAll(usedEntities);
			}
		}
		
		/**
		 * Check if an entity has been added to the join sequence.
		 * @param ent The entity to check for.
		 * @return true, if the entity has already been added
		 * to the join sequence, false if not.
		 */
		private boolean hasBeenProcessed(Entity ent) {
			return processedEntities.contains(ent);
		}
		
		/**
		 * Mark an entity as processed.
		 * @param ent The entity.
		 */
		private void markProcessed(Entity ent) {
			processedEntities.add(ent);
		}
		
		/**
		 * Get all processed entities.
		 * @return A collection containing all processed entities.
		 */
		private Collection getProcessedEntities() {
			return processedEntities;
		}
		
		/**
		 * Get the id of an entity.
		 * Being marked processed, an entity gets an id. These
		 * ids start at 0 and are handed out sequentially since
		 * they are used in bit sets later on.
		 */
		private int getId(Relation table) {
			assert processedTables.containsKey(table) : "table must have been processed yet";
			return ((Integer) processedTables.get(table)).intValue();
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
		private void addCondDep(Term cond, Collection tables) {
			BitSet deps = new BitSet();
			
			for(Iterator it = tables.iterator(); it.hasNext();) {
				Table table = (Table) it.next();
				deps.set(getId(table));
			}
			
			joinCondDeps.put(cond, deps);
		}
		
		/**
		 * Add a condition tree to the list of scheduled conditions.
		 *
		 * The condition is added to the join condition dependency
		 * set. The relations in the collection <code>tables</code>
		 * are expressing the dependencies of the condtion. Any
		 * existing dependencies concerning <code>cond</code> are
		 * overwritten.
		 *
		 * @param cond The condition tree.
		 * @param tables A collection recording all relations, the
		 * condition depends on.
		 */
		public void scheduleCond(Term cond, Collection tables) {
			if(conditions.containsKey(cond)) {
				Collection deps = (Collection) conditions.get(cond);
				deps.addAll(tables);
			} else
				conditions.put(cond, tables);
		}
		
		/**
		 * Mark a condition dependent of a table.
		 *
		 * The table is added to the condition's dependency set.
		 * @param cond The condition.
		 * @param table A table on which <code>cond</code> depends.
		 */
		public void scheduleCond(Term cond, Relation table) {
			if(conditions.containsKey(cond)) {
				Collection deps = (Collection) conditions.get(cond);
				deps.add(table);
			} else {
				Collection deps = new HashSet();
				deps.add(table);
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
			
			// make an auxillary working set.
			BitSet work = new BitSet(currId);
			
			debug.entering();
			debug.report(NOTE, "processed: " + processed);
			
			// Look at all conditions that are in the join dependency set.
			for(Iterator it = joinCondDeps.keySet().iterator(); it.hasNext();) {
				Term term = (Term) it.next();
				BitSet dep = (BitSet) joinCondDeps.get(term);
				
				
				// look if all processed relations are in the dependency set
				// of this condition.
				work.clear();
				work.or(dep);
				work.and(processed);
				work.xor(dep);
				
				debug.report(NOTE, "  dep: " + dep + " -> " + work);
				
				// If yes, add this condition to the returned ones and
				// remove it from the join condition dependency set.
				if(work.nextSetBit(0) == -1) {
					res = factory.addExpression(Opcodes.AND, res, term);
					it.remove();
				}
			}
			
			debug.report(NOTE, "  res: " + res);
			debug.leaving();

			return res;
		}
		
		/**
		 * Make the condition term that is to be added to a node join.
		 * @param node The node.
		 */
		private void addNodeJoinCond(Node node) {
			Collection dep = new LinkedList();
			NodeTable nodeTable = tableFactory.nodeTable(node);
			
			dep.add(nodeTable);
			// Make type constraints
			Term res = factory.isA(nodeTable, node.getNodeType(), typeID);
			
			// Make the clauses guaranteeing injectiveness
			for(Iterator it = processedEntities.iterator(); it.hasNext();) {
				Object obj = it.next();
				if(obj instanceof Node) {
					Node curr = (Node) obj;
					NodeTable currTable = tableFactory.nodeTable(curr);
					
					if(!node.isHomomorphic(curr)) {
						res = factory.addExpression(Opcodes.AND, res,
																				factory.expression(Opcodes.NE,
																													 factory.expression(nodeTable.colId()),
																													 factory.expression(currTable.colId())));
						dep.add(currTable);
					}
				}
			}
			
			scheduleCond(res, dep);
		}
		
		/**
		 * Add the condition for an edge join.
		 * This method schedules all neccessary conditions for a join
		 * over an edge table.
		 * @param g The graph the edge occurs in.
		 * @param edge The edge.
		 * @param swapped If true, the edge is considered swapped (src and
		 * tgt node are exchanged).
		 */
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
			
			scheduleCond(typeCond, table);
			scheduleCond(srcCond, firstTable);
			scheduleCond(srcCond, table);
			scheduleCond(tgtCond, secondTable);
			scheduleCond(tgtCond, table);
		}
		
		/**
		 * Add the condition that links the node or edge table to the
		 * corresponding attribute table.
		 * @param idTable The id table (node or edge table).
		 * @param attrTable The attribute table that shall be linked to
		 * the <code>idTable</code>.
		 */
		private void addAttrJoinCond(IdTable idTable, AttributeTable attrTable) {
			Column col = attrTable.colId();
			
			Term cond = factory.expression(Opcodes.EQ,
																		 factory.expression(idTable.colId()),
																		 factory.expression(col));
			scheduleCond(cond, idTable);
			scheduleCond(cond, attrTable);
		}
		
		/**
		 * Add a join to the join list.
		 * @param rel The relation that is joined.
		 * @param kind The kind of the join (inner join, outer join, etc.)
		 */
		private void addJoin(Relation rel, int kind) {
			assert !(currJoin == null) || (rel instanceof Table)
				: "if this is the first join, the relation must be a table";
			
			boolean firstJoin = currJoin == null;
			currJoin = firstJoin ? rel : factory.join(kind, currJoin, rel, defCond);
			
			if(!firstJoin)
				joins.add(currJoin);
			
			processedTables.put(rel, new Integer(currId++));
		}
		
		/**
		 * Add a join over a node to the join list.
		 * This method automatically detects, if the join over this node
		 * was already made and leaves it out if this was the case.
		 * Additionally, a join over the attribute table is performed, if
		 * the node occurs in a condition.
		 * @param node The node.
		 * @param joinMethod The kind of join (inner, outer, etc.)
		 */
		private void addNodeJoin(Node node, int joinMethod, boolean nac) {
			if(!hasBeenProcessed(node)) {
				if(!nac)
					matchedNodes.add(node);
				
				NodeTable nodeTable = tableFactory.nodeTable(node);
				addJoin(nodeTable, joinMethod);
				addNodeJoinCond(node);
				
				if(occurInCond.contains(node)) {
					AttributeTable attrTable = tableFactory.nodeAttrTable(node);
					addJoin(attrTable, joinMethod);
					addAttrJoinCond(nodeTable, attrTable);
				}
				
				markProcessed(node);
			}
		}
		
		/**
		 * Add a join over an edge to the join list.
		 * This method does basically the same as
		 * {@link #addNodeJoin(Node, int)} but for edges.
		 * @param g The graph, the edge is in.
		 * @param edge The edge.
		 * @param swapped If true, the edge is walked "the wrong" direction in
		 * the search path, thus src and tgt node must be exchanged.
		 * @param joinMethod The metjod of join.
		 */
		private void addEdgeJoin(Graph g, Edge edge, boolean swapped,
														 int joinMethod, boolean nac) {
			if(!hasBeenProcessed(edge)) {
				if(!nac)
					matchedEdges.add(edge);
				
				EdgeTable edgeTable = tableFactory.edgeTable(edge);
				addJoin(edgeTable, joinMethod);
				addEdgeJoinCond(g, edge, swapped);

				if(occurInCond.contains(edge)) {
					AttributeTable attrTable = tableFactory.edgeAttrTable(edge);
					addJoin(attrTable, joinMethod);
					addAttrJoinCond(edgeTable, attrTable);
				}
				
				markProcessed(edge);
			}
		}
		
		/**
		 * Add a search path to the join sequence.
		 * @param sp The search path,
		 * @param joinMethod The join method.
		 */
		void addPath(SearchPath sp, int joinMethod) {
			for(Iterator it = sp.edges.iterator(); it.hasNext(); ) {
				Edge edge = (Edge) it.next();
				Graph g = sp.getGraph(edge);
				
				// If the edge is reverse the selector selects the nodes
				// backwards.
				boolean reverse = sp.isReverse(edge);
				Node firstNode = g.getEnd(edge, !reverse);
				Node secondNode = g.getEnd(edge, reverse);
				boolean nac = sp.isNAC();
				
				addNodeJoin(firstNode, joinMethod, nac);
				addEdgeJoin(g, edge, reverse, joinMethod, nac);
				addNodeJoin(secondNode, joinMethod, nac);
			}
		}
		
		/**
		 * Finish the build phase and produce a query.
		 * This method is to be called after all desired paths
		 * have been added via {@link #addPath(SearchPath).
		 * @return The query representing the added paths.
		 */
		Query produceQuery() {
			
			debug.entering();
			
			
			// If there's just one processed table (the singleton node)
			// Add a join to the neutral table to create a join
			if(processedTables.size() == 1) {
				Table neutral = tableFactory.neutralTable();
				addJoin(neutral, Join.INNER);
			}
			
			// From here: |processedTable| >= 2
			
			// First, add all terms from the cond part of the rule to
			// the join cond dependency map.
			for(Iterator it = conditions.keySet().iterator(); it.hasNext();) {
				Term term = (Term) it.next();
				Collection tables = (Collection) conditions.get(term);
				addCondDep(term, tables);
			}
			
			debug.report(NOTE, "processed: " + processedTables);
			debug.report(NOTE, "deps: " + joinCondDeps);
			
			// Add all scheduled conditions to their
			// respective joins.
			BitSet procTables = new BitSet(currId);
			
			int i = 0;
			for(Iterator jt = joins.iterator(); jt.hasNext(); i++) {
				
				Join join = (Join) jt.next();
				
				// For the first join, we also mark the left hand side
				// relation processed.
				if(i == 0) {
					assert join.getLeft() instanceof Table
						: "lhs of first join must be a table";
					procTables.set(getId(join.getLeft()));
				}
				
				// Mark the rhs of the join as processed.
				// The rhs is always a table (see the addJoin method for
				// how joins are made).
				procTables.set(getId(join.getRight()));
				
				Term conds = deliverConditions(procTables);
				debug.report(NOTE, "current: " + procTables);
				debug.report(NOTE, "conds: " + (conds != null ? true : false));
				
				// If this join has conditions, set them
				// else leave the condition to TRUE
				if(conds != null)
					join.setCondition(conds);
			}
			
			// Build the column list
			List columns = new LinkedList();
			
			for(Iterator it = matchedNodes.iterator(); it.hasNext();) {
				Node node = (Node) it.next();
				NodeTable nodeTable = tableFactory.nodeTable(node);
				columns.add(nodeTable.colId());
			}
			
			for(Iterator it = matchedEdges.iterator(); it.hasNext();) {
				Edge edge = (Edge) it.next();
				EdgeTable edgeTable = tableFactory.edgeTable(edge);
				columns.add(edgeTable.colId());
			}
			
			// assert processedTables.size() > 1 : "Small queries not yet supported";
			Query result = factory.explicitQuery(true, columns, currJoin);
			
			debug.leaving();
			
			return result;
		}
		
	}
}


