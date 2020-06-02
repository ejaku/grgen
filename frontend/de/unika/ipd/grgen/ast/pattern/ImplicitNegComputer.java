/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * ImplicitNegComputer.java
 *
 * @author Sebastian Buchwald (, Edgar Jakumeit)
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraph;
import de.unika.ipd.grgen.util.Pair;

/**
 * Class computing the implicit negative application conditions
 * that are used to implement the induced, exact, dpo (dangling+identification) modifiers
 */
public class ImplicitNegComputer
{
	/** the pattern graph for which the implicit negatives are to be computed */
	PatternGraphNode patternGraph;
	
	/** All nodes that needed a single node NAC. */
	private Set<NodeDeclNode> singleNodeNegNodes =
		new LinkedHashSet<NodeDeclNode>();

	/** All node pairs that needed a double node NAC. */
	private Set<Pair<NodeDeclNode, NodeDeclNode>> doubleNodeNegPairs =
		new LinkedHashSet<Pair<NodeDeclNode, NodeDeclNode>>();

	/** Map a homomorphic set to a set of edges (of the NAC). */
	private Map<Set<NodeDeclNode>, Set<ConnectionNode>> singleNodeNegMap =
		new LinkedHashMap<Set<NodeDeclNode>, Set<ConnectionNode>>();

	/** Map each pair of homomorphic sets of nodes to a set of edges (of the NAC). */
	private Map<Pair<Set<NodeDeclNode>, Set<NodeDeclNode>>, Set<ConnectionNode>> doubleNodeNegMap =
		new LinkedHashMap<Pair<Set<NodeDeclNode>, Set<NodeDeclNode>>, Set<ConnectionNode>>();

	// counts number of implicit single and double node negative patterns
	// created from pattern modifiers, in order to get unique negative names
	int implicitNegCounter = 0;

	
	public ImplicitNegComputer(PatternGraphNode patternGraph)
	{
		this.patternGraph = patternGraph;
	}
	
	/**
	 * Get all implicit NACs.
	 * @return The Collection for the NACs.
	 */
	public LinkedList<PatternGraph> getImplicitNegGraphs()
	{
		LinkedList<PatternGraph> implicitNegGraphs = new LinkedList<PatternGraph>();

		initDoubleNodeNegMap();
		implicitNegGraphs.addAll(getDoubleNodeNegGraphs());

		initSingleNodeNegMap();
		implicitNegGraphs.addAll(getSingleNodeNegGraphs());

		return implicitNegGraphs;
	}

	private void initDoubleNodeNegMap()
	{
		if(patternGraph.isInduced()) {
			addToDoubleNodeMap(patternGraph.getNodes());

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
				InducedNode oldOcc = patternGraph.induceds.get(generatedInducedSets.get(inducedNodes));
				induced.reportWarning("Same induced statement also occurs at " + oldOcc.getCoords());
			} else {
				addToDoubleNodeMap(inducedNodes);
				generatedInducedSets.put(inducedNodes, i);
			}
		}

		warnRedundantInducedStatement(generatedInducedSets);
	}

	private LinkedList<PatternGraph> getDoubleNodeNegGraphs()
	{
		assert patternGraph.isResolved();
		
		LinkedList<PatternGraph> implicitNegGraphs = new LinkedList<PatternGraph>();

		// add existing edges to the corresponding pattern graph
		for(BaseNode connection : patternGraph.connections.getChildren()) {
			if(connection instanceof ConnectionNode) {
				ConnectionNode cn = (ConnectionNode)connection;

				Pair<Set<NodeDeclNode>, Set<NodeDeclNode>> key = new Pair<Set<NodeDeclNode>, Set<NodeDeclNode>>();
				key.first = patternGraph.getHomomorphic(cn.getSrc());
				key.second = patternGraph.getHomomorphic(cn.getTgt());

				Set<ConnectionNode> edges = doubleNodeNegMap.get(key);
				// edges == null if conn is a dangling edge or one of the nodes
				// is not induced
				if(edges != null) {
					edges.add(cn);
					doubleNodeNegMap.put(key, edges);
				}
			}
		}

		TypeDeclNode edgeRoot = patternGraph.getArbitraryEdgeRootTypeDecl();

		for(Pair<NodeDeclNode, NodeDeclNode> pair : doubleNodeNegPairs) {
			NodeDeclNode src = pair.first;
			NodeDeclNode tgt = pair.second;

			if(src.getId().compareTo(tgt.getId()) > 0) {
				continue;
			}

			Pair<Set<NodeDeclNode>, Set<NodeDeclNode>> key = new Pair<Set<NodeDeclNode>, Set<NodeDeclNode>>();
			key.first = patternGraph.getHomomorphic(src);
			key.second = patternGraph.getHomomorphic(tgt);
			Pair<Set<NodeDeclNode>, Set<NodeDeclNode>> key2 = new Pair<Set<NodeDeclNode>, Set<NodeDeclNode>>();
			key2.first = patternGraph.getHomomorphic(tgt);
			key2.second = patternGraph.getHomomorphic(src);
			Set<EdgeDeclNode> allNegEdges = new LinkedHashSet<EdgeDeclNode>();
			Set<NodeDeclNode> allNegNodes = new LinkedHashSet<NodeDeclNode>();
			Set<ConnectionNode> edgeSet = doubleNodeNegMap.get(key);
			edgeSet.addAll(doubleNodeNegMap.get(key2));

			PatternGraph neg = new PatternGraph("implneg_" + implicitNegCounter, 0);
			++implicitNegCounter;
			neg.setDirectlyNestingLHSGraph(neg);

			// add edges to the NAC
			for(ConnectionNode conn : edgeSet) {
				conn.addToGraph(neg);

				allNegEdges.add(conn.getEdge());
				allNegNodes.add(conn.getSrc());
				allNegNodes.add(conn.getTgt());
			}

			addInheritedHomSet(neg, allNegEdges, allNegNodes);

			// add another edge of type edgeRoot to the NAC
			EdgeDeclNode edge = patternGraph.getAnonymousEdgeDecl(edgeRoot, patternGraph.context);

			ConnectionCharacter conn = new ConnectionNode(src, edge, tgt, ConnectionNode.ARBITRARY, patternGraph);

			conn.addToGraph(neg);

			implicitNegGraphs.add(neg);
		}
		
		return implicitNegGraphs;
	}

	private void initSingleNodeNegMap()
	{
		if(patternGraph.isExact()) {
			addToSingleNodeMap(patternGraph.getNodes());

			if(patternGraph.isDangling() && !patternGraph.isIdentification()) {
				patternGraph.reportWarning("The keyword \"dangling\" is redundant for exact patterns");
			}

			for(ExactNode exact : patternGraph.exacts.getChildren()) {
				exact.reportWarning("Exact statement occurs in exact pattern");
			}

			return;
		}

		if(patternGraph.isDangling()) {
			Set<ConstraintDeclNode> deletedNodes = patternGraph.getRule().getDeletedElements();
			addToSingleNodeMap(getDpoPatternNodes(deletedNodes));

			for(ExactNode exact : patternGraph.exacts.getChildren()) {
				for(NodeDeclNode exactNode : exact.getExactNodes()) {
					if(deletedNodes.contains(exactNode)) {
						exact.reportWarning("Exact statement for " + exactNode.getUseString() + " "
								+ exactNode.getIdentNode().getSymbol().getText()
								+ " is redundant, since the pattern is declared \"dangling\" or \"dpo\"");
					}
				}
			}
		}

		Map<NodeDeclNode, Integer> generatedExactNodes = new LinkedHashMap<NodeDeclNode, Integer>();		
		for(int i = 0; i < patternGraph.exacts.getChildren().size(); i++) { // exact Statements
			ExactNode exact = patternGraph.exacts.get(i);
			for(NodeDeclNode exactNode : exact.getExactNodes()) {
				// coords of occurrence are not available
				if(generatedExactNodes.containsKey(exactNode)) {
					exact.reportWarning(exactNode.getUseString() + " "
							+ exactNode.getIdentNode().getSymbol().getText()
							+ " already occurs in exact statement at "
							+ patternGraph.exacts.get(generatedExactNodes.get(exactNode)).getCoords());
				} else {
					generatedExactNodes.put(exactNode, i);
				}
			}
		}

		addToSingleNodeMap(generatedExactNodes.keySet());
	}

	private LinkedList<PatternGraph> getSingleNodeNegGraphs()
	{
		assert patternGraph.isResolved();

		LinkedList<PatternGraph> implicitNegGraphs = new LinkedList<PatternGraph>();

		// add existing edges to the corresponding sets
		for(BaseNode connection : patternGraph.connections.getChildren()) {
			if(connection instanceof ConnectionNode) {
				ConnectionNode cn = (ConnectionNode)connection;
				NodeDeclNode src = cn.getSrc();
				if(singleNodeNegNodes.contains(src)) {
					Set<NodeDeclNode> homSet = patternGraph.getHomomorphic(src);
					Set<ConnectionNode> edges = singleNodeNegMap.get(homSet);
					edges.add(cn);
					singleNodeNegMap.put(homSet, edges);
				}
				NodeDeclNode tgt = cn.getTgt();
				if(singleNodeNegNodes.contains(tgt)) {
					Set<NodeDeclNode> homSet = patternGraph.getHomomorphic(tgt);
					Set<ConnectionNode> edges = singleNodeNegMap.get(homSet);
					edges.add(cn);
					singleNodeNegMap.put(homSet, edges);
				}
			}
		}

		TypeDeclNode edgeRoot = patternGraph.getArbitraryEdgeRootTypeDecl();
		TypeDeclNode nodeRoot = patternGraph.getNodeRootTypeDecl();

		// generate and add pattern graphs
		for(NodeDeclNode singleNodeNegNode : singleNodeNegNodes) {
			//for (int direction = INCOMING; direction <= OUTGOING; direction++) {
			Set<EdgeDeclNode> allNegEdges = new LinkedHashSet<EdgeDeclNode>();
			Set<NodeDeclNode> allNegNodes = new LinkedHashSet<NodeDeclNode>();
			Set<ConnectionNode> edgeSet = singleNodeNegMap.get(patternGraph.getHomomorphic(singleNodeNegNode));
			PatternGraph neg = new PatternGraph("implneg_" + implicitNegCounter, 0);
			++implicitNegCounter;
			neg.setDirectlyNestingLHSGraph(neg);

			// add edges to NAC
			for(ConnectionNode conn : edgeSet) {
				conn.addToGraph(neg);

				allNegEdges.add(conn.getEdge());
				allNegNodes.add(conn.getSrc());
				allNegNodes.add(conn.getTgt());
			}

			addInheritedHomSet(neg, allNegEdges, allNegNodes);

			// add another edge of type edgeRoot to the NAC
			EdgeDeclNode edge = patternGraph.getAnonymousEdgeDecl(edgeRoot, patternGraph.context);
			NodeDeclNode dummyNode = patternGraph.getAnonymousDummyNode(nodeRoot, patternGraph.context);

			ConnectionNode conn = new ConnectionNode(singleNodeNegNode, edge, dummyNode, ConnectionNode.ARBITRARY, patternGraph);
			conn.addToGraph(neg);

			implicitNegGraphs.add(neg);
			//}
		}
		
		return implicitNegGraphs;
	}

	/**
	 * Add a set of nodes to the singleNodeMap.
	 *
	 * @param nodes Set of Nodes.
	 */
	private void addToSingleNodeMap(Set<NodeDeclNode> nodes)
	{
		for(NodeDeclNode node : nodes) {
			if(node.isDummy())
				continue;

			singleNodeNegNodes.add(node);
			Set<NodeDeclNode> homSet = patternGraph.getHomomorphic(node);
			if(!singleNodeNegMap.containsKey(homSet)) {
				Set<ConnectionNode> edgeSet = new HashSet<ConnectionNode>();
				singleNodeNegMap.put(homSet, edgeSet);
			}
		}
	}

	private void addToDoubleNodeMap(Set<NodeDeclNode> nodes)
	{
		for(NodeDeclNode src : nodes) {
			if(src.isDummy())
				continue;

			for(NodeDeclNode tgt : nodes) {
				if(tgt.isDummy())
					continue;

				Pair<NodeDeclNode, NodeDeclNode> pair = new Pair<NodeDeclNode, NodeDeclNode>();
				pair.first = src;
				pair.second = tgt;
				doubleNodeNegPairs.add(pair);

				Pair<Set<NodeDeclNode>, Set<NodeDeclNode>> key = new Pair<Set<NodeDeclNode>, Set<NodeDeclNode>>();
				key.first = patternGraph.getHomomorphic(src);
				key.second = patternGraph.getHomomorphic(tgt);

				if(!doubleNodeNegMap.containsKey(key)) {
					Set<ConnectionNode> edges = new LinkedHashSet<ConnectionNode>();
					doubleNodeNegMap.put(key, edges);
				}
			}
		}
	}

	/**
	 * Return the set of nodes needed for the singleNodeNegMap if the whole
	 * pattern is dpo.
	 */
	private Set<NodeDeclNode> getDpoPatternNodes(Set<ConstraintDeclNode> deletedEntities)
	{
		Set<NodeDeclNode> deletedNodes = new LinkedHashSet<NodeDeclNode>();

		for(DeclNode declNode : deletedEntities) {
			if(declNode instanceof NodeDeclNode) {
				NodeDeclNode node = (NodeDeclNode)declNode;
				if(!node.isDummy()) {
					deletedNodes.add(node);
				}
			}
		}

		return deletedNodes;
	}
	
	/**
	 * Add all necessary homomorphic sets to a NAC.
	 *
	 * If an edge a-e->b is homomorphic to another edge c-f->d f only added if
	 * a is homomorphic to c and b is homomorphic to d.
	 */
	private void addInheritedHomSet(PatternGraph neg, Set<EdgeDeclNode> allNegEdges, Set<NodeDeclNode> allNegNodes)
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
	private void warnRedundantInducedStatement(Map<Set<NodeDeclNode>, Integer> generatedInducedSets)
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
					witnessesLoc += patternGraph.induceds.get(index).getCoords() + " ";
				}
				witnessesLoc = witnessesLoc.trim();
				patternGraph.induceds.get(candidate.getValue()).reportWarning(
						"Induced statement is redundant, since covered by statement(s) at " + witnessesLoc);
			}
		}
	}
	
	private void fillInducedEdgeMap(Map<Map<List<NodeDeclNode>, Boolean>, Integer> inducedEdgeMap,
			Map.Entry<Set<NodeDeclNode>, Integer> nodeMapEntry)
	{
		// if the Boolean in markedMap is true -> edge is marked
		Map<List<NodeDeclNode>, Boolean> markedMap = new LinkedHashMap<List<NodeDeclNode>, Boolean>();

		for(NodeDeclNode src : nodeMapEntry.getKey()) {
			for(NodeDeclNode tgt : nodeMapEntry.getKey()) {
				List<NodeDeclNode> edge = new LinkedList<NodeDeclNode>();
				edge.add(src);
				edge.add(tgt);

				markedMap.put(edge, false);
			}
		}

		inducedEdgeMap.put(markedMap, nodeMapEntry.getValue());
	}

	private Set<Integer> getWitnessesAndMarkEdge(Map<Map<List<NodeDeclNode>, Boolean>, Integer> inducedEdgeMap,
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
							candidateMarkedMap.setValue(true);
							// add witness
							witnesses.add(witness.getValue());
						}
					}
				}
			}
		}
		
		return witnesses;
	}

	private boolean allMarked(Map.Entry<Map<List<NodeDeclNode>, Boolean>, Integer> candidate)
	{
		boolean allMarked = true;
		
		for(boolean edgeMarked : candidate.getKey().values()) {
			allMarked &= edgeMarked;
		}
		
		return allMarked;
	}
}
