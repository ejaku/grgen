/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.decl.pattern;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeExprNode;
import de.unika.ipd.grgen.ir.pattern.Node;

/**
 * Dummy node needed for dangling edges
 */
public class DummyNodeDeclNode extends NodeDeclNode
{
	static {
		setName(DummyNodeDeclNode.class, "dummy node");
	}

	public DummyNodeDeclNode(IdentNode id, BaseNode type, int context, PatternGraphLhsNode directlyNestingLHSGraph)
	{
		super(id, type, CopyKind.None, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
	}

	@Override
	public Node getNode()
	{
		return null;
	}

	@Override
	public boolean isDummy()
	{
		return true;
	}

	@Override
	public String toString()
	{
		return "a dummy node";
	}
}
