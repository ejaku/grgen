/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ir.pattern
{

using System.Collections.Generic;
using System.Diagnostics;

using Entity = de.unika.ipd.grgen.ir.Entity;
using IR = de.unika.ipd.grgen.ir.IR;
using GraphDumpable = de.unika.ipd.grgen.util.GraphDumpable;
using GraphDumper = de.unika.ipd.grgen.util.GraphDumper;

/// <summary>
/// This is a base class for the pattern graph containing the nodes/edges, and analogously variables and subpatterns.
/// It has own classes for the nodes and edges as proxy objects to the actual Node and Edge objects.
/// The reason for this is: The nodes and edges in a rule that are common to the left and the right side
/// exist only once as an object (that's due to the fact that these objects are created from the AST declaration,
/// which exist only once per defined object).
/// But we want to discriminate between the nodes on the left and right hand side of a rule,
/// even if they represent the same declared nodes.
/// </summary>
public abstract class PatternGraphBase : IR
{
	public class GraphNode : Node
	{
		private readonly PatternGraphBase outerInstance;

		internal readonly ISet<PatternGraphBase.GraphEdge> outgoing;
		internal readonly ISet<PatternGraphBase.GraphEdge> incoming;
		internal readonly Node node;
		internal readonly string nodeId;

		internal GraphNode(PatternGraphBase outerInstance, Node node)
			: base(node.Ident, node.NodeType, node.directlyNestingLHSGraph,
					node.IsMaybeDeleted(), node.IsMaybeRetyped(), node.IsDefToBeYieldedTo(), node.context)
		{
			this.outerInstance = outerInstance;
			this.incoming = new LinkedHashSet<PatternGraphBase.GraphEdge>();
			this.outgoing = new LinkedHashSet<PatternGraphBase.GraphEdge>();
			this.node = node;
			this.nodeId = "g" + outerInstance.Id + "_" + base.NodeId;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeId() "/>
		public override string NodeId
		{
			get
			{
				return nodeId;
			}
		}

		public override string NodeInfo
		{
			get
			{
				return node.NodeInfo;
			}
		}

	}

	public class GraphEdge : Edge
	{
		private readonly PatternGraphBase outerInstance;

		internal GraphNode source;
		internal GraphNode target;
		internal Edge edge;
		internal readonly string nodeId;

		internal GraphEdge(PatternGraphBase outerInstance, Edge edge)
			: base(edge.Ident, edge.EdgeType, edge.directlyNestingLHSGraph,
					edge.IsMaybeDeleted(), edge.IsMaybeRetyped(), edge.IsDefToBeYieldedTo(), edge.context)
		{
			this.outerInstance = outerInstance;
			this.edge = edge;
			this.nodeId = "g" + outerInstance.Id + "_" + base.NodeId;
			this.fixedDirection = edge.fixedDirection;
		}

		public override string NodeId
		{
			get
			{
				return nodeId;
			}
		}

		public override int NodeShape
		{
			get
			{
				return GraphDumper.ELLIPSE;
			}
		}

		public override string NodeInfo
		{
			get
			{
				return edge.NodeInfo;
			}
		}
	}

	/// <summary>
	/// Map that maps a node to an internal node. </summary>
	protected internal readonly IDictionary<Node, PatternGraphBase.GraphNode> nodes;

	/// <summary>
	/// Map that maps an edge to an internal edge. </summary>
	protected internal readonly IDictionary<Edge, PatternGraphBase.GraphEdge> edges;

	protected internal readonly ISet<Variable> vars = new LinkedHashSet<Variable>();

	protected internal readonly ISet<SubpatternUsage> subpatternUsages;

	/// <summary>
	/// A set of nodes which will be matched homomorphically to any other node in the pattern.
	///  they appear if they're not referenced within the pattern, but some nested component uses them 
	/// </summary>
	protected internal readonly HashSet<Node> homToAllNodes = new HashSet<Node>();

	/// <summary>
	/// A set of edges which will be matched homomorphically to any other edge in the pattern.
	///  they appear if they're not referenced within the pattern, but some nested component uses them  
	/// </summary>
	protected internal readonly HashSet<Edge> homToAllEdges = new HashSet<Edge>();

	internal PatternGraphLhs directlyNestingLHSGraph; // either this or the left graph

	private string nameOfGraph;


	/// <summary>
	/// Make a new graph. </summary>
	public PatternGraphBase(string nameOfGraph)
		: base("graph")
	{
		this.nameOfGraph = nameOfGraph;
		this.nodes = new LinkedHashMap<Node, PatternGraphBase.GraphNode>();
		this.edges = new LinkedHashMap<Edge, PatternGraphBase.GraphEdge>();
		this.subpatternUsages = new LinkedHashSet<SubpatternUsage>();
	}

	/// <summary>
	/// Make a new pattern graph with preset nodes, edges, subpatternUsages (copy from another pattern graph). </summary>
	protected internal PatternGraphBase(string nameOfGraph,
			IDictionary<Node, PatternGraphBase.GraphNode> nodes,
			IDictionary<Edge, PatternGraphBase.GraphEdge> edges,
			ISet<SubpatternUsage> subpatternUsages)
		: base("graph")
	{
		this.nameOfGraph = nameOfGraph;
		this.nodes = nodes;
		this.edges = edges;
		this.subpatternUsages = subpatternUsages;
	}

	public virtual PatternGraphLhs DirectlyNestingLHSGraph
	{
		set
		{
			// This is for setting the value for a retyped node when it gets added
			this.directlyNestingLHSGraph = value;
		}
	}

	public virtual string NameOfGraph
	{
		get
		{
			return nameOfGraph;
		}
	}

	/// <summary>
	/// Allows another class to append a suffix to the graph's name.
	/// This is useful for rules, that can add "left" or "right" to the graph's name. </summary>
	/// <param name="suffix"> A suffix for the graph's name. </param>
	public virtual string NameSuffix
	{
		set
		{
			Name = "graph " + value;
		}
	}

	/////////////////////////////////////////////////////////////////////

	private GraphNode GetOrSetNode(Node node)
	{
		GraphNode res;
		if(node == null)
			return null;

		// Do not include the virtual retyped nodes in the graph.
		// (Alternative handling: we could just check in the generator whether this is a retyped node, eliminating the <code>changesType()</code> stuff.)
		if(node.IsRetyped() && node.IsRHSEntity())
		{
			RetypedNode retypedNode = (RetypedNode)node;
			node = retypedNode.OldNode;
			node.SetRetypedNode(retypedNode, this);
			retypedNode.directlyNestingLHSGraph = directlyNestingLHSGraph;
		}

		if(!nodes.ContainsKey(node))
		{
			res = new GraphNode(this, node);
			nodes[node] = res;
		}
		else
			res = nodes[node];

		return res;
	}

	private GraphNode CheckNode(Node node)
	{
		Debug.Assert(nodes.ContainsKey(node), "Node must be in graph: " + node);
		return nodes[node];
	}

	/// <returns> true, if the given node is contained in the graph, false, if not. </returns>
	public virtual bool HasNode(Node node)
	{
		return nodes.ContainsKey(node);
	}

	/// <summary>
	/// Get a read-only collection containing all nodes in this graph. </summary>
	/// <returns> A collection containing all nodes in this graph.
	/// Note: The collection is read-only and may not be modified. </returns>
	public virtual ICollection<Node> Nodes
	{
		get
		{
			return Collections.UnmodifiableSet(nodes.Keys);
		}
	}

	/// <summary>
	/// Put all nodes in this pattern graph into a collection. </summary>
	/// <param name="collection"> The collection to put them into. </param>
	/// <returns> The given collection. </returns>
	public virtual ICollection<Node> PutNodes(ICollection<Node> collection)
	{
		collection.AddAll(nodes.Keys);
		return collection;
	}

	/// <summary>
	/// Add a single node (i.e. no incident edges) to the graph. </summary>
	public virtual void AddSingleNode(Node node)
	{
		GetOrSetNode(node);
	}

	/// <returns> true, if the node is single (i.e. has no incident edges), false if not. </returns>
	public virtual bool IsSingle(Node node)
	{
		GraphNode graphNode = CheckNode(node);
		return graphNode.incoming.Count == 0 && graphNode.outgoing.Count == 0;
	}

	public virtual void AddNodeIfNotYetContained(Node node)
	{
		if(HasNode(node))
			return;

		AddSingleNode(node);
		AddHomToAll(node);
	}

	public virtual void AddHomToAll(Node node)
	{
		homToAllNodes.Add(node);
	}

	/// <returns> A graph dumpable thing representing the given node local in this pattern graph. </returns>
	public virtual GraphDumpable GetLocalDumpable(Node node)
	{
		if(node == null)
			return null;
		else
			return CheckNode(node);
	}

	/////////////////////////////////////////////////////////////////////

	/// <returns> The number of incoming edges of the given node </returns>
	public virtual int GetInDegree(Node node)
	{
		GraphNode graphNode = CheckNode(node);
		return graphNode.incoming.Count;
	}

	/// <returns> The number of outgoing edges of the given node </returns>
	public virtual int GetOutDegree(Node node)
	{
		GraphNode graphNode = CheckNode(node);
		return graphNode.outgoing.Count;
	}

	/// <summary>
	/// Get the set of all incoming edges for a given node, they are put into the given set (which gets returned) </summary>
	public virtual ISet<Edge> GetIncoming(Node node, ISet<Edge> collection)
	{
		GraphNode graphNode = CheckNode(node);
		foreach(GraphEdge graphEdge in graphNode.incoming)
			collection.Add(graphEdge.edge);
		return collection;
	}

	/// <summary>
	/// Get the set of all incoming edges for a given node </summary>
	public virtual ISet<Edge> GetIncoming(Node node)
	{
		return Collections.UnmodifiableSet(GetIncoming(node, new HashSet<Edge>()));
	}

	/// <summary>
	/// Get the set of all outgoing edges for a given node, they are put into the given set (which gets returned) </summary>
	public virtual ISet<Edge> GetOutgoing(Node node, ISet<Edge> collection)
	{
		GraphNode graphNode = CheckNode(node);
		foreach(GraphEdge graphEdge in graphNode.outgoing)
			collection.Add(graphEdge.edge);
		return collection;
	}

	/// <summary>
	/// Get the set of all outgoing edges for a given node </summary>
	public virtual ISet<Edge> GetOutgoing(Node node)
	{
		return Collections.UnmodifiableSet(GetOutgoing(node, new HashSet<Edge>()));
	}

	/// <summary>
	/// Add a connection to the graph. </summary>
	/// <param name="left"> The left node. </param>
	/// <param name="edge"> The edge connecting the left and the right node. </param>
	/// <param name="right"> The right node. </param>
	/// <param name="fixedDirection"> Tells whether this is a directed edge or not </param>
	/// <param name="redirectSource"> Tells whether the edge should be redirected to the source </param>
	/// <param name="redirectTarget"> Tells whether the edge should be redirected to the target </param>
	public virtual void AddConnection(Node left, Edge edge, Node right, bool fixedDirection,
			bool redirectSource, bool redirectTarget)
	{
		// Get the nodes and edges from the map.
		GraphNode leftGraphNode = GetOrSetNode(left);
		GraphNode rightGraphNode = GetOrSetNode(right);
		edge.fixedDirection = fixedDirection;
		GraphEdge graphEdge = GetOrSetEdge(edge);

		// Update outgoing and incoming of the nodes.
		if(!redirectSource)
		{
			if(leftGraphNode != null)
				leftGraphNode.outgoing.Add(graphEdge);
		}
		if(!redirectTarget)
		{
			if(rightGraphNode != null)
				rightGraphNode.incoming.Add(graphEdge);
		}

		// Set the edge source and target
		if(redirectSource)
			edge.SetRedirectedSource(left, this);
		else
			graphEdge.source = leftGraphNode;
		if(redirectTarget)
			edge.SetRedirectedTarget(right, this);
		else
			graphEdge.target = rightGraphNode;
	}

	/////////////////////////////////////////////////////////////////////

	private GraphEdge GetOrSetEdge(Edge edge)
	{
		GraphEdge res;

		if(edge.IsRetyped() && edge.IsRHSEntity())
		{
			RetypedEdge retypedEdge = (RetypedEdge)edge;
			edge = retypedEdge.OldEdge;
			edge.SetRetypedEdge(retypedEdge, this);
			retypedEdge.directlyNestingLHSGraph = directlyNestingLHSGraph;
		}

		if(!edges.ContainsKey(edge))
		{
			res = new GraphEdge(this, edge);
			edges[edge] = res;
		}
		else
			res = edges[edge];

		return res;
	}

	private GraphEdge CheckEdge(Edge edge)
	{
		Debug.Assert(edges.ContainsKey(edge), "Edge must be in graph: " + edge);
		return edges[edge];
	}

	/// <returns> true, if the given edge is contained in the graph, false, if not. </returns>
	public virtual bool HasEdge(Edge edge)
	{
		return edges.ContainsKey(edge);
	}

	/// <summary>
	/// Get a read-only collection containing all edges in this graph. </summary>
	/// <returns> A collection containing all edges in this graph.
	/// Note: The collection is read-only and may not be modified. </returns>
	public virtual ICollection<Edge> Edges
	{
		get
		{
			return Collections.UnmodifiableSet(edges.Keys);
		}
	}

	/// <summary>
	/// Put all edges in this pattern graph into a collection. </summary>
	/// <param name="collection"> The collection to put them into. </param>
	/// <returns> The given collection. </returns>
	public virtual ICollection<Edge> PutEdges(ICollection<Edge> collection)
	{
		collection.AddAll(edges.Keys);
		return collection;
	}

	/// <summary>
	/// Add a single edge (i.e. dangling) to the graph. </summary>
	public virtual void AddSingleEdge(Edge edge)
	{
		GetOrSetEdge(edge);
	}

	public virtual void AddEdgeIfNotYetContained(Edge edge)
	{
		if(HasEdge(edge))
			return;

		AddSingleEdge(edge);
		AddHomToAll(edge);
	}

	public virtual void AddHomToAll(Edge edge)
	{
		homToAllEdges.Add(edge);
	}

	/// <seealso cref=".getLocalDumpable(Node) "/>
	public virtual GraphDumpable GetLocalDumpable(Edge edge)
	{
		return CheckEdge(edge);
	}

	/////////////////////////////////////////////////////////////////////

	/// <returns> The source node, the edge leaves from, or null in case of a single edge. </returns>
	public virtual Node GetSource(Edge edge)
	{
		GraphEdge graphEdge = CheckEdge(edge);
		return graphEdge.source != null ? graphEdge.source.node : null;
	}

	/// <returns> The target node, the edge points to, or null in case of a single edge. </returns>
	public virtual Node GetTarget(Edge edge)
	{
		GraphEdge graphEdge = CheckEdge(edge);
		return graphEdge.target != null ? graphEdge.target.node : null;
	}

	/////////////////////////////////////////////////////////////////////

	public virtual void AddVariable(Variable var)
	{
		vars.Add(var);
	}

	public virtual ICollection<Variable> Vars
	{
		get
		{
			return Collections.UnmodifiableSet(vars);
		}
	}

	public virtual bool HasVar(Variable var)
	{
		return vars.Contains(var);
	}

	/////////////////////////////////////////////////////////////////////

	/// <returns> true, if the given subpattern usage is contained in the graph, false, if not. </returns>
	public virtual bool HasSubpatternUsage(SubpatternUsage sub)
	{
		return subpatternUsages.Contains(sub);
	}

	/// <summary>
	/// Get a read-only collection containing all subpattern usages in this graph. </summary>
	/// <returns> A collection containing all subpattern usages in this graph.
	/// Note: The collection is read-only and may not be modified. </returns>
	public virtual ICollection<SubpatternUsage> SubpatternUsages
	{
		get
		{
			return Collections.UnmodifiableSet(subpatternUsages);
		}
	}

	/// <summary>
	/// Add a subpattern usage to the graph. </summary>
	public virtual void AddSubpatternUsage(SubpatternUsage subpatternUsage)
	{
		subpatternUsages.Add(subpatternUsage);
	}

	/////////////////////////////////////////////////////////////////////

	public virtual Entity TryGetMember(string name)
	{
		foreach(Node node in nodes.Keys)
		{
			if(node.Ident.ToString().Equals(name))
				return node;
		}
		foreach(Edge edge in edges.Keys)
		{
			if(edge.Ident.ToString().Equals(name))
				return edge;
		}
		foreach(Variable var in vars)
		{
			if(var.Ident.ToString().Equals(name))
				return var;
		}
		return null;
	}
}

}
