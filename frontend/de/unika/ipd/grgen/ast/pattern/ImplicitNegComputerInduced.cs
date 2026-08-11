/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// ImplicitNegComputerInduced.java
/// 
/// @author Sebastian Buchwald, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.pattern
{

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ast;
	using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using de.unika.ipd.grgen.util.collection;

	/// <summary>
	/// Class computing the implicit negative application conditions
	/// that are used to implement the induced modifier
	/// </summary>
	public class ImplicitNegComputerInduced
	{
		/// <summary>
		/// the pattern graph for which the implicit negatives are to be computed </summary>
		internal PatternGraphLhsNode patternGraph;

		/// <summary>
		/// All node pairs that need a double node NAC. </summary>
		private ISet<Pair<NodeDeclNode, NodeDeclNode>> nodePairsRequiringNeg =
			new LinkedHashSet<Pair<NodeDeclNode, NodeDeclNode>>();

		/// <summary>
		/// Map each pair of homomorphic sets of nodes to a set of edges (of the NAC). </summary>
		private IDictionary<Pair<ISet<NodeDeclNode>, ISet<NodeDeclNode>>, ISet<ConnectionNode>> homNodePairsToEdges =
			new LinkedHashMap<Pair<ISet<NodeDeclNode>, ISet<NodeDeclNode>>, ISet<ConnectionNode>>();

		// counts number of implicit double node negative patterns
		// created from pattern modifiers, in order to get unique negative names
		internal int implicitNegCounter = 0;


		public ImplicitNegComputerInduced(PatternGraphLhsNode patternGraph)
		{
			this.patternGraph = patternGraph;

			if(patternGraph.IsInduced())
			{
				NodesRequiringPairNeg(patternGraph.Nodes);

				foreach(InducedNode induced in patternGraph.induceds.ChildrenExact)
					induced.ReportWarning("Induced statement occurs in induced pattern");
				return;
			}

			IDictionary<ISet<NodeDeclNode>, int> generatedInducedSets = new LinkedHashMap<ISet<NodeDeclNode>, int>();
			for(int i = 0; i < patternGraph.induceds.ChildrenExact.Count; i++)
			{
				InducedNode induced = patternGraph.induceds.Get(i);
				ISet<NodeDeclNode> inducedNodes = induced.InducedNodesSet;
				if(generatedInducedSets.ContainsKey(inducedNodes))
				{
					InducedNode oldOcc = patternGraph.induceds.Get(generatedInducedSets[inducedNodes]);
					induced.ReportWarning("Same induced statement also occurs at " + oldOcc.Coords);
				}
				else
				{
					NodesRequiringPairNeg(inducedNodes);
					generatedInducedSets[inducedNodes] = Convert.ToInt32(i);
				}
			}

			WarnRedundantInducedStatement(patternGraph.induceds, generatedInducedSets);
		}

		private void NodesRequiringPairNeg(ISet<NodeDeclNode> nodes)
		{
			foreach(NodeDeclNode src in nodes)
			{
				if(src.IsDummy())
					continue;

				foreach(NodeDeclNode tgt in nodes)
				{
					if(tgt.IsDummy())
						continue;

					Pair<NodeDeclNode, NodeDeclNode> pair = new Pair<NodeDeclNode, NodeDeclNode>(src, tgt);
					nodePairsRequiringNeg.Add(pair);

					Pair<ISet<NodeDeclNode>, ISet<NodeDeclNode>> key = new Pair<ISet<NodeDeclNode>, ISet<NodeDeclNode>>(
							patternGraph.GetHomomorphic(src), patternGraph.GetHomomorphic(tgt));

					if(!homNodePairsToEdges.ContainsKey(key))
						homNodePairsToEdges[key] = new LinkedHashSet<ConnectionNode>();
				}
			}
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

				// add existing edges to the corresponding pattern graph
				foreach(ConnectionCharacter connection in patternGraph.connections.ChildrenExact)
				{
					if(!(connection is ConnectionNode))
						continue;

					ConnectionNode cn = (ConnectionNode)connection;

					Pair<ISet<NodeDeclNode>, ISet<NodeDeclNode>> key = new Pair<ISet<NodeDeclNode>, ISet<NodeDeclNode>>(
							patternGraph.GetHomomorphic(cn.Src), patternGraph.GetHomomorphic(cn.Tgt));

					ISet<ConnectionNode> edges = homNodePairsToEdges[key];
					// edges == null if conn is a dangling edge or one of the nodes is not induced
					if(edges != null)
					{
						edges.Add(cn);
						homNodePairsToEdges[key] = edges;
					}
				}

				TypeDeclNode edgeRoot = patternGraph.ArbitraryEdgeRootTypeDecl;

				foreach(Pair<NodeDeclNode, NodeDeclNode> pair in nodePairsRequiringNeg)
				{
					NodeDeclNode src = pair.first;
					NodeDeclNode tgt = pair.second;

					if(string.CompareOrdinal(src.Id, tgt.Id) > 0)
						continue;

					Pair<ISet<NodeDeclNode>, ISet<NodeDeclNode>> key = new Pair<ISet<NodeDeclNode>, ISet<NodeDeclNode>>(
							patternGraph.GetHomomorphic(src), patternGraph.GetHomomorphic(tgt));
					Pair<ISet<NodeDeclNode>, ISet<NodeDeclNode>> reverseKey = new Pair<ISet<NodeDeclNode>, ISet<NodeDeclNode>>(
							patternGraph.GetHomomorphic(tgt), patternGraph.GetHomomorphic(src));

					ISet<ConnectionNode> edgeSet = homNodePairsToEdges[key];
					edgeSet.AddAll(homNodePairsToEdges[reverseKey]);

					PatternGraphLhs neg = new PatternGraphLhs("implnegind_" + implicitNegCounter, 0);
					++implicitNegCounter;
					neg.DirectlyNestingLHSGraph = neg;

					// add edges to the NAC
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

					ConnectionCharacter conn = new ConnectionNode(src, edge, tgt,
							ConnectionKind.ARBITRARY, patternGraph);

					conn.AddToGraph(neg);

					implicitNegGraphs.Add(neg);
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
						homSet.Add(homNode.CheckIR<Node>(typeof(Node)));
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
						homSet.Add(homEdge.CheckIR<Edge>(typeof(Edge)));
				}
				if(homSet.Count > 1)
					neg.AddHomomorphicEdges(homSet);
			}
		}

		/// <summary>
		/// warn if an induced statement is redundant.
		/// 
		/// Algorithm:
		/// Input: Sets V_i of nodes
		/// for each V_i
		///   K_i = all pairs of nodes of V_i
		/// for each i
		///   for each k_i of K_i
		///     for each K_j
		///       if k_i \in K_j: mark k_i
		///   if all k_i marked: warn
		/// </summary>
		/// <param name="generatedInducedSets"> Set of all induced statements </param>
		private static void WarnRedundantInducedStatement(CollectNode<InducedNode> induceds,
				IDictionary<ISet<NodeDeclNode>, int> generatedInducedSets)
		{
			IDictionary<IDictionary<IList<NodeDeclNode>, bool>, int> inducedEdgeMap =
					new LinkedHashMap<IDictionary<IList<NodeDeclNode>, bool>, int>();

			// create all pairs of nodes (->edges)
			foreach(KeyValuePair<ISet<NodeDeclNode>, int> nodeMapEntry in generatedInducedSets.SetOfKeyValuePairs())
				FillInducedEdgeMap(inducedEdgeMap, nodeMapEntry);

			foreach(KeyValuePair<IDictionary<IList<NodeDeclNode>, bool>, int> candidate in inducedEdgeMap.SetOfKeyValuePairs())
			{
				ISet<int> witnesses = GetWitnessesAndMarkEdge(inducedEdgeMap, candidate);

				// all edges marked?
				if(AllMarked(candidate))
				{
					string witnessesLoc = "";
					foreach(int? index in witnesses)
						witnessesLoc += induceds.Get(index.Value).GetCoords() + " ";
					witnessesLoc = witnessesLoc.Trim();
					induceds.Get(candidate.Value.IntValue()).ReportWarning(
							"Induced statement is redundant, since covered by statement(s) at " + witnessesLoc);
				}
			}
		}

		private static void FillInducedEdgeMap(IDictionary<IDictionary<IList<NodeDeclNode>, bool>, int> inducedEdgeMap,
				KeyValuePair<ISet<NodeDeclNode>, int> nodeMapEntry)
		{
			// if the Boolean in markedMap is true -> edge is marked
			IDictionary<IList<NodeDeclNode>, bool> markedMap = new LinkedHashMap<IList<NodeDeclNode>, bool>();

			foreach(NodeDeclNode src in nodeMapEntry.Key)
			{
				foreach(NodeDeclNode tgt in nodeMapEntry.Key)
				{
					IList<NodeDeclNode> edge = new List<NodeDeclNode>();
					edge.Add(src);
					edge.Add(tgt);

					markedMap[edge] = Convert.ToBoolean(false);
				}
			}

			inducedEdgeMap[markedMap] = nodeMapEntry.Value;
		}

		private static ISet<int> GetWitnessesAndMarkEdge(IDictionary<IDictionary<IList<NodeDeclNode>, bool>, int> inducedEdgeMap,
				KeyValuePair<IDictionary<IList<NodeDeclNode>, bool>, int> candidate)
		{
			ISet<int> witnesses = new LinkedHashSet<int>();

			IList<IList<NodeDeclNode>> toBeMarkedKeys = new List<IList<NodeDeclNode>>();

			foreach(KeyValuePair<IList<NodeDeclNode>, bool> candidateMarkedMapEntry in candidate.Key.EntrySet())
			{
				// TODO also mark witness edge (and candidate as witness)
				if(!candidateMarkedMapEntry.Value.BooleanValue())
				{
					foreach(KeyValuePair<IDictionary<IList<NodeDeclNode>, bool>, int> witness in inducedEdgeMap.SetOfKeyValuePairs())
					{
						if(candidate.Key != witness.Key && candidate.Value != witness.Value)
						{
							// if witness contains edge
							if(witness.Key.ContainsKey(candidateMarkedMapEntry.Key))
							{
								// remember to mark Edge
								toBeMarkedKeys.Add(candidateMarkedMapEntry.Key);
								// add witness
								witnesses.Add(witness.Value);
							}
						}
					}
				}
			}

			foreach(IList<NodeDeclNode> toBeMarkedKey in toBeMarkedKeys)
			{
				Debug.Assert((candidate.Key.ContainsKey(toBeMarkedKey)));
				candidate.Key.Put(toBeMarkedKey, true);
			}

			return witnesses;
		}

		private static bool AllMarked(KeyValuePair<IDictionary<IList<NodeDeclNode>, bool>, int> candidate)
		{
			bool allMarked = true;

			foreach(bool edgeMarked in candidate.Key.Values)
				allMarked &= edgeMarked;

			return allMarked;
		}
	}

}
