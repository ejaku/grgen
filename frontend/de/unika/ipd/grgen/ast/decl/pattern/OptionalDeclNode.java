/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.decl.pattern;

import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;

/**
 * AST node for an optional pattern, maybe including replacements.
 */
public class OptionalDeclNode extends IteratedDeclNode
{
	static {
		setName(OptionalDeclNode.class, "optional");
	}

	public OptionalDeclNode(IdentNode id, PatternGraphLhsNode left, RhsDeclNode right)
	{
		super(id, left, right);
	}

	@Override
	protected int getMinMatches()
	{
		return 0;
	}
	
	@Override
	protected int getMaxMatches()
	{
		return 1;
	}

	public static String getKindStr()
	{
		return "optional";
	}
}
