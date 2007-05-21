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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.*;

import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumpableProxy;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.Walkable;

/**
 * A graph pattern.
 * This is used for tests and the left and right sides and the NAC part of a rule.
 * These graphs have own classes for the nodes and edges as proxy objects
 * to the actual Node and Edge objects. The reason for this is:
 * The nodes and edges in a rule   that are common to the left and the right
 * side exist only once as a object (that's due to the fact, that these
 * objects are created from the AST declaration, which exist only once per
 * defined object). But we want to dicriminate between the nodes on the
 * left and right hand side od a rule, even, if they represent the same
 * declared nodes.
 */
public class Graph extends IR {
	
	protected abstract class GraphObject extends GraphDumpableProxy implements Walkable {
		public GraphObject(GraphDumpable gd) {
			super(gd);
		}
	}
	
	protected class GraphNode extends Node {
		private final Set<Graph.GraphEdge> outgoing;
		private final Set<Graph.GraphEdge> incoming;
		private final Node node;
		private final String nodeId;
		
		private GraphNode(Node n) {
			super(n.getIdent(), n.getNodeType());
			this.incoming = new HashSet<Graph.GraphEdge>();
			this.outgoing = new HashSet<Graph.GraphEdge>();
			this.node = n;
			this.nodeId = "g" + Graph.super.getId() + "_" + super.getNodeId();
		}
		
		/**
		 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeId()
		 */
		public String getNodeId() {
			return nodeId;
		}
		
		public String getNodeInfo() {
			return node.getNodeInfo();
		}
		
	}
	
	protected class GraphEdge extends Edge {
		private GraphNode source;
		private GraphNode target;
		private Edge edge;
		private final String nodeId;
		
		private GraphEdge(Edge e) {
			super(e.getIdent(), e.getEdgeType());
			this.edge = e;
			this.nodeId = "g" + Graph.super.getId() + "_" + super.getNodeId();
		}
		
		public String getNodeId() {
			return nodeId;
		}
		
		public int getNodeShape() {
			return GraphDumper.ELLIPSE;
		}
		
		public String getNodeInfo() {
			return edge.getNodeInfo();
		}
	}
	
	/** Map that maps a node to an internal node. */
	private final Map<Node, Graph.GraphNode> nodes = new HashMap<Node, Graph.GraphNode>();
	
	/** Map that maps an edge to an internal edge. */
	private final Map<Edge, Graph.GraphEdge> edges = new HashMap<Edge, Graph.GraphEdge>();
	
	private GraphNode getOrSetNode(Node n) {
		GraphNode res;
		
		// Do not include the virtual retyped nodes in the graph.
		// TODO why??? we could just check in the generator whether this is a retyped node
		// this would eliminate this unnecessary <code>changesType()</code> stuff
		if(n.isRetyped()) n = ((RetypedNode)n).getOldNode();
		
		if(!nodes.containsKey(n)) {
			res = new GraphNode(n);
			nodes.put(n, res);
		} else
			res = nodes.get(n);
		
		return res;
	}
	
	private GraphEdge getOrSetEdge(Edge e) {
		GraphEdge res;
		Map<Edge, Graph.GraphEdge> map = edges;
		
		if(!map.containsKey(e)) {
			res = new GraphEdge(e);
			map.put(e, res);
		} else
			res = map.get(e);
		
		return res;
	}
	
	private GraphNode checkNode(Node n) {
		assert nodes.containsKey(n) : "Node must be in graph: " + n;
		return nodes.get(n);
	}
	
	private GraphEdge checkEdge(Edge e) {
		assert edges.containsKey(e) : "Edge must be in graph: " + e;
		return edges.get(e);
	}
	
	/**
	 * Make a new graph.
	 */
	public Graph() {
		super("graph");
	}
	
	/**
	 * Allows another class to append a suffix to the graph's name.
	 * This is useful for rules, that can add "left" or "right" to the
	 * graph's name.
	 * @param s A suffix for the graph's name.
	 */
	public void setNameSuffix(String s) {
		setName("graph " + s);
	}
	
	/**
	 * Check if a node is contained in the graph.
	 * @param node The node
	 * @return true, if the node is contained the graph, false, if not.
	 */
	public boolean hasNode(Node node) {
		return nodes.containsKey(node);
	}
	
	/**
	 * Check if an edge is contained in the graph.
	 * @param edge The edge
	 * @return true, if the edge is contained the graph, false, if not.
	 */
	public boolean hasEdge(Edge edge) {
		return edges.containsKey(edge);
	}
	
	/**
	 * Get a read-only collection containing all nodes in this graph.
	 * @return A collection containing all nodes in this graph.
	 * @note The collection is read-only and may not be modified.
	 */
	public Collection<Node> getNodes() {
		return Collections.unmodifiableCollection(nodes.keySet());
	}
	
	/**
	 * Get a read-only collection containing all edges in this graph.
	 * @return A collection containing all edges in this graph.
	 * @note The collection is read-only and may not be modified.
	 */
	public Collection<Edge> getEdges() {
		return Collections.unmodifiableCollection(edges.keySet());
	}
	
	/**
	 * Put all nodes in this graph into a collection.
	 * @param c The collection to put them into.
	 * @return The given collection.
	 */
	public Collection<Node> putNodes(Collection<Node> c) {
		c.addAll(nodes.keySet());
		return c;
	}
	
	/**
	 * Put all edges in this graph into a collection.
	 * @param c The collection to put them into.
	 * @return The given collection.
	 */
	public Collection<Edge> putEdges(Collection<Edge> c) {
		c.addAll(edges.keySet());
		return c;
	}
	
	/**
	 * Get the number of ingoing edges.
	 * @param node The node.
	 * @return The number of ingoing edges;
	 */
	public int getInDegree(Node node) {
		GraphNode gn = checkNode(node);
		return gn.incoming.size();
	}
	
	/**
	 * Get the number of outgoing edges.
	 * @param node The node.
	 * @return The number of outgoing edges;
	 */
	public int getOutDegree(Node node) {
		GraphNode gn = checkNode(node);
		return gn.outgoing.size();
	}
	
	/**
	 * Get the set of all incoming edges for a node.
	 * @param n The node.
	 * @param c A set where the edges are put to.
	 */
	public Collection<Edge> getIncoming(Node n, Collection<Edge> c) {
		GraphNode gn = checkNode(n);
		for(Iterator<Graph.GraphEdge> it = gn.incoming.iterator(); it.hasNext();) {
			GraphEdge e = it.next();
			c.add(e.edge);
		}
		return c;
	}
	
	/**
	 * Get an iterator iterating over all incoming edges of a node.
	 * @param n The node
	 * @return The iterator.
	 */
	public Collection<? extends Edge> getIncoming(Node n) {
		return Collections.unmodifiableCollection(getIncoming(n, new LinkedList<Edge>()));
	}
	
	/**
	 * Get the set of outgoing edges for a node.
	 * @param n The node.
	 * @param c A set where the edges are put to.
	 */
	public Collection<Edge> getOutgoing(Node n, Collection<Edge> c) {
		GraphNode gn = checkNode(n);
		for(GraphEdge e : gn.outgoing) {
			c.add(e.edge);
		}
		return c;
	}
	
	/**
	 * Get an iterator iterating over all outgoing edges of a node.
	 * @param n The node
	 * @return The iterator.
	 */
	public Collection<Edge> getOutgoing(Node n) {
		return Collections.unmodifiableCollection(getOutgoing(n, new LinkedList<Edge>()));
	}
	
	/**
	 * Get the source node of an edge.
	 * @param e The edge.
	 * @return The node, the edge leaves, or null in case of a single edge.
	 */
	public Node getSource(Edge e) {
		GraphEdge ge = checkEdge(e);
		return ge.source!=null?ge.source.node:null;
	}
	
	/**
	 * Get the target node of an edge.
	 * @param e The edge
	 * @return The node that the edge points to, or null in case of a single edge.
	 */
	public Node getTarget(Edge e) {
		GraphEdge ge = checkEdge(e);
		return ge.target!=null?ge.target.node:null;
	}
	
	/**
	 * Get an "end" of an edge.
	 * @param e The edge.
	 * @param source The end.
	 * @return If <code>source</code> was true, this method returns the
	 * source node of the edge, if <code>source</code> was false, it
	 * returns the target node.
	 */
	public Node getEnd(Edge e, boolean source) {
		return source ? getSource(e) : getTarget(e);
	}
	
	/**
	 * Add a connection to the graph.
	 * @param left The left node.
	 * @param edge The edge connecting the left and the right node.
	 * @param right The right node.
	 */
	public void addConnection(Node left, Edge edge, Node right) {
		// Get the nodes and edges from the map.
		GraphNode l = getOrSetNode(left);
		GraphNode r = getOrSetNode(right);
		GraphEdge e = getOrSetEdge(edge);
		
		// Update outgoing and incoming of the nodes.
		l.outgoing.add(e);
		r.incoming.add(e);
		
		// Set the edge source and target
		e.source = l;
		e.target = r;
	}
	
	/**
	 * Add a single node (without an edge) to the graph.
	 * @param node The node.
	 */
	public void addSingleNode(Node node) {
		getOrSetNode(node);
	}
	
	
	/**
	 * Add a single node (without an edge) to the graph.
	 * @param node The node.
	 */
	public void addSingleEdge(Edge edge) {
		GraphEdge e = getOrSetEdge(edge);
		e.source = null;
		e.target = null;
	}
	
	/**
	 * Check, if a node is a single node.
	 * A node is <i>single</i>, if it has no incident edges.
	 * @param node The node.
	 * @return true, if the node is single, false if not.
	 */
	public boolean isSingle(Node node) {
		GraphNode gn = checkNode(node);
		return gn.incoming.isEmpty() && gn.outgoing.isEmpty();
	}
	
	/**
	 * Get a graph dumpable thing for a node that is local in this graph.
	 * @param node The node.
	 * @return A graph dumpable thing representing this node local in this graph.
	 */
	public GraphDumpable getLocalDumpable(Node node) {
		return checkNode(node);
	}
	
	/**
	 * @see #getLocalDumpable(Node)
	 */
	public GraphDumpable getLocalDumpable(Edge edge) {
		return checkEdge(edge);
	}
	
	/**
	 * Check, if this graph is a subgraph of another one.
	 * @param g The other graph.
	 * @return true, if all nodes and edges of this graph are
	 * also contained in g.
	 */
	public boolean isSubOf(Graph g) {
		return g.getNodes().containsAll(nodes.keySet())
			&& g.getEdges().containsAll(edges.keySet());
	}
	
}

