/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.Map;
import java.util.Set;
import java.util.List;

import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumpableProxy;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.Walkable;

/**
 * A graph pattern.
 * This is used for tests and the left and right sides and the NAC part of a rule.
 * These graphs have own classes for the nodes and edges as proxy objects to the actual Node and Edge objects.
 * The reason for this is: The nodes and edges in a rule that are common to the left and the right side
 * exist only once as a object (that's due to the fact, that these objects are created from the AST declaration,
 * which exist only once per defined object).
 * But we want to discriminate between the nodes on the left and right hand side of a rule,
 * even if they represent the same declared nodes.
 */
/// TODO: this class is never instantiated, only PatternGraphs are used -- ...?
public abstract class Graph extends IR {
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
			super(n.getIdent(), n.getNodeType(), n.directlyNestingLHSGraph,
					n.isMaybeDeleted(), n.isMaybeRetyped(), n.isDefToBeYieldedTo(), n.context);
			this.incoming = new LinkedHashSet<Graph.GraphEdge>();
			this.outgoing = new LinkedHashSet<Graph.GraphEdge>();
			this.node = n;
			this.nodeId = "g" + Graph.super.getId() + "_" + super.getNodeId();
		}

		/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeId() */
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
			super(e.getIdent(), e.getEdgeType(), e.directlyNestingLHSGraph, 
					e.isMaybeDeleted(), e.isMaybeRetyped(), e.isDefToBeYieldedTo(), e.context);
			this.edge = e;
			this.nodeId = "g" + Graph.super.getId() + "_" + super.getNodeId();
			this.fixedDirection = e.fixedDirection;
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
	private final Map<Node, Graph.GraphNode> nodes = new LinkedHashMap<Node, Graph.GraphNode>();

	/** Map that maps an edge to an internal edge. */
	private final Map<Edge, Graph.GraphEdge> edges = new LinkedHashMap<Edge, Graph.GraphEdge>();

	private Set<SubpatternUsage> subpatternUsages = new LinkedHashSet<SubpatternUsage>();

	private List<OrderedReplacements> orderedReplacements = new LinkedList<OrderedReplacements>();
	
	PatternGraph directlyNestingLHSGraph; // either this or the left graph

	private String nameOfGraph;

	/** Make a new graph. */
	public Graph(String nameOfGraph) {
		super("graph");
		this.nameOfGraph = nameOfGraph;
	}

	public void setDirectlyNestingLHSGraph(PatternGraph directlyNestingLHSGraph) {
		// this is for setting the directlyNestingLHSGraph for a retyped node when it gets added
		// TODO: in a lot of situations one would need the pointer to the parent node in the AST and in the IR 
		// a lot of convoluted code making everything unnecessarily complex was written to circumvent the lack thereof
		// TODO: take care of the major architectural misdecision to not include parent-pointers only allowing top-down processing,
		// and replace a lot of non understandable side effects by local routines accessing the parent to get the context information needed
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
	}
	
	public String getNameOfGraph() {
		return nameOfGraph;
	}

	private GraphNode getOrSetNode(Node n) {
		GraphNode res;
		if (n == null) return null;

		// Do not include the virtual retyped nodes in the graph.
		// TODO why??? we could just check in the generator whether this is a retyped node
		// this would eliminate this unnecessary <code>changesType()</code> stuff
		if (n.isRetyped() && n.isRHSEntity()) {
			RetypedNode retypedNode = (RetypedNode) n;
			n = retypedNode.getOldNode();
			n.setRetypedNode(retypedNode, this);
			retypedNode.directlyNestingLHSGraph = directlyNestingLHSGraph;
		}
		
		if (!nodes.containsKey(n)) {
			res = new GraphNode(n);
			nodes.put(n, res);
		} else {
			res = nodes.get(n);
		}

		return res;
	}

	private GraphEdge getOrSetEdge(Edge e) {
		GraphEdge res;

		// TODO Batz included this because an analogous invocation can be found
		// in the method right above, don't exactly whether this makes sense
		if (e.isRetyped() && e.isRHSEntity()) {
			RetypedEdge retypedEdge = (RetypedEdge) e;
			e = retypedEdge.getOldEdge();
			e.setRetypedEdge(retypedEdge, this);
			retypedEdge.directlyNestingLHSGraph = directlyNestingLHSGraph;
		}
		
		if (!edges.containsKey(e)) {
			res = new GraphEdge(e);
			edges.put(e, res);
		} else {
			res = edges.get(e);
		}

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
	 * Allows another class to append a suffix to the graph's name.
	 * This is useful for rules, that can add "left" or "right" to the graph's name.
	 * @param s A suffix for the graph's name.
	 */
	public void setNameSuffix(String s) {
		setName("graph " + s);
	}

	/** @return true, if the given node is contained in the graph, false, if not. */
	public boolean hasNode(Node node) {
		return nodes.containsKey(node);
	}

	/** @return true, if the given edge is contained in the graph, false, if not. */
	public boolean hasEdge(Edge edge) {
		return edges.containsKey(edge);
	}

	/** @return true, if the given subpattern usage is contained in the graph, false, if not. */
	public boolean hasSubpatternUsage(SubpatternUsage sub) {
		return subpatternUsages.contains(sub);
	}

	/**
	 * Get a read-only collection containing all nodes in this graph.
	 * @return A collection containing all nodes in this graph.
	 * Note: The collection is read-only and may not be modified.
	 */
	public Collection<Node> getNodes() {
		return Collections.unmodifiableCollection(nodes.keySet());
	}

	/**
	 * Get a read-only collection containing all edges in this graph.
	 * @return A collection containing all edges in this graph.
	 * Note: The collection is read-only and may not be modified.
	 */
	public Collection<Edge> getEdges() {
		return Collections.unmodifiableCollection(edges.keySet());
	}

	/**
	 * Get a read-only collection containing all subpattern usages in this graph.
	 * @return A collection containing all subpattern usages in this graph.
	 * Note: The collection is read-only and may not be modified.
	 */
	public Collection<SubpatternUsage> getSubpatternUsages() {
		return Collections.unmodifiableCollection(subpatternUsages);
	}

	/**
	 * Get a read-only collection containing all ordered replacements
	 * (subpattern dependent replacement, emit here) in this graph.
	 * @return A collection containing all ordered replacements in this graph.
	 * Note: The collection is read-only and may not be modified.
	 */
	public Collection<OrderedReplacements> getOrderedReplacements() {
		return Collections.unmodifiableCollection(orderedReplacements);
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

	/** @return The number of incoming edges of the given node */
	public int getInDegree(Node node) {
		GraphNode gn = checkNode(node);
		return gn.incoming.size();
	}

	/** @return The number of outgoing edges of the given node */
	public int getOutDegree(Node node) {
		GraphNode gn = checkNode(node);
		return gn.outgoing.size();
	}

	/** Get the set of all incoming edges for a given node, they are put into the given collection (which gets returned)*/
	public Collection<Edge> getIncoming(Node n, Collection<Edge> c) {
		GraphNode gn = checkNode(n);
		for (Iterator<Graph.GraphEdge> it = gn.incoming.iterator(); it.hasNext();) {
			GraphEdge e = it.next();
			c.add(e.edge);
		}
		return c;
	}

	/** Get the set of all incoming edges for a given node */
	public Collection<? extends Edge> getIncoming(Node n) {
		return Collections.unmodifiableCollection(getIncoming(n, new LinkedList<Edge>()));
	}

	/** Get the set of all outgoing edges for a given node, they are put into the given collection (which gets returned)*/
	public Collection<Edge> getOutgoing(Node n, Collection<Edge> c) {
		GraphNode gn = checkNode(n);
		for (GraphEdge e : gn.outgoing) {
			c.add(e.edge);
		}
		return c;
	}

	/** Get the set of all outgoing edges for a given node */
	public Collection<Edge> getOutgoing(Node n) {
		return Collections.unmodifiableCollection(getOutgoing(n, new LinkedList<Edge>()));
	}

	/** @return The source node, the edge leaves from, or null in case of a single edge. */
	public Node getSource(Edge e) {
		GraphEdge ge = checkEdge(e);
		return ge.source != null ? ge.source.node : null;
	}

	/** @return The target node, the edge points to, or null in case of a single edge. */
	public Node getTarget(Edge e) {
		GraphEdge ge = checkEdge(e);
		return ge.target != null ? ge.target.node : null;
	}

	/**
	 * Add a connection to the graph.
	 * @param left The left node.
	 * @param edge The edge connecting the left and the right node.
	 * @param right The right node.
	 * @param fixedDirection Tells whether this is a directed edge or not
	 * @param redirectSource Tells whether the edge should be redirected to the source
	 * @param redirectTarget Tells whether the edge should be redirected to the target
	 */
	public void addConnection(Node left, Edge edge, Node right, boolean fixedDirection, 
			boolean redirectSource, boolean redirectTarget) {
		// Get the nodes and edges from the map.
		GraphNode l = getOrSetNode(left);
		GraphNode r = getOrSetNode(right);
		edge.fixedDirection = fixedDirection;
		GraphEdge e = getOrSetEdge(edge);

		// Update outgoing and incoming of the nodes.
		if(!redirectSource) if(l != null) l.outgoing.add(e);
		if(!redirectTarget) if(r != null) r.incoming.add(e);

        // Set the edge source and target
		if(redirectSource) edge.setRedirectedSource(left, this);
		else e.source = l;
		if(redirectTarget) edge.setRedirectedTarget(right, this);
		else e.target = r;
	}

	/** Add a single node (i.e. no incident edges) to the graph. */
	public void addSingleNode(Node node) {
		getOrSetNode(node);
	}

    /** Add a single edge (i.e. dangling) to the graph. */
	public void addSingleEdge(Edge edge) {
		getOrSetEdge(edge);
	}

	/** Add a subpattern usage to the graph. */
	public void addSubpatternUsage(SubpatternUsage subpatternUsage) {
		subpatternUsages.add(subpatternUsage);
	}

	/** Add a ordered replacement (subpattern dependent replacement, emit here) to the graph */
	public void addOrderedReplacement(OrderedReplacements orderedRepl) {
		orderedReplacements.add(orderedRepl);
	}

	/** @return true, if the node is single (i.e. has no incident edges), false if not. */
	public boolean isSingle(Node node) {
		GraphNode gn = checkNode(node);
		return gn.incoming.isEmpty() && gn.outgoing.isEmpty();
	}

	/** @return A graph dumpable thing representing the given node local in this graph. */
	public GraphDumpable getLocalDumpable(Node node) {
		if (node == null)
			return null;
		else
			return checkNode(node);
	}

	/** @see #getLocalDumpable(Node) */
	public GraphDumpable getLocalDumpable(Edge edge) {
		return checkEdge(edge);
	}
}
