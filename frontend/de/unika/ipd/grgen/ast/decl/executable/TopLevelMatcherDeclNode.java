/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.decl.executable;

import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.RhsDeclNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeNode;

/**
 * Base class for top level pattern matching related ast nodes
 */
public abstract class TopLevelMatcherDeclNode extends MatcherDeclNode
{
	public TopLevelMatcherDeclNode(IdentNode id, TypeNode type, PatternGraphLhsNode left)
	{
		super(id, type, left);
	}
	
	protected boolean noAbstractElementInstantiated(RhsDeclNode right)
	{
		boolean abstr = true;

		for(NodeDeclNode node : right.patternGraph.getNodes()) {
			if(!node.inheritsType() && node.getDeclType().isAbstract() && !pattern.getNodes().contains(node)
					&& (node.context & CONTEXT_PARAMETER) != CONTEXT_PARAMETER) {
				node.reportError("Instances of abstract node classes are not allowed (node" + node.emptyWhenAnonymousPostfix(" ")
						+ " is declared with the abstract type " + node.getDeclType().toStringWithDeclarationCoords() + ").");
				abstr = false;
			}
		}
		for(EdgeDeclNode edge : right.patternGraph.getEdges()) {
			if(!edge.inheritsType() && edge.getDeclType().isAbstract() && !pattern.getEdges().contains(edge)
					&& (edge.context & CONTEXT_PARAMETER) != CONTEXT_PARAMETER) {
				edge.reportError("Instances of abstract edge classes are not allowed (edge" + edge.emptyWhenAnonymousPostfix(" ")
						+ " is declared with the abstract type " + edge.getDeclType().toStringWithDeclarationCoords() + ").");
				abstr = false;
			}
		}

		return abstr;
	}
}
