/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.pattern
{
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;

/// <summary>
/// AST node for an iterated pattern with explicitly specified min and max bounds on the matches, maybe including replacements.
/// </summary>
public class IteratedMinMaxDeclNode : IteratedDeclNode
{
	static IteratedMinMaxDeclNode()
	{
		SetClassName(typeof(IteratedMinMaxDeclNode), "iterated minmax");
	}

	private int minMatches;
	private int maxMatches;

	public IteratedMinMaxDeclNode(IdentNode id, PatternGraphLhsNode left, RhsDeclNode right, int minMatches, int maxMatches)
		: base(id, left, right)
	{
		this.minMatches = minMatches;
		this.maxMatches = maxMatches;
	}

	protected internal override int MinMatches
	{
		get
		{
			return minMatches;
		}
	}

	protected internal override int MaxMatches
	{
		get
		{
			return maxMatches;
		}
	}

	public static string KindStr
	{
		get
		{
			return "iterated-minmax";
		}
	}
}

}
