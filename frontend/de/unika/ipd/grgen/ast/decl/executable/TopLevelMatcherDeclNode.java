/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
				error.error(node.getCoords(), "Instances of abstract nodes are not allowed");
				abstr = false;
			}
		}
		for(EdgeDeclNode edge : right.patternGraph.getEdges()) {
			if(!edge.inheritsType() && edge.getDeclType().isAbstract() && !pattern.getEdges().contains(edge)
					&& (edge.context & CONTEXT_PARAMETER) != CONTEXT_PARAMETER) {
				error.error(edge.getCoords(), "Instances of abstract edges are not allowed");
				abstr = false;
			}
		}

		return abstr;
	}
}
