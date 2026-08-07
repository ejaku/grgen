/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// ImplicitNegComputer.java
/// 
/// @author Sebastian Buchwald, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.pattern
{

using System;
using System.Collections.Generic;
using System.Diagnostics;

using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
using Node = de.unika.ipd.grgen.ir.pattern.Node;
using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;

/// <summary>
/// Class computing the implicit negative application conditions
/// that are used to implement the exact and dpo (dangling+identification) modifiers
/// </summary>
public class ImplicitNegComputer
{
	/// <summary>
	/// the pattern graph for which the implicit negatives are to be computed </summary>
	internal PatternGraphLhsNode patternGraph;

	/// <summary>
	/// All nodes that need a single node NAC. </summary>
	private ISet<NodeDeclNode> nodesRequiringNeg =
		new LinkedHashSet<NodeDeclNode>();

	/// <summary>
	/// Map a homomorphic set to a set of edges (of the NAC). </summary>
	private IDictionary<ISet<NodeDeclNode>, ISet<ConnectionNode>> homNodesToEdges =
		new LinkedHashMap<ISet<NodeDeclNode>, ISet<ConnectionNode>>();

	// counts number of implicit single node negative patterns
	// created from pattern modifiers, in order to get unique negative names
	internal int implicitNegCounter = 0;


	public ImplicitNegComputer(PatternGraphLhsNode patternGraph)
	{
		this.patternGraph = patternGraph;

		if(patternGraph.IsExact())
		{
			NodesRequireNeg(patternGraph.Nodes);

			if(patternGraph.IsDangling() && !patternGraph.IsIdentification())
				patternGraph.ReportWarning("The keyword \"dangling\" is redundant for exact patterns");

			foreach(ExactNode exact in patternGraph.exacts.ChildrenExact)
				exact.ReportWarning("Exact statement occurs in exact pattern");

			return;
		}

		if(patternGraph.IsDangling())
		{
			ISet<ConstraintDeclNode> deletedNodes = patternGraph.Rule.DeletedElements;
			NodesRequireNeg(GetDpoPatternNodes(deletedNodes));

			foreach(ExactNode exact in patternGraph.exacts.ChildrenExact)
			{
				foreach(NodeDeclNode exactNode in exact.ExactNodes)
				{
					if(deletedNodes.Contains(exactNode))
						exact.ReportWarning("Exact statement for " + exactNode.Kind + " "
								+ exactNode.Ident.Symbol.Text
								+ " is redundant, since the pattern is declared \"dangling\" or \"dpo\"");
				}
			}
		}

		IDictionary<NodeDeclNode, int> generatedExactNodes = new LinkedHashMap<NodeDeclNode, int>();
		for(int i = 0; i < patternGraph.exacts.ChildrenExact.Count; i++)
		{ // exact Statements
			ExactNode exact = patternGraph.exacts.Get(i);
			foreach(NodeDeclNode exactNode in exact.ExactNodes)
			{
				// coords of occurrence are not available
				if(generatedExactNodes.ContainsKey(exactNode))
				{
					exact.ReportWarning(exactNode.Kind + " "
							+ exactNode.Ident.Symbol.Text
							+ " already occurs in exact statement at "
							+ patternGraph.exacts.Get(generatedExactNodes[exactNode]).GetCoords());
				}
				else
					generatedExactNodes[exactNode] = Convert.ToInt32(i);
			}
		}

		NodesRequireNeg(generatedExactNodes.Keys);
	}

	private void NodesRequireNeg(ICollection<NodeDeclNode> nodes)
	{
		foreach(NodeDeclNode node in nodes)
		{
			if(node.IsDummy())
				continue;

			nodesRequiringNeg.Add(node);
			ISet<NodeDeclNode> homSet = patternGraph.GetHomomorphic(node);
			if(!homNodesToEdges.ContainsKey(homSet))
			{
				ISet<ConnectionNode> edgeSet = new HashSet<ConnectionNode>();
				homNodesToEdges[homSet] = edgeSet;
			}
		}
	}

	/// <summary>
	/// Return the set of nodes needed for the singleNodeNegMap if the whole pattern is dpo.
	/// </summary>
	private static ISet<NodeDeclNode> GetDpoPatternNodes(ISet<ConstraintDeclNode> deletedEntities)
	{
		ISet<NodeDeclNode> deletedNodes = new LinkedHashSet<NodeDeclNode>();

		foreach(DeclNode declNode in deletedEntities)
		{
			if(declNode is NodeDeclNode)
			{
				NodeDeclNode node = (NodeDeclNode)declNode;
				if(!node.IsDummy())
					deletedNodes.Add(node);
			}
		}

		return deletedNodes;
	}

	/// <summary>
	/// Get all implicit NACs. </summary>
	/// <returns> The Collection for the NACs. </returns>
	public virtual IList<PatternGraphLhs> ImplicitNegGraphs
	{
		get
		{
		Debug.Assert(patternGraph.IsResolved());

		IList<PatternGraphLhs> implicitNegGraphs = new List<PatternGraphLhs>();

		// add existing edges to the corresponding sets
		foreach(ConnectionCharacter connection in patternGraph.connections.ChildrenExact)
		{
			if(!(connection is ConnectionNode))
				continue;

			ConnectionNode cn = (ConnectionNode)connection;
			NodeDeclNode src = cn.Src;
			if(nodesRequiringNeg.Contains(src))
			{
				ISet<NodeDeclNode> homSet = patternGraph.GetHomomorphic(src);
				ISet<ConnectionNode> edges = homNodesToEdges[homSet];
				edges.Add(cn);
				homNodesToEdges[homSet] = edges;
			}
			NodeDeclNode tgt = cn.Tgt;
			if(nodesRequiringNeg.Contains(tgt))
			{
				ISet<NodeDeclNode> homSet = patternGraph.GetHomomorphic(tgt);
				ISet<ConnectionNode> edges = homNodesToEdges[homSet];
				edges.Add(cn);
				homNodesToEdges[homSet] = edges;
			}
		}

		TypeDeclNode edgeRoot = patternGraph.ArbitraryEdgeRootTypeDecl;
		TypeDeclNode nodeRoot = patternGraph.NodeRootTypeDecl;

		// generate and add pattern graphs
		foreach(NodeDeclNode nodeRequiringNeg in nodesRequiringNeg)
		{
			//for (int direction = INCOMING; direction <= OUTGOING; direction++) {
			ISet<ConnectionNode> edgeSet = homNodesToEdges[patternGraph.GetHomomorphic(nodeRequiringNeg)];

			PatternGraphLhs neg = new PatternGraphLhs("implneg_" + implicitNegCounter, 0);
			++implicitNegCounter;
			neg.DirectlyNestingLHSGraph = neg;

			// add edges to NAC
			ISet<EdgeDeclNode> allNegEdges = new LinkedHashSet<EdgeDeclNode>();
			ISet<NodeDeclNode> allNegNodes = new LinkedHashSet<NodeDeclNode>();
			foreach(ConnectionNode connEdge in edgeSet)
			{
				connEdge.AddToGraph(neg);

				allNegEdges.Add(connEdge.Edge);
				allNegNodes.Add(connEdge.Src);
				allNegNodes.Add(connEdge.Tgt);
			}

			AddInheritedHomSet(neg, allNegEdges, allNegNodes);

			// add another edge of type edgeRoot to the NAC
			EdgeDeclNode edge = patternGraph.GetAnonymousEdgeDecl(edgeRoot, patternGraph.context);
			NodeDeclNode dummyNode = patternGraph.GetAnonymousDummyNode(nodeRoot, patternGraph.context);

			ConnectionNode conn = new ConnectionNode(nodeRequiringNeg, edge, dummyNode,
					ConnectionKind.ARBITRARY, patternGraph);
			conn.AddToGraph(neg);

			implicitNegGraphs.Add(neg);
			//}
		}

		return implicitNegGraphs;
		}
	}

	/// <summary>
	/// Add all necessary homomorphic sets to a NAC.
	/// 
	/// If an edge a-e->b is homomorphic to another edge c-f->d f only added if
	/// a is homomorphic to c and b is homomorphic to d.
	/// </summary>
	private void AddInheritedHomSet(PatternGraphLhs neg, ISet<EdgeDeclNode> allNegEdges, ISet<NodeDeclNode> allNegNodes)
	{
		// inherit homomorphic nodes
		foreach(NodeDeclNode node in allNegNodes)
		{
			ISet<Node> homSet = new LinkedHashSet<Node>();
			ISet<NodeDeclNode> homNodes = patternGraph.GetHomomorphic(node);

			foreach(NodeDeclNode homNode in homNodes)
			{
				if(allNegNodes.Contains(homNode))
					homSet.Add(homNode.CheckIR(typeof(Node)));
			}
			if(homSet.Count > 1)
				neg.AddHomomorphicNodes(homSet);
		}

		// inherit homomorphic edges
		foreach(EdgeDeclNode edge in allNegEdges)
		{
			ISet<Edge> homSet = new LinkedHashSet<Edge>();
			ISet<EdgeDeclNode> homEdges = patternGraph.GetHomomorphic(edge);

			foreach(EdgeDeclNode homEdge in homEdges)
			{
				if(allNegEdges.Contains(homEdge))
					homSet.Add(homEdge.CheckIR(typeof(Edge)));
			}
			if(homSet.Count > 1)
				neg.AddHomomorphicEdges(homSet);
		}
	}
}

}
