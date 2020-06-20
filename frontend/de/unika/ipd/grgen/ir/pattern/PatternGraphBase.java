/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir.pattern;

import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumpableProxy;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.Walkable;

/**
 * This is a base class for the pattern graph containing the nodes/edges, and analogously variables and subpatterns.
 * It has own classes for the nodes and edges as proxy objects to the actual Node and Edge objects.
 * The reason for this is: The nodes and edges in a rule that are common to the left and the right side
 * exist only once as an object (that's due to the fact that these objects are created from the AST declaration,
 * which exist only once per defined object).
 * But we want to discriminate between the nodes on the left and right hand side of a rule,
 * even if they represent the same declared nodes.
 */
public abstract class PatternGraphBase extends IR
{
	protected abstract class GraphObject extends GraphDumpableProxy implements Walkable
	{
		public GraphObject(GraphDumpable graphDumpable)
		{
			super(graphDumpable);
		}
	}

	protected class GraphNode extends Node
	{
		private final Set<PatternGraphBase.GraphEdge> outgoing;
		private final Set<PatternGraphBase.GraphEdge> incoming;
		private final Node node;
		private final String nodeId;

		private GraphNode(Node node)
		{
			super(node.getIdent(), node.getNodeType(), node.directlyNestingLHSGraph,
					node.isMaybeDeleted(), node.isMaybeRetyped(), node.isDefToBeYieldedTo(), node.context);
			this.incoming = new LinkedHashSet<PatternGraphBase.GraphEdge>();
			this.outgoing = new LinkedHashSet<PatternGraphBase.GraphEdge>();
			this.node = node;
			this.nodeId = "g" + PatternGraphBase.super.getId() + "_" + super.getNodeId();
		}

		/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeId() */
		@Override
		public String getNodeId()
		{
			return nodeId;
		}

		@Override
		public String getNodeInfo()
		{
			return node.getNodeInfo();
		}

	}

	protected class GraphEdge extends Edge
	{
		private GraphNode source;
		private GraphNode target;
		private Edge edge;
		private final String nodeId;

		private GraphEdge(Edge edge)
		{
			super(edge.getIdent(), edge.getEdgeType(), edge.directlyNestingLHSGraph,
					edge.isMaybeDeleted(), edge.isMaybeRetyped(), edge.isDefToBeYieldedTo(), edge.context);
			this.edge = edge;
			this.nodeId = "g" + PatternGraphBase.super.getId() + "_" + super.getNodeId();
			this.fixedDirection = edge.fixedDirection;
		}

		@Override
		public String getNodeId()
		{
			return nodeId;
		}

		@Override
		public int getNodeShape()
		{
			return GraphDumper.ELLIPSE;
		}

		@Override
		public String getNodeInfo()
		{
			return edge.getNodeInfo();
		}
	}

	/** Map that maps a node to an internal node. */
	protected final Map<Node, PatternGraphBase.GraphNode> nodes;

	/** Map that maps an edge to an internal edge. */
	protected final Map<Edge, PatternGraphBase.GraphEdge> edges;

	protected final Collection<Variable> vars = new LinkedHashSet<Variable>();

	protected final Set<SubpatternUsage> subpatternUsages;

	/** A set of nodes which will be matched homomorphically to any other node in the pattern.
	 *  they appear if they're not referenced within the pattern, but some nested component uses them */
	protected final HashSet<Node> homToAllNodes = new HashSet<Node>();

	/** A set of edges which will be matched homomorphically to any other edge in the pattern.
	 *  they appear if they're not referenced within the pattern, but some nested component uses them  */
	protected final HashSet<Edge> homToAllEdges = new HashSet<Edge>();

	PatternGraphLhs directlyNestingLHSGraph; // either this or the left graph

	private String nameOfGraph;


	/** Make a new graph. */
	public PatternGraphBase(String nameOfGraph)
	{
		super("graph");
		this.nameOfGraph = nameOfGraph;
		this.nodes = new LinkedHashMap<Node, PatternGraphBase.GraphNode>();
		this.edges = new LinkedHashMap<Edge, PatternGraphBase.GraphEdge>();
		this.subpatternUsages = new LinkedHashSet<SubpatternUsage>();
	}

	/** Make a new graph with preset nodes, edges, subpatternUsages (copy from another pattern graph). */
	protected PatternGraphBase(String nameOfGraph,
			Map<Node, PatternGraphBase.GraphNode> nodes,
			Map<Edge, PatternGraphBase.GraphEdge> edges,
			Set<SubpatternUsage> subpatternUsages)
	{
		super("graph");
		this.nameOfGraph = nameOfGraph;
		this.nodes = nodes;
		this.edges = edges;
		this.subpatternUsages = subpatternUsages;
	}
	
	public void setDirectlyNestingLHSGraph(PatternGraphLhs directlyNestingLHSGraph)
	{
		// This is for setting the directlyNestingLHSGraph for a retyped node when it gets added
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
	}

	public String getNameOfGraph()
	{
		return nameOfGraph;
	}

	/**
	 * Allows another class to append a suffix to the graph's name.
	 * This is useful for rules, that can add "left" or "right" to the graph's name.
	 * @param suffix A suffix for the graph's name.
	 */
	public void setNameSuffix(String suffix)
	{
		setName("graph " + suffix);
	}

	/////////////////////////////////////////////////////////////////////

	private GraphNode getOrSetNode(Node node)
	{
		GraphNode res;
		if(node == null)
			return null;

		// Do not include the virtual retyped nodes in the graph.
		// (Alternative handling: we could just check in the generator whether this is a retyped node, eliminating the <code>changesType()</code> stuff.)
		if(node.isRetyped() && node.isRHSEntity()) {
			RetypedNode retypedNode = (RetypedNode)node;
			node = retypedNode.getOldNode();
			node.setRetypedNode(retypedNode, this);
			retypedNode.directlyNestingLHSGraph = directlyNestingLHSGraph;
		}

		if(!nodes.containsKey(node)) {
			res = new GraphNode(node);
			nodes.put(node, res);
		} else {
			res = nodes.get(node);
		}

		return res;
	}

	private GraphNode checkNode(Node node)
	{
		assert nodes.containsKey(node) : "Node must be in graph: " + node;
		return nodes.get(node);
	}

	/** @return true, if the given node is contained in the graph, false, if not. */
	public boolean hasNode(Node node)
	{
		return nodes.containsKey(node);
	}

	/**
	 * Get a read-only collection containing all nodes in this graph.
	 * @return A collection containing all nodes in this graph.
	 * Note: The collection is read-only and may not be modified.
	 */
	public Collection<Node> getNodes()
	{
		return Collections.unmodifiableCollection(nodes.keySet());
	}

	/**
	 * Put all nodes in this graph into a collection.
	 * @param collection The collection to put them into.
	 * @return The given collection.
	 */
	public Collection<Node> putNodes(Collection<Node> collection)
	{
		collection.addAll(nodes.keySet());
		return collection;
	}

	/** Add a single node (i.e. no incident edges) to the graph. */
	public void addSingleNode(Node node)
	{
		getOrSetNode(node);
	}

	/** @return true, if the node is single (i.e. has no incident edges), false if not. */
	public boolean isSingle(Node node)
	{
		GraphNode graphNode = checkNode(node);
		return graphNode.incoming.isEmpty() && graphNode.outgoing.isEmpty();
	}
	
	public void addNodeIfNotYetContained(Node node)
	{
		if(hasNode(node))
			return;
		
		addSingleNode(node);
		addHomToAll(node);
	}

	public void addHomToAll(Node node)
	{
		homToAllNodes.add(node);
	}

	/** @return A graph dumpable thing representing the given node local in this graph. */
	public GraphDumpable getLocalDumpable(Node node)
	{
		if(node == null)
			return null;
		else
			return checkNode(node);
	}

	/////////////////////////////////////////////////////////////////////

	/** @return The number of incoming edges of the given node */
	public int getInDegree(Node node)
	{
		GraphNode graphNode = checkNode(node);
		return graphNode.incoming.size();
	}

	/** @return The number of outgoing edges of the given node */
	public int getOutDegree(Node node)
	{
		GraphNode graphNode = checkNode(node);
		return graphNode.outgoing.size();
	}

	/** Get the set of all incoming edges for a given node, they are put into the given collection (which gets returned)*/
	public Collection<Edge> getIncoming(Node node, Collection<Edge> collection)
	{
		GraphNode graphNode = checkNode(node);
		for(Iterator<PatternGraphBase.GraphEdge> it = graphNode.incoming.iterator(); it.hasNext();) {
			GraphEdge graphEdge = it.next();
			collection.add(graphEdge.edge);
		}
		return collection;
	}

	/** Get the set of all incoming edges for a given node */
	public Collection<? extends Edge> getIncoming(Node node)
	{
		return Collections.unmodifiableCollection(getIncoming(node, new LinkedList<Edge>()));
	}

	/** Get the set of all outgoing edges for a given node, they are put into the given collection (which gets returned)*/
	public Collection<Edge> getOutgoing(Node node, Collection<Edge> collection)
	{
		GraphNode graphNode = checkNode(node);
		for(GraphEdge graphEdge : graphNode.outgoing) {
			collection.add(graphEdge.edge);
		}
		return collection;
	}

	/** Get the set of all outgoing edges for a given node */
	public Collection<Edge> getOutgoing(Node node)
	{
		return Collections.unmodifiableCollection(getOutgoing(node, new LinkedList<Edge>()));
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
			boolean redirectSource, boolean redirectTarget)
	{
		// Get the nodes and edges from the map.
		GraphNode leftGraphNode = getOrSetNode(left);
		GraphNode rightGraphNode = getOrSetNode(right);
		edge.fixedDirection = fixedDirection;
		GraphEdge graphEdge = getOrSetEdge(edge);

		// Update outgoing and incoming of the nodes.
		if(!redirectSource) {
			if(leftGraphNode != null)
				leftGraphNode.outgoing.add(graphEdge);
		}
		if(!redirectTarget) {
			if(rightGraphNode != null)
				rightGraphNode.incoming.add(graphEdge);
		}

		// Set the edge source and target
		if(redirectSource)
			edge.setRedirectedSource(left, this);
		else
			graphEdge.source = leftGraphNode;
		if(redirectTarget)
			edge.setRedirectedTarget(right, this);
		else
			graphEdge.target = rightGraphNode;
	}

	/////////////////////////////////////////////////////////////////////

	private GraphEdge getOrSetEdge(Edge edge)
	{
		GraphEdge res;

		if(edge.isRetyped() && edge.isRHSEntity()) {
			RetypedEdge retypedEdge = (RetypedEdge)edge;
			edge = retypedEdge.getOldEdge();
			edge.setRetypedEdge(retypedEdge, this);
			retypedEdge.directlyNestingLHSGraph = directlyNestingLHSGraph;
		}

		if(!edges.containsKey(edge)) {
			res = new GraphEdge(edge);
			edges.put(edge, res);
		} else {
			res = edges.get(edge);
		}

		return res;
	}

	private GraphEdge checkEdge(Edge edge)
	{
		assert edges.containsKey(edge) : "Edge must be in graph: " + edge;
		return edges.get(edge);
	}

	/** @return true, if the given edge is contained in the graph, false, if not. */
	public boolean hasEdge(Edge edge)
	{
		return edges.containsKey(edge);
	}

	/**
	 * Get a read-only collection containing all edges in this graph.
	 * @return A collection containing all edges in this graph.
	 * Note: The collection is read-only and may not be modified.
	 */
	public Collection<Edge> getEdges()
	{
		return Collections.unmodifiableCollection(edges.keySet());
	}

	/**
	 * Put all edges in this graph into a collection.
	 * @param collection The collection to put them into.
	 * @return The given collection.
	 */
	public Collection<Edge> putEdges(Collection<Edge> collection)
	{
		collection.addAll(edges.keySet());
		return collection;
	}

	/** Add a single edge (i.e. dangling) to the graph. */
	public void addSingleEdge(Edge edge)
	{
		getOrSetEdge(edge);
	}

	public void addEdgeIfNotYetContained(Edge edge)
	{
		if(hasEdge(edge))
			return;
		
		addSingleEdge(edge);
		addHomToAll(edge);
	}

	public void addHomToAll(Edge edge)
	{
		homToAllEdges.add(edge);
	}

	/** @see #getLocalDumpable(Node) */
	public GraphDumpable getLocalDumpable(Edge edge)
	{
		return checkEdge(edge);
	}

	/////////////////////////////////////////////////////////////////////

	/** @return The source node, the edge leaves from, or null in case of a single edge. */
	public Node getSource(Edge edge)
	{
		GraphEdge graphEdge = checkEdge(edge);
		return graphEdge.source != null ? graphEdge.source.node : null;
	}

	/** @return The target node, the edge points to, or null in case of a single edge. */
	public Node getTarget(Edge edge)
	{
		GraphEdge graphEdge = checkEdge(edge);
		return graphEdge.target != null ? graphEdge.target.node : null;
	}

	/////////////////////////////////////////////////////////////////////
	
	public void addVariable(Variable var)
	{
		vars.add(var);
	}

	public Collection<Variable> getVars()
	{
		return Collections.unmodifiableCollection(vars);
	}

	public boolean hasVar(Variable var)
	{
		return vars.contains(var);
	}

	/////////////////////////////////////////////////////////////////////
	
	/** @return true, if the given subpattern usage is contained in the graph, false, if not. */
	public boolean hasSubpatternUsage(SubpatternUsage sub)
	{
		return subpatternUsages.contains(sub);
	}

	/**
	 * Get a read-only collection containing all subpattern usages in this graph.
	 * @return A collection containing all subpattern usages in this graph.
	 * Note: The collection is read-only and may not be modified.
	 */
	public Collection<SubpatternUsage> getSubpatternUsages()
	{
		return Collections.unmodifiableCollection(subpatternUsages);
	}

	/** Add a subpattern usage to the graph. */
	public void addSubpatternUsage(SubpatternUsage subpatternUsage)
	{
		subpatternUsages.add(subpatternUsage);
	}
}
