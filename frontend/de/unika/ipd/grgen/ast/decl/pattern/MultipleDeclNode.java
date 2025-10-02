/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.decl.pattern;

import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;

/**
 * AST node for a multiple pattern, maybe including replacements.
 */
public class MultipleDeclNode extends IteratedDeclNode
{
	static {
		setName(MultipleDeclNode.class, "multiple");
	}

	public MultipleDeclNode(IdentNode id, PatternGraphLhsNode left, RhsDeclNode right)
	{
		super(id, left, right);
	}

	@Override
	protected int getMinMatches()
	{
		return 1;
	}
	
	@Override
	protected int getMaxMatches()
	{
		return 0;
	}

	public static String getKindStr()
	{
		return "multiple";
	}
}
