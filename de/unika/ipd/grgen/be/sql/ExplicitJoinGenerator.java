/**
 * Created on Mar 31, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.PrintStream;
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

import de.unika.ipd.grgen.ir.Edge;
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
	public ExplicitJoinGenerator(SQLParameters parameters, SQLFormatter formatter,
			TypeID typeID) {
		
		super(parameters, formatter, typeID);
		
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
		Comparator comparator = new NodeComparator(pattern);

		debug.entering();
		
		if(rest.isEmpty())
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
	 * Make a table column alias for the node table.
	 * @param node The node.
	 * @return The column alias.
	 */
	private String makeNodeTableAlias(Node node) {
		return parameters.getTableNodes() + " AS " + mangleNode(node) + " ("
		+ getNodeCol(node, parameters.getColNodesId()) + ","
		+ getNodeCol(node, parameters.getColNodesTypeId()) + ")";
	}
	
	/**
	 * Make a table column alias for the edge table.
	 * @param edge The edge.
	 * @return The column alias.
	 */
	private String makeEdgeTableAlias(Edge edge) {
		return parameters.getTableEdges() + " AS " + mangleEdge(edge) + " ("
		+ getEdgeCol(edge, parameters.getColEdgesId()) + ","
		+ getEdgeCol(edge, parameters.getColEdgesTypeId()) + ","
		+ getEdgeCol(edge, parameters.getColEdgesSrcId()) + ","
		+ getEdgeCol(edge, parameters.getColEdgesTgtId()) + ")";
	}
	
	class StmtContext {
		Graph graph;
		Set processedNodes = new HashSet();
		Set processedEdges = new HashSet();
		
		StmtContext(Graph graph) {
			this.graph = graph;
		}
	}
	
	private void makeNodeJoinCond(Node node, StmtContext ctx, StringBuffer sb) {
		sb.append(formatter.makeNodeTypeIsA(node, this));
		
		// Make the clauses guaranteeing injectiveness
		for(Iterator it = ctx.processedNodes.iterator(); it.hasNext();) {
			Node curr = (Node) it.next();
			
			if(!node.isHomomorphic(curr)) 
				addToCond(sb, getNodeCol(node, parameters.getColNodesId()) 
						+ " <> " + getNodeCol(curr, parameters.getColNodesId()));
		}
		sb.append(" AND ");
	}
	
	private void makeEdgeJoin(Edge edge, boolean reverse, StmtContext ctx, StringBuffer sb) {
		if(!ctx.processedEdges.contains(edge)) {
			
			debug.entering();
			
			Node[] nodes = new Node[] { 
					(Node) ctx.graph.getSource(edge),
					(Node) ctx.graph.getTarget(edge)					
			};
			
			// If the edge is reverse the selector selects the nodes 
			// backwards. 
			int first = reverse ? 1 : 0;
			int second = 1 - first;
			
			Node firstNode = nodes[first];
			Node secondNode = nodes[second];
			
			debug.report(NOTE, "join: " + firstNode + ", " + edge + ", " + secondNode);
			
			boolean genFirst = !ctx.processedNodes.contains(firstNode); 
			
			if(genFirst)  
				addTo(sb, "", " JOIN ", makeNodeTableAlias(firstNode));

			addTo(sb, "", " JOIN ", makeEdgeTableAlias(edge));
			sb.append(" ON ");
			
			if(genFirst) { 
				makeNodeJoinCond(firstNode, ctx, sb);
				ctx.processedNodes.add(firstNode);
			}

			sb.append(getNodeCol(firstNode, parameters.getColNodesId())
					+ " = " + getEdgeCol(edge, edgeCols[first]));
			
			ctx.processedEdges.add(edge);
			
			if(!ctx.processedNodes.contains(secondNode)) {
				addTo(sb, "", " JOIN ", makeNodeTableAlias(secondNode));
				sb.append(" ON ");
				makeNodeJoinCond(secondNode, ctx, sb);
				ctx.processedNodes.add(secondNode);
			} else
				sb.append(" AND ");

			sb.append(getNodeCol(secondNode, parameters.getColNodesId())
					+ " = " + getEdgeCol(edge, edgeCols[second]));
			sb.append(getBreakLine());
			
			debug.leaving();
		}
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLGenerator#genMatchStatement(de.unika.ipd.grgen.ir.MatchingAction, java.util.List, java.util.List)
	 */
	public String genMatchStatement(MatchingAction act, List matchedNodes,
			List matchedEdges) {

		debug.entering();
		
		StringBuffer stmt = new StringBuffer();
		Graph graph = act.getPattern();
		SearchPath[] paths = computeSearchPaths(graph);
		
		for(int i = 0; i < paths.length; i++) {
			StringBuffer sb = new StringBuffer();
			paths[i].dump(sb, graph);
			debug.report(NOTE, sb.toString());
		}
		
		StmtContext stmtCtx = new StmtContext(graph);
	
		int pathsProcessed = 0;
		int selectedPath = 0;
		boolean[] done = new boolean[paths.length];
		
		// Build joins until all paths are covered
		// This is not streight forward since all paths after the first (which is
		// given) are selected dependent on what has been joined already. In other
		// words: Avoid joining two non-connected paths.
		while(selectedPath >= 0) {
			SearchPath path = paths[selectedPath];
			
			assert !path.edges.isEmpty() : "Edge list of path may not be empty";
			
			for(Iterator it = path.edges.iterator(); it.hasNext();) {
				Edge edge = (Edge) it.next();
				makeEdgeJoin(edge, path.isReverse(edge), stmtCtx, stmt);
			}
			
			done[selectedPath] = true;
			
			selectedPath = -1;
			
			// TODO Test unconnected graphs here!
			for(int i = 0; i < paths.length; i++) {
				if(!done[i]) {
					selectedPath = i;
					Edge firstEdge = (Edge) paths[i].edges.get(0);
					Node start = (Node) graph.getSource(firstEdge);
					
					if(stmtCtx.processedNodes.contains(start))
						break;
				}
			}
		}
		
		// Also add the edges that have not beed considered yet.
		Collection restEdges = graph.getEdges(new HashSet());
		restEdges.removeAll(stmtCtx.processedEdges);
		
		for(Iterator it = restEdges.iterator(); it.hasNext();) {
			Edge edge = (Edge) it.next();
			makeEdgeJoin(edge, false, stmtCtx, stmt);
		}

		StringBuffer front = new StringBuffer();
		for(Iterator it = stmtCtx.processedNodes.iterator(); it.hasNext();) {
			Node node = (Node) it.next();
			matchedNodes.add(node);
			addToList(front, getNodeCol(node, parameters.getColNodesId()));
		}

		for(Iterator it = graph.getEdges(matchedEdges).iterator(); it.hasNext();) {
			Edge edge = (Edge) it.next();
			addToList(front, getEdgeCol(edge, parameters.getColEdgesId()));
		}
		
		debug.leaving();
		
		String res = "SELECT " + front.toString() + " FROM " + stmt.toString();

		
		File f = new File("stmt_" + act.getIdent() + ".txt");
		try {
			f.createNewFile();
			FileOutputStream fos = new FileOutputStream(f);
			PrintStream ps = new PrintStream(fos);
			ps.println("EXPLAIN " + res);
			fos.close();
		} catch(IOException e) {
		}
		
		
		return res; 
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLGenerator#getEdgeCol(de.unika.ipd.grgen.ir.Edge, java.lang.String)
	 */
	public String getEdgeCol(Edge e, String col) {
		return mangleEdge(e) + "_" + col;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLGenerator#getNodeCol(de.unika.ipd.grgen.ir.Node, java.lang.String)
	 */
	public String getNodeCol(Node n, String col) {
		return mangleNode(n) + "_" + col;
	}
	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLGenerator#getBreakLine()
	 */
	protected String getBreakLine() {
		return " ";
	}
}
