/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
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
 * AST node for an iterated pattern with explicitly specified min and max bounds on the matches, maybe including replacements.
 */
public class IteratedMinMaxDeclNode extends IteratedDeclNode
{
	static {
		setName(IteratedMinMaxDeclNode.class, "iterated minmax");
	}

	private int minMatches;
	private int maxMatches;

	public IteratedMinMaxDeclNode(IdentNode id, PatternGraphLhsNode left, RhsDeclNode right, int minMatches, int maxMatches)
	{
		super(id, left, right);
		this.minMatches = minMatches;
		this.maxMatches = maxMatches;
	}

	@Override
	protected int getMinMatches()
	{
		return minMatches;
	}
	
	@Override
	protected int getMaxMatches()
	{
		return maxMatches;
	}

	public static String getKindStr()
	{
		return "iterated-minmax";
	}
}
