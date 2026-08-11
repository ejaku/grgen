/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{

using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
using MemberDeclNode = de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
using MatchTypeNode = de.unika.ipd.grgen.ast.type.MatchTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using UntypedExecVarTypeNode = de.unika.ipd.grgen.ast.type.basic.UntypedExecVarTypeNode;
using de.unika.ipd.grgen.ast.util;
using Entity = de.unika.ipd.grgen.ir.Entity;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using GraphEntityExpression = de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
using MatchAccess = de.unika.ipd.grgen.ir.expr.MatchAccess;
using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
using VariableExpression = de.unika.ipd.grgen.ir.expr.VariableExpression;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class MemberAccessExprNode : ExprNode
{
	static MemberAccessExprNode()
	{
		SetClassName(typeof(MemberAccessExprNode), "member access expression");
	}

	private ExprNode targetExpr; // resulting from primary expression, most often an IdentExprNode
	private IdentNode memberIdent;
	private DeclNode member;

	public MemberAccessExprNode(Coords coords, ExprNode targetExpr, IdentNode memberIdent)
		: base(coords)
	{
		this.targetExpr = BecomeParent(targetExpr);
		this.memberIdent = BecomeParent(memberIdent);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			if(IsResolved() && ResolutionResult())
			{
				if(targetExpr.Type is MatchTypeNode)
					return children; // behave like a nop in case we're a match access
			}
			children.Add(targetExpr);
			children.Add(memberIdent);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("targetExpr");
			childrenNames.Add("memberIdent");
			return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		if(!targetExpr.Resolve())
			return false;

		if(targetExpr is IdentExprNode)
		{
			IdentExprNode identExpr = (IdentExprNode)targetExpr;
			if(identExpr.decl is TypeDeclNode)
			{
				TypeDeclNode typeNode = (TypeDeclNode)identExpr.decl;
				ReportError("Member access expects an entity, but is given a type"
						+ " (unexpected " + typeNode.Ident + " when accessing " + memberIdent + ").");
			}
		}
		if(targetExpr is TypeofNode)
		{
			TypeofNode typeofExpr = (TypeofNode)targetExpr;
			ReportError("Member access expects an entity, but is given a type"
					+ " (unexpected typeof(" + typeofExpr.Entity.Decl.GetIdent() + ") when accessing " + memberIdent + ").");
		}

		TypeNode ownerType = targetExpr.Type;

		if(ownerType is UntypedExecVarTypeNode)
		{
			member = new MemberDeclNode(memberIdent, BasicTypeNode.untypedType, false);
			member.Resolve();
			SetCheckVisited();
			return true;
		}

		member = Resolver.ResolveMember(ownerType, memberIdent);

		return member != null;
	}

	protected internal override bool CheckLocal()
	{
		return true;
	}

	public ExprNode Target
	{
		get
		{
			return targetExpr; // resulting from primary expression, most often an IdentExprNode
		}
	}

	public MemberDeclNode Decl
	{
		get
		{
			Debug.Assert(IsResolved());

			return member is MemberDeclNode ? (MemberDeclNode)member : null;
		}
	}

	public override TypeNode Type
	{
		get
		{
			TypeNode declType = null;
			if(targetExpr.Type is MatchTypeNode)
				declType = member.DeclType;
			else
				declType = member.Decl.GetDeclType(); // untyped exec var type in case owner is an untyped exec var
			return declType;
		}
	}

	protected internal override IR ConstructIR()
	{
		targetExpr = targetExpr.Evaluate();
		if(targetExpr.Type is MatchTypeNode)
			return new MatchAccess(targetExpr.CheckIR(typeof(Expression)), member.CheckIR(typeof(Entity)));

		if(targetExpr.IR is VariableExpression)
		{
			return new Qualification(targetExpr.CheckIR(typeof(VariableExpression)).GetVariable(),
					member.CheckIR(typeof(Entity)));
		}
		else if(targetExpr.IR is GraphEntityExpression)
		{
			return new Qualification(targetExpr.CheckIR(typeof(GraphEntityExpression)).GetGraphEntity(),
					member.CheckIR(typeof(Entity)));
		}
		else
		{
			return new Qualification(targetExpr.CheckIR(typeof(Expression)), // normally a Cast (or an untyped exec var)
					member.CheckIR(typeof(Entity)));
		}
	}

	public static string KindStr
	{
		get
		{
			return "member";
		}
	}
}

}
