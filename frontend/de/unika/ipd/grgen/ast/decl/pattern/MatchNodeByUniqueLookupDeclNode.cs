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

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using UnitNode = de.unika.ipd.grgen.ast.UnitNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using TypeExprNode = de.unika.ipd.grgen.ast.type.TypeExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using IntTypeNode = de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Node = de.unika.ipd.grgen.ir.pattern.Node;
using UniqueLookup = de.unika.ipd.grgen.ir.pattern.UniqueLookup;

public class MatchNodeByUniqueLookupDeclNode : NodeDeclNode
{
	static MatchNodeByUniqueLookupDeclNode()
	{
		SetClassName(typeof(MatchNodeByUniqueLookupDeclNode), "match node by unique lookup decl");
	}

	private ExprNode expr;

	public MatchNodeByUniqueLookupDeclNode(IdentNode id, BaseNode type, int context,
			ExprNode expr, PatternGraphLhsNode directlyNestingLHSGraph)
		: base(id, type, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph)
	{
		this.expr = expr;
		BecomeParent(this.expr);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(ident);
		children.Add(GetValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
		children.Add(constraints);
		children.Add(expr);
		return children;
		}
	}

	/// <summary>
	/// returns names of the children, same order as in getChildren </summary>
	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("ident");
		childrenNames.Add("type");
		childrenNames.Add("constraints");
		childrenNames.Add("expression");
		return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = base.ResolveLocal();
		successfullyResolved &= expr.Resolve();
		return successfullyResolved;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		bool res = base.CheckLocal();
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
		{
			ReportError("Cannot employ match node by unique index lookup in the rewrite part"
					+ EmptyWhenAnonymous(" (as it occurs in match node " + Ident + ")") + ".");
			return false;
		}
		TypeNode expectedLookupType = IntTypeNode.intType;
		TypeNode lookupType = expr.Type;
		if(!lookupType.IsCompatibleTo(expectedLookupType))
		{
			string expTypeName = expectedLookupType.TypeName;
			string typeName = lookupType.TypeName;
			ident.ReportError("Cannot convert type used in accessing unique index from " + typeName
					+ " to the expected " + expTypeName + " in match node" + EmptyWhenAnonymousPostfix(" ") + " by unique index lookup.");
			return false;
		}
		if(!UnitNode.Root.GetModel().IsUniqueIndexDefined())
		{
			ReportError("The match node by unique index lookup expects a model with a unique index, but the required index unique; declaration is missing in the model specification.");
			return false;
		}
		return res;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
	protected internal override IR ConstructIR()
	{
		if(IsIRAlreadySet()) // break endless recursion in case of cycle in usage
			return IR;

		Node node = (Node)base.ConstructIR();

		IR = node;

		expr = expr.Evaluate();
		node.UniqueIndexAccess = new UniqueLookup(expr.CheckIR(typeof(Expression)));
		return node;
	}
}

}
