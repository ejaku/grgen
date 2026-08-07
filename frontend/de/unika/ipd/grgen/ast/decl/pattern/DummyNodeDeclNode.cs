/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.decl.pattern
{
using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using TypeExprNode = de.unika.ipd.grgen.ast.type.TypeExprNode;
using Node = de.unika.ipd.grgen.ir.pattern.Node;

/// <summary>
/// Dummy node needed for dangling edges
/// </summary>
public class DummyNodeDeclNode : NodeDeclNode
{
	static DummyNodeDeclNode()
	{
		SetClassName(typeof(DummyNodeDeclNode), "dummy node");
	}

	public DummyNodeDeclNode(IdentNode id, BaseNode type, int context, PatternGraphLhsNode directlyNestingLHSGraph)
		: base(id, type, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph)
	{
	}

	public override Node IRNode
	{
		get
		{
		return null;
		}
	}

	public override bool IsDummy()
	{
		return true;
	}

	public override string ToString()
	{
		return "a dummy node";
	}
}

}
