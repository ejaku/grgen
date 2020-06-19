/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * ImplicitNegComputerInduced.java
 *
 * @author Sebastian Buchwald, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
import de.unika.ipd.grgen.util.Pair;

/**
 * Class computing the implicit negative application conditions
 * that are used to implement the induced modifier
 */
public class ImplicitNegComputerInduced
{
	/** the pattern graph for which the implicit negatives are to be computed */
	PatternGraphNode patternGraph;
	
	/** All node pairs that need a double node NAC. */
	private Set<Pair<NodeDeclNode, NodeDeclNode>> nodePairsRequiringNeg =
		new LinkedHashSet<Pair<NodeDeclNode, NodeDeclNode>>();

	/** Map each pair of homomorphic sets of nodes to a set of edges (of the NAC). */
	private Map<Pair<Set<NodeDeclNode>, Set<NodeDeclNode>>, Set<ConnectionNode>> homNodePairsToEdges =
		new LinkedHashMap<Pair<Set<NodeDeclNode>, Set<NodeDeclNode>>, Set<ConnectionNode>>();

	// counts number of implicit double node negative patterns
	// created from pattern modifiers, in order to get unique negative names
	int implicitNegCounter = 0;

	
	public ImplicitNegComputerInduced(PatternGraphNode patternGraph)
	{
		this.patternGraph = patternGraph;
		
		if(patternGraph.isInduced()) {
			nodesRequiringPairNeg(patternGraph.getNodes());

			for(InducedNode induced : patternGraph.induceds.getChildren()) {
				induced.reportWarning("Induced statement occurs in induced pattern");
			}
			return;
		}

		Map<Set<NodeDeclNode>, Integer> generatedInducedSets = new LinkedHashMap<Set<NodeDeclNode>, Integer>();
		for(int i = 0; i < patternGraph.induceds.getChildren().size(); i++) {
			InducedNode induced = patternGraph.induceds.get(i);
			Set<NodeDeclNode> inducedNodes = induced.getInducedNodesSet();
			if(generatedInducedSets.containsKey(inducedNodes)) {
				InducedNode oldOcc = patternGraph.induceds.get(generatedInducedSets.get(inducedNodes).intValue());
				induced.reportWarning("Same induced statement also occurs at " + oldOcc.getCoords());
			} else {
				nodesRequiringPairNeg(inducedNodes);
				generatedInducedSets.put(inducedNodes, Integer.valueOf(i));
			}
		}

		warnRedundantInducedStatement(patternGraph.induceds, generatedInducedSets);
	}

	private void nodesRequiringPairNeg(Set<NodeDeclNode> nodes)
	{
		for(NodeDeclNode src : nodes) {
			if(src.isDummy())
				continue;

			for(NodeDeclNode tgt : nodes) {
				if(tgt.isDummy())
					continue;

				Pair<NodeDeclNode, NodeDeclNode> pair = new Pair<NodeDeclNode, NodeDeclNode>(src, tgt);
				nodePairsRequiringNeg.add(pair);

				Pair<Set<NodeDeclNode>, Set<NodeDeclNode>> key = new Pair<Set<NodeDeclNode>, Set<NodeDeclNode>>(
						patternGraph.getHomomorphic(src), patternGraph.getHomomorphic(tgt));

				if(!homNodePairsToEdges.containsKey(key)) {
					homNodePairsToEdges.put(key, new LinkedHashSet<ConnectionNode>());
				}
			}
		}
	}
	
	/**
	 * Get all implicit NACs.
	 * @return The Collection for the NACs.
	 */
	public LinkedList<PatternGraphLhs> getImplicitNegGraphs()
	{
		assert patternGraph.isResolved();
		
		LinkedList<PatternGraphLhs> implicitNegGraphs = new LinkedList<PatternGraphLhs>();

		// add existing edges to the corresponding pattern graph
		for(ConnectionCharacter connection : patternGraph.connections.getChildren()) {
			if(!(connection instanceof ConnectionNode))
				continue;
			
			ConnectionNode cn = (ConnectionNode)connection;

			Pair<Set<NodeDeclNode>, Set<NodeDeclNode>> key = new Pair<Set<NodeDeclNode>, Set<NodeDeclNode>>(
					patternGraph.getHomomorphic(cn.getSrc()), patternGraph.getHomomorphic(cn.getTgt()));

			Set<ConnectionNode> edges = homNodePairsToEdges.get(key);
			// edges == null if conn is a dangling edge or one of the nodes is not induced
			if(edges != null) {
				edges.add(cn);
				homNodePairsToEdges.put(key, edges);
			}
		}

		TypeDeclNode edgeRoot = patternGraph.getArbitraryEdgeRootTypeDecl();

		for(Pair<NodeDeclNode, NodeDeclNode> pair : nodePairsRequiringNeg) {
			NodeDeclNode src = pair.first;
			NodeDeclNode tgt = pair.second;

			if(src.getId().compareTo(tgt.getId()) > 0) {
				continue;
			}

			Pair<Set<NodeDeclNode>, Set<NodeDeclNode>> key = new Pair<Set<NodeDeclNode>, Set<NodeDeclNode>>(
					patternGraph.getHomomorphic(src), patternGraph.getHomomorphic(tgt));
			Pair<Set<NodeDeclNode>, Set<NodeDeclNode>> reverseKey = new Pair<Set<NodeDeclNode>, Set<NodeDeclNode>>(
					patternGraph.getHomomorphic(tgt), patternGraph.getHomomorphic(src));

			Set<ConnectionNode> edgeSet = homNodePairsToEdges.get(key);
			edgeSet.addAll(homNodePairsToEdges.get(reverseKey));

			PatternGraphLhs neg = new PatternGraphLhs("implnegind_" + implicitNegCounter, 0);
			++implicitNegCounter;
			neg.setDirectlyNestingLHSGraph(neg);

			// add edges to the NAC
			Set<EdgeDeclNode> allNegEdges = new LinkedHashSet<EdgeDeclNode>();
			Set<NodeDeclNode> allNegNodes = new LinkedHashSet<NodeDeclNode>();
			for(ConnectionNode conn : edgeSet) {
				conn.addToGraph(neg);

				allNegEdges.add(conn.getEdge());
				allNegNodes.add(conn.getSrc());
				allNegNodes.add(conn.getTgt());
			}

			addInheritedHomSet(neg, allNegEdges, allNegNodes);

			// add another edge of type edgeRoot to the NAC
			EdgeDeclNode edge = patternGraph.getAnonymousEdgeDecl(edgeRoot, patternGraph.context);

			ConnectionCharacter conn = new ConnectionNode(src, edge, tgt,
					ConnectionNode.ConnectionKind.ARBITRARY, patternGraph);

			conn.addToGraph(neg);

			implicitNegGraphs.add(neg);
		}
		
		return implicitNegGraphs;
	}

	/**
	 * Add all necessary homomorphic sets to a NAC.
	 *
	 * If an edge a-e->b is homomorphic to another edge c-f->d f only added if
	 * a is homomorphic to c and b is homomorphic to d.
	 */
	private void addInheritedHomSet(PatternGraphLhs neg, Set<EdgeDeclNode> allNegEdges, Set<NodeDeclNode> allNegNodes)
	{
		// inherit homomorphic nodes
		for(NodeDeclNode node : allNegNodes) {
			Set<Node> homSet = new LinkedHashSet<Node>();
			Set<NodeDeclNode> homNodes = patternGraph.getHomomorphic(node);

			for(NodeDeclNode homNode : homNodes) {
				if(allNegNodes.contains(homNode)) {
					homSet.add(homNode.checkIR(Node.class));
				}
			}
			if(homSet.size() > 1) {
				neg.addHomomorphicNodes(homSet);
			}
		}

		// inherit homomorphic edges
		for(EdgeDeclNode edge : allNegEdges) {
			Set<Edge> homSet = new LinkedHashSet<Edge>();
			Set<EdgeDeclNode> homEdges = patternGraph.getHomomorphic(edge);

			for(EdgeDeclNode homEdge : homEdges) {
				if(allNegEdges.contains(homEdge)) {
					homSet.add(homEdge.checkIR(Edge.class));
				}
			}
			if(homSet.size() > 1) {
				neg.addHomomorphicEdges(homSet);
			}
		}
	}
	
	/**
	 * warn if an induced statement is redundant.
	 *
	 * Algorithm:
	 * Input: Sets V_i of nodes
	 * for each V_i
	 *   K_i = all pairs of nodes of V_i
	 * for each i
	 *   for each k_i of K_i
	 *     for each K_j
	 *       if k_i \in K_j: mark k_i
	 *   if all k_i marked: warn
	 *
	 * @param generatedInducedSets Set of all induced statements
	 */
	private static void warnRedundantInducedStatement(CollectNode<InducedNode> induceds,
			Map<Set<NodeDeclNode>, Integer> generatedInducedSets)
	{
		Map<Map<List<NodeDeclNode>, Boolean>, Integer> inducedEdgeMap =
				new LinkedHashMap<Map<List<NodeDeclNode>, Boolean>, Integer>();

		// create all pairs of nodes (->edges)
		for(Map.Entry<Set<NodeDeclNode>, Integer> nodeMapEntry : generatedInducedSets.entrySet()) {
			fillInducedEdgeMap(inducedEdgeMap, nodeMapEntry);
		}

		for(Map.Entry<Map<List<NodeDeclNode>, Boolean>, Integer> candidate : inducedEdgeMap.entrySet()) {
			Set<Integer> witnesses = getWitnessesAndMarkEdge(inducedEdgeMap, candidate);

			// all edges marked?
			if(allMarked(candidate)) {
				String witnessesLoc = "";
				for(Integer index : witnesses) {
					witnessesLoc += induceds.get(index.intValue()).getCoords() + " ";
				}
				witnessesLoc = witnessesLoc.trim();
				induceds.get(candidate.getValue().intValue()).reportWarning(
						"Induced statement is redundant, since covered by statement(s) at " + witnessesLoc);
			}
		}
	}
	
	private static void fillInducedEdgeMap(Map<Map<List<NodeDeclNode>, Boolean>, Integer> inducedEdgeMap,
			Map.Entry<Set<NodeDeclNode>, Integer> nodeMapEntry)
	{
		// if the Boolean in markedMap is true -> edge is marked
		Map<List<NodeDeclNode>, Boolean> markedMap = new LinkedHashMap<List<NodeDeclNode>, Boolean>();

		for(NodeDeclNode src : nodeMapEntry.getKey()) {
			for(NodeDeclNode tgt : nodeMapEntry.getKey()) {
				List<NodeDeclNode> edge = new LinkedList<NodeDeclNode>();
				edge.add(src);
				edge.add(tgt);

				markedMap.put(edge, Boolean.valueOf(false));
			}
		}

		inducedEdgeMap.put(markedMap, nodeMapEntry.getValue());
	}

	private static Set<Integer> getWitnessesAndMarkEdge(Map<Map<List<NodeDeclNode>, Boolean>, Integer> inducedEdgeMap,
			Map.Entry<Map<List<NodeDeclNode>, Boolean>, Integer> candidate)
	{
		Set<Integer> witnesses = new LinkedHashSet<Integer>();

		for(Map.Entry<List<NodeDeclNode>, Boolean> candidateMarkedMap : candidate.getKey().entrySet()) {
			// TODO also mark witness edge (and candidate as witness)
			if(!candidateMarkedMap.getValue().booleanValue()) {
				for(Map.Entry<Map<List<NodeDeclNode>, Boolean>, Integer> witness : inducedEdgeMap.entrySet()) {
					if(candidate != witness) {
						// if witness contains edge
						if(witness.getKey().containsKey(candidateMarkedMap.getKey())) {
							// mark Edge
							candidateMarkedMap.setValue(Boolean.valueOf(true));
							// add witness
							witnesses.add(witness.getValue());
						}
					}
				}
			}
		}
		
		return witnesses;
	}

	private static boolean allMarked(Map.Entry<Map<List<NodeDeclNode>, Boolean>, Integer> candidate)
	{
		boolean allMarked = true;
		
		for(boolean edgeMarked : candidate.getKey().values()) {
			allMarked &= edgeMarked;
		}
		
		return allMarked;
	}
}
