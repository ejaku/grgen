/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.decl.executable;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.RhsDeclNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeNode;

import java.util.Collection;
import java.util.LinkedList;
import java.util.Queue;

/**
 * Base class for nested pattern matching related ast nodes
 */
public abstract class NestedMatcherDeclNode extends MatcherDeclNode
{
	public RhsDeclNode right;

	public NestedMatcherDeclNode(IdentNode id, TypeNode type, PatternGraphLhsNode left, RhsDeclNode right)
	{
		super(id, type, left);
		
		this.right = right;
		becomeParent(this.right);
	}
	
	@Override
	protected boolean checkLocal()
	{
		boolean nonActionIsOk = super.checkNonAction(right);
		boolean abstr = true;
		if(right != null)
			abstr = noAbstractElementInstantiatedNestedPattern(right);
		return nonActionIsOk & abstr;
	}
	
	protected boolean noAbstractElementInstantiatedNestedPattern(RhsDeclNode right)
	{
		boolean abstr = true;

nodeAbstrLoop:
		for(NodeDeclNode node : right.patternGraph.getNodes()) {
			if(!node.inheritsType() && node.getDeclType().isAbstract()) {
				if((node.context & CONTEXT_PARAMETER) == CONTEXT_PARAMETER) {
					continue;
				}
				for(PatternGraphLhsNode pattern = this.pattern; pattern != null; pattern = getParentPatternGraph(pattern)) {
					if(pattern.getNodes().contains(node)) {
						continue nodeAbstrLoop;
					}
				}
				error.error(node.getCoords(), "Instances of abstract nodes are not allowed");
				abstr = false;
			}
		}

edgeAbstrLoop:
		for(EdgeDeclNode edge : right.patternGraph.getEdges()) {
			if(!edge.inheritsType() && edge.getDeclType().isAbstract()) {
				if((edge.context & CONTEXT_PARAMETER) == CONTEXT_PARAMETER) {
					continue;
				}
				for(PatternGraphLhsNode pattern = this.pattern; pattern != null; pattern = getParentPatternGraph(pattern)) {
					if(pattern.getEdges().contains(edge)) {
						continue edgeAbstrLoop;
					}
				}
				error.error(edge.getCoords(), "Instances of abstract edges are not allowed");
				abstr = false;
			}
		}

		return abstr;
	}
	
	private static PatternGraphLhsNode getParentPatternGraph(PatternGraphLhsNode pattern)
	{
		if(pattern == null) {
			return null;
		}

		Queue<Collection<BaseNode>> queue = new LinkedList<Collection<BaseNode>>();
		for(Collection<BaseNode> parents = pattern.getParents(); parents != null; parents = queue.poll()) {
			for(BaseNode parent : parents) {
				if(parent instanceof PatternGraphLhsNode) {
					return (PatternGraphLhsNode)parent;
				}
				Collection<BaseNode> grandParents = parent.getParents();
				if(grandParents != null && !grandParents.isEmpty()) {
					queue.add(grandParents);
				}
			}
		}

		return null;
	}
}
