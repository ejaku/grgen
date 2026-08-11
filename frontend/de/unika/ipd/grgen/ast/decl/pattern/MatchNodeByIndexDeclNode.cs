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
using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using IndexDeclNode = de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using TypeExprNode = de.unika.ipd.grgen.ast.type.TypeExprNode;
using de.unika.ipd.grgen.ast.util;

public abstract class MatchNodeByIndexDeclNode : NodeDeclNode
{
	static MatchNodeByIndexDeclNode()
	{
		SetClassName(typeof(MatchNodeByIndexDeclNode), "match node by index");
	}

	protected internal IdentNode indexUnresolved;
	protected internal IndexDeclNode index;

	protected internal MatchNodeByIndexDeclNode(IdentNode id, BaseNode type, int context,
			IdentNode index, PatternGraphLhsNode directlyNestingLHSGraph)
		: base(id, type, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph)
	{
		this.indexUnresolved = index;
		BecomeParent(this.indexUnresolved);
	}

	private static DeclarationResolver<IndexDeclNode> indexResolver =
			new DeclarationResolver<IndexDeclNode>(typeof(IndexDeclNode));

	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = base.ResolveLocal();
		index = indexResolver.Resolve(indexUnresolved, this);
		successfullyResolved &= index != null;
		return successfullyResolved;
	}

	protected internal override bool CheckLocal()
	{
		bool res = base.CheckLocal();
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
		{
			ReportError("Cannot employ match node by index in the rewrite part"
					+ " (as it occurs in match node" + EmptyWhenAnonymousPostfix(" ") + " by index access of " + index.Ident + ").");
			res = false;
		}
		return res;
	}
}

}
