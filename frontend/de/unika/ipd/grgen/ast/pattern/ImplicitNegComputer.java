/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * ImplicitNegComputer.java
 *
 * @author Sebastian Buchwald, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;

/**
 * Class computing the implicit negative application conditions
 * that are used to implement the exact and dpo (dangling+identification) modifiers
 */
public class ImplicitNegComputer
{
	/** the pattern graph for which the implicit negatives are to be computed */
	PatternGraphLhsNode patternGraph;
	
	/** All nodes that need a single node NAC. */
	private Set<NodeDeclNode> nodesRequiringNeg =
		new LinkedHashSet<NodeDeclNode>();

	/** Map a homomorphic set to a set of edges (of the NAC). */
	private Map<Set<NodeDeclNode>, Set<ConnectionNode>> homNodesToEdges =
		new LinkedHashMap<Set<NodeDeclNode>, Set<ConnectionNode>>();

	// counts number of implicit single node negative patterns
	// created from pattern modifiers, in order to get unique negative names
	int implicitNegCounter = 0;

	
	public ImplicitNegComputer(PatternGraphLhsNode patternGraph)
	{
		this.patternGraph = patternGraph;
		
		if(patternGraph.isExact()) {
			nodesRequireNeg(patternGraph.getNodes());

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
			nodesRequireNeg(getDpoPatternNodes(deletedNodes));

			for(ExactNode exact : patternGraph.exacts.getChildren()) {
				for(NodeDeclNode exactNode : exact.getExactNodes()) {
					if(deletedNodes.contains(exactNode)) {
						exact.reportWarning("Exact statement for " + exactNode.getKind() + " "
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
					exact.reportWarning(exactNode.getKind() + " "
							+ exactNode.getIdentNode().getSymbol().getText()
							+ " already occurs in exact statement at "
							+ patternGraph.exacts.get(generatedExactNodes.get(exactNode).intValue()).getCoords());
				} else {
					generatedExactNodes.put(exactNode, Integer.valueOf(i));
				}
			}
		}

		nodesRequireNeg(generatedExactNodes.keySet());
	}

	private void nodesRequireNeg(Set<NodeDeclNode> nodes)
	{
		for(NodeDeclNode node : nodes) {
			if(node.isDummy())
				continue;

			nodesRequiringNeg.add(node);
			Set<NodeDeclNode> homSet = patternGraph.getHomomorphic(node);
			if(!homNodesToEdges.containsKey(homSet)) {
				Set<ConnectionNode> edgeSet = new HashSet<ConnectionNode>();
				homNodesToEdges.put(homSet, edgeSet);
			}
		}
	}

	/**
	 * Return the set of nodes needed for the singleNodeNegMap if the whole pattern is dpo.
	 */
	private static Set<NodeDeclNode> getDpoPatternNodes(Set<ConstraintDeclNode> deletedEntities)
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
	 * Get all implicit NACs.
	 * @return The Collection for the NACs.
	 */
	public LinkedList<PatternGraphLhs> getImplicitNegGraphs()
	{
		assert patternGraph.isResolved();

		LinkedList<PatternGraphLhs> implicitNegGraphs = new LinkedList<PatternGraphLhs>();

		// add existing edges to the corresponding sets
		for(ConnectionCharacter connection : patternGraph.connections.getChildren()) {
			if(!(connection instanceof ConnectionNode))
				continue;
			
			ConnectionNode cn = (ConnectionNode)connection;
			NodeDeclNode src = cn.getSrc();
			if(nodesRequiringNeg.contains(src)) {
				Set<NodeDeclNode> homSet = patternGraph.getHomomorphic(src);
				Set<ConnectionNode> edges = homNodesToEdges.get(homSet);
				edges.add(cn);
				homNodesToEdges.put(homSet, edges);
			}
			NodeDeclNode tgt = cn.getTgt();
			if(nodesRequiringNeg.contains(tgt)) {
				Set<NodeDeclNode> homSet = patternGraph.getHomomorphic(tgt);
				Set<ConnectionNode> edges = homNodesToEdges.get(homSet);
				edges.add(cn);
				homNodesToEdges.put(homSet, edges);
			}
		}

		TypeDeclNode edgeRoot = patternGraph.getArbitraryEdgeRootTypeDecl();
		TypeDeclNode nodeRoot = patternGraph.getNodeRootTypeDecl();

		// generate and add pattern graphs
		for(NodeDeclNode nodeRequiringNeg : nodesRequiringNeg) {
			//for (int direction = INCOMING; direction <= OUTGOING; direction++) {
			Set<ConnectionNode> edgeSet = homNodesToEdges.get(patternGraph.getHomomorphic(nodeRequiringNeg));
			
			PatternGraphLhs neg = new PatternGraphLhs("implneg_" + implicitNegCounter, 0);
			++implicitNegCounter;
			neg.setDirectlyNestingLHSGraph(neg);

			// add edges to NAC
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
			NodeDeclNode dummyNode = patternGraph.getAnonymousDummyNode(nodeRoot, patternGraph.context);

			ConnectionNode conn = new ConnectionNode(nodeRequiringNeg, edge, dummyNode,
					ConnectionNode.ConnectionKind.ARBITRARY, patternGraph);
			conn.addToGraph(neg);

			implicitNegGraphs.add(neg);
			//}
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
}
