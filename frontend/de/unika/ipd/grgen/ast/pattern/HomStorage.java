/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * HomStorage.java
 *
 * @author Sebastian Buchwald, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.Collection;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;

/**
 * AST node that stores node/edge is-homomorphic-to information
 */
public class HomStorage
{
	/** Stores the sets of homomorphic elements (not equivalent to the contents of the hom statements) */
	private Collection<Set<ConstraintDeclNode>> homSets =
		new LinkedHashSet<Set<ConstraintDeclNode>>();

	/** Map an edge to its homomorphic set. */
	private Map<EdgeDeclNode, Set<EdgeDeclNode>> edgeToHomEdges =
		new LinkedHashMap<EdgeDeclNode, Set<EdgeDeclNode>>();

	/** Map a node to its homomorphic set. */
	private Map<NodeDeclNode, Set<NodeDeclNode>> nodeToHomNodes =
		new LinkedHashMap<NodeDeclNode, Set<NodeDeclNode>>();
	
	private Set<NodeDeclNode> emptyHomNodeSet = new LinkedHashSet<NodeDeclNode>();
	private Set<EdgeDeclNode> emptyHomEdgeSet = new LinkedHashSet<EdgeDeclNode>();


	// Don't call in PatternGraphNode constructor / until all pattern graphs were constructed
	// as it accesses the parents of the pattern graph
	public HomStorage(PatternGraphLhsNode patternGraph)
	{
		// fill with own homomorphic sets
		if(patternGraph.isIdentification()) {
			// Split one hom statement into two parts, so deleted and reuse nodes/edges can't be matched homomorphically.
			// This behavior is required for DPO-semantic / more exactly the identification condition.
			Set<ConstraintDeclNode> deletedEntities = patternGraph.getRule().getDeletedElements();
			for(HomNode homNode : patternGraph.homs.getChildren()) {
				Set<ConstraintDeclNode> deleteHomSet = getDeleteHomSet(homNode.getChildren(), deletedEntities);
				addIfNonTrivialHomSet(homSets, deleteHomSet);
				Set<ConstraintDeclNode> reuseHomSet = getReuseHomSet(homNode.getChildren(), deletedEntities);
				addIfNonTrivialHomSet(homSets, reuseHomSet);
			}
		} else {
			for(HomNode homNode : patternGraph.homs.getChildren()) {
				Set<ConstraintDeclNode> homSet = getHomSet(homNode.getChildren());
				addIfNonTrivialHomSet(homSets, homSet);
			}
		}

		// then add inherited homomorphic sets
		for(PatternGraphLhsNode parent = patternGraph.getParentPatternGraph(); parent != null;
				parent = parent.getParentPatternGraph()) {
			for(Set<ConstraintDeclNode> parentHomSet : parent.getHoms()) {
				Set<ConstraintDeclNode> inheritedHomSet = getInheritedHomSet(parentHomSet, 
						patternGraph.getNodes(), patternGraph.getEdges());
				addIfNonTrivialHomSet(homSets, inheritedHomSet);
			}
		}

		initElementsToHomElements(patternGraph.getNodes(), patternGraph.getEdges());
	}

	private static Set<ConstraintDeclNode> getDeleteHomSet(Collection<? extends BaseNode> homChildren,
			Set<ConstraintDeclNode> deletedElements)
	{
		// homs between deleted entities
		HashSet<ConstraintDeclNode> deleteHomSet = new HashSet<ConstraintDeclNode>();

		for(BaseNode homChild : homChildren) {
			ConstraintDeclNode decl = (ConstraintDeclNode)homChild;
			if(deletedElements.contains(decl)) {
				deleteHomSet.add(decl);
			}
		}
		
		return deleteHomSet;
	}

	private static Set<ConstraintDeclNode> getReuseHomSet(Collection<? extends BaseNode> homChildren,
			Set<ConstraintDeclNode> deletedElements)
	{
		// homs between reused entities
		HashSet<ConstraintDeclNode> reuseHomSet = new HashSet<ConstraintDeclNode>();

		for(BaseNode homChild : homChildren) {
			ConstraintDeclNode decl = (ConstraintDeclNode)homChild;
			if(!deletedElements.contains(decl)) {
				reuseHomSet.add(decl);
			}
		}

		return reuseHomSet;
	}

	private static Set<ConstraintDeclNode> getHomSet(Collection<? extends BaseNode> homChildren)
	{
		// simply the entities from the hom statements
		Set<ConstraintDeclNode> homSet = new LinkedHashSet<ConstraintDeclNode>();

		for(BaseNode homChild : homChildren) {
			ConstraintDeclNode decl = (ConstraintDeclNode)homChild;
			homSet.add(decl);
		}

		return homSet;
	}

	private static Set<ConstraintDeclNode> getInheritedHomSet(Set<ConstraintDeclNode> parentHomSet,
			Set<NodeDeclNode> nodes, Set<EdgeDeclNode> edges)
	{
		Set<ConstraintDeclNode> inheritedHomSet = new LinkedHashSet<ConstraintDeclNode>();
		
		if(parentHomSet.iterator().next() instanceof NodeDeclNode) {
			for(ConstraintDeclNode homNode : parentHomSet) {
				if(nodes.contains(homNode)) {
					inheritedHomSet.add(homNode);
				}
			}
		} else {
			for(ConstraintDeclNode homEdge : parentHomSet) {
				if(edges.contains(homEdge)) {
					inheritedHomSet.add(homEdge);
				}
			}
		}
		
		return inheritedHomSet;
	}

	private static void addIfNonTrivialHomSet(Collection<Set<ConstraintDeclNode>> collectionToAddTo,
			Set<ConstraintDeclNode> setToAdd)
	{
		if(setToAdd.size() > 1) {
			collectionToAddTo.add(setToAdd);
		}
	}

	private void initElementsToHomElements(Set<NodeDeclNode> nodes, Set<EdgeDeclNode> edges)
	{
		// Each node is homomorphic to itself (trivial hom).
		for(NodeDeclNode node : nodes) {
			Set<NodeDeclNode> homSet = new LinkedHashSet<NodeDeclNode>();
			homSet.add(node);
			nodeToHomNodes.put(node, homSet);
		}

		// Each edge is homomorphic to itself (trivial hom).
		for(EdgeDeclNode edge : edges) {
			Set<EdgeDeclNode> homSet = new LinkedHashSet<EdgeDeclNode>();
			homSet.add(edge);
			edgeToHomEdges.put(edge, homSet);
		}

		for(Set<ConstraintDeclNode> homSet : homSets) {
			if(homSet.iterator().next() instanceof NodeDeclNode) {
				fillHomNodesInNodesToHomNodes(homSet);
			} else {//if(homSet.iterator().next() instanceof EdgeDeclNode)
				fillHomEdgesInEdgesToHomEdges(homSet);
			}
		}
	}

	private void fillHomNodesInNodesToHomNodes(Set<ConstraintDeclNode> homSet)
	{
		for(ConstraintDeclNode elem : homSet) {
			NodeDeclNode node = (NodeDeclNode)elem;
			Set<NodeDeclNode> mapEntry = nodeToHomNodes.get(node);
			for(ConstraintDeclNode homomorphicNode : homSet) {
				mapEntry.add((NodeDeclNode)homomorphicNode);
			}
		}
	}

	private void fillHomEdgesInEdgesToHomEdges(Set<ConstraintDeclNode> homSet)
	{
		for(ConstraintDeclNode elem : homSet) {
			EdgeDeclNode edge = (EdgeDeclNode)elem;
			Set<EdgeDeclNode> mapEntry = edgeToHomEdges.get(edge);
			for(ConstraintDeclNode homomorphicEdge : homSet) {
				mapEntry.add((EdgeDeclNode)homomorphicEdge);
			}
		}
	}

	public Collection<Set<ConstraintDeclNode>> getHoms()
	{
		return homSets;
	}
	
	/** Return the correspondent homomorphic set. */
	public Set<NodeDeclNode> getHomomorphic(NodeDeclNode node)
	{
		Set<NodeDeclNode> homSet = nodeToHomNodes.get(node);

		// If the node isn't part of the pattern, return empty set.
		if(homSet == null)
			return emptyHomNodeSet;
		else
			return homSet;
	}

	/** Return the correspondent homomorphic set. */
	public Set<EdgeDeclNode> getHomomorphic(EdgeDeclNode edge)
	{
		Set<EdgeDeclNode> homSet = edgeToHomEdges.get(edge);

		// If the edge isn't part of the pattern, return empty set.
		if(homSet == null)
			return emptyHomEdgeSet;
		else
			return homSet;
	}	
}
