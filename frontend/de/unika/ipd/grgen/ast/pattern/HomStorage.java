/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * HomStorage.java
 *
 * @author Sebastian Buchwald (, Edgar Jakumeit)
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
	private Collection<Set<ConstraintDeclNode>> homSets = null;

	/** Map an edge to its homomorphic set. */
	private Map<EdgeDeclNode, Set<EdgeDeclNode>> edgeToHomEdges =
		new LinkedHashMap<EdgeDeclNode, Set<EdgeDeclNode>>();

	/** Map a node to its homomorphic set. */
	private Map<NodeDeclNode, Set<NodeDeclNode>> nodeToHomNodes =
		new LinkedHashMap<NodeDeclNode, Set<NodeDeclNode>>();


	// Don't call in PatternGraphNode constructor / until all pattern graphs were constructed
	// as it accesses the parents of the pattern graph
	public HomStorage(PatternGraphNode patternGraph)
	{
		initHomSets(patternGraph);
		initHomMaps(patternGraph);
	}

	private void initHomSets(PatternGraphNode patternGraph)
	{
		homSets = new LinkedHashSet<Set<ConstraintDeclNode>>();

		// Own homomorphic sets.
		for(HomNode homNode : patternGraph.homs.getChildren()) {
			homSets.addAll(splitHoms(patternGraph, homNode.getChildren()));
		}

		Set<NodeDeclNode> nodes = patternGraph.getNodes();
		Set<EdgeDeclNode> edges = patternGraph.getEdges();

		// Inherited homomorphic sets.
		for(PatternGraphNode parent = patternGraph.getParentPatternGraph(); parent != null;
				parent = parent.getParentPatternGraph()) {
			for(Set<ConstraintDeclNode> parentHomSet : parent.getHoms()) {
				addInheritedHomSet(parentHomSet, nodes, edges);
			}
		}
	}

	/**
	 * Split one hom statement into two parts, so deleted and reuse nodes/edges
	 * can't be matched homomorphically.
	 *
	 * This behavior is required for DPO-semantic.
	 * If the rule is not DPO the (casted) original homomorphic set is returned.
	 * Only homomorphic set with two or more entities will returned.
	 *
	 * @param homChildren Children of a HomNode
	 */
	private Set<Set<ConstraintDeclNode>> splitHoms(PatternGraphNode patternGraph,
			Collection<? extends BaseNode> homChildren)
	{
		Set<Set<ConstraintDeclNode>> ret = new LinkedHashSet<Set<ConstraintDeclNode>>();
		if(patternGraph.isIdentification()) {
			// homs between deleted entities
			HashSet<ConstraintDeclNode> deleteHomSet = new HashSet<ConstraintDeclNode>();
			// homs between reused entities
			HashSet<ConstraintDeclNode> reuseHomSet = new HashSet<ConstraintDeclNode>();

			for(BaseNode homChild : homChildren) {
				ConstraintDeclNode decl = (ConstraintDeclNode)homChild;
				Set<ConstraintDeclNode> deletedEntities = patternGraph.getRule().getDeletedElements();
				if(deletedEntities.contains(decl)) {
					deleteHomSet.add(decl);
				} else {
					reuseHomSet.add(decl);
				}
			}
			if(deleteHomSet.size() > 1) {
				ret.add(deleteHomSet);
			}
			if(reuseHomSet.size() > 1) {
				ret.add(reuseHomSet);
			}
			return ret;
		}

		Set<ConstraintDeclNode> homSet = new LinkedHashSet<ConstraintDeclNode>();

		for(BaseNode homChild : homChildren) {
			ConstraintDeclNode decl = (ConstraintDeclNode)homChild;
			homSet.add(decl);
		}
		if(homSet.size() > 1) {
			ret.add(homSet);
		}
		return ret;
	}

	private void addInheritedHomSet(Set<ConstraintDeclNode> parentHomSet,
			Set<NodeDeclNode> nodes, Set<EdgeDeclNode> edges)
	{
		Set<ConstraintDeclNode> inheritedHomSet = new LinkedHashSet<ConstraintDeclNode>();
		if(parentHomSet.iterator().next() instanceof NodeDeclNode) {
			for(ConstraintDeclNode homNode : parentHomSet) {
				if(nodes.contains(homNode)) {
					inheritedHomSet.add(homNode);
				}
			}
			if(inheritedHomSet.size() > 1) {
				homSets.add(inheritedHomSet);
			}
		} else {
			for(ConstraintDeclNode homEdge : parentHomSet) {
				if(edges.contains(homEdge)) {
					inheritedHomSet.add(homEdge);
				}
			}
			if(inheritedHomSet.size() > 1) {
				homSets.add(inheritedHomSet);
			}
		}
	}

	private void initHomMaps(PatternGraphNode patternGraph)
	{
		Collection<Set<ConstraintDeclNode>> homSets = getHoms();

		// Each node is homomorphic to itself.
		for(NodeDeclNode node : patternGraph.getNodes()) {
			Set<NodeDeclNode> homSet = new LinkedHashSet<NodeDeclNode>();
			homSet.add(node);
			nodeToHomNodes.put(node, homSet);
		}

		// Each edge is homomorphic to itself.
		for(EdgeDeclNode edge : patternGraph.getEdges()) {
			Set<EdgeDeclNode> homSet = new LinkedHashSet<EdgeDeclNode>();
			homSet.add(edge);
			edgeToHomEdges.put(edge, homSet);
		}

		for(Set<ConstraintDeclNode> homSet : homSets) {
			if(homSet.iterator().next() instanceof NodeDeclNode) {
				initNodeHomSet(homSet);
			} else {//if(homSet.iterator().next() instanceof EdgeDeclNode)
				initEdgeHomSet(homSet);
			}
		}
	}

	private void initNodeHomSet(Set<ConstraintDeclNode> homSet)
	{
		for(ConstraintDeclNode elem : homSet) {
			NodeDeclNode node = (NodeDeclNode)elem;
			Set<NodeDeclNode> mapEntry = nodeToHomNodes.get(node);
			for(ConstraintDeclNode homomorphicNode : homSet) {
				mapEntry.add((NodeDeclNode)homomorphicNode);
			}
		}
	}

	private void initEdgeHomSet(Set<ConstraintDeclNode> homSet)
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

		if(homSet == null) {
			// If the node isn't part of the pattern, return empty set.
			homSet = new LinkedHashSet<NodeDeclNode>();
		}

		return homSet;
	}

	/** Return the correspondent homomorphic set. */
	public Set<EdgeDeclNode> getHomomorphic(EdgeDeclNode edge)
	{
		Set<EdgeDeclNode> homSet = edgeToHomEdges.get(edge);

		if(homSet == null) {
			// If the edge isn't part of the pattern, return empty set.
			homSet = new LinkedHashSet<EdgeDeclNode>();
		}

		return homSet;
	}
}
