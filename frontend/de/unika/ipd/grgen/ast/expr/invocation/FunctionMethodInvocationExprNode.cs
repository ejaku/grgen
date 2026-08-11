/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.invocation
{

using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using Entity = de.unika.ipd.grgen.ir.Entity;
using IR = de.unika.ipd.grgen.ir.IR;
using Function = de.unika.ipd.grgen.ir.executable.Function;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using FunctionMethodInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.FunctionMethodInvocationExpr;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// Invocation of a function method
/// </summary>
public class FunctionMethodInvocationExprNode : FunctionInvocationBaseNode
{
	static FunctionMethodInvocationExprNode()
	{
		SetClassName(typeof(FunctionMethodInvocationExprNode), "function method invocation expression");
	}

	private IdentNode ownerUnresolved;
	private DeclNode owner;

	private IdentNode functionUnresolved;
	private FunctionDeclNode functionDecl;

	public FunctionMethodInvocationExprNode(IdentNode owner, IdentNode functionUnresolved,
			CollectNode<ExprNode> arguments)
		: base(functionUnresolved.Coords, arguments)
	{
		this.ownerUnresolved = BecomeParent(owner);
		this.functionUnresolved = BecomeParent(functionUnresolved);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(GetValidVersion(ownerUnresolved, owner));
		children.Add(GetValidVersion(functionUnresolved, functionDecl));
		children.Add(arguments);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("owner");
		childrenNames.Add("function method");
		childrenNames.Add("arguments");
		return childrenNames;
		}
	}

	private static readonly DeclarationResolver<DeclNode> ownerResolver =
			new DeclarationResolver<DeclNode>(typeof(DeclNode));
	private static readonly DeclarationResolver<FunctionDeclNode> resolver =
			new DeclarationResolver<FunctionDeclNode>(typeof(FunctionDeclNode));

	protected internal override bool ResolveLocal()
	{
		/* 1) resolve left hand side identifier, yielding a declaration of a type owning a scope
		 * 2) the scope owned by the lhs allows the ident node of the right hand side to fix/find its definition therein
		 * 3) resolve now complete/correct right hand side identifier into its declaration */
		bool res = FixupDefinition(ownerUnresolved, ownerUnresolved.Scope);
		if(!res)
			return false;

		bool successfullyResolved = true;
		owner = ownerResolver.Resolve(ownerUnresolved, this);
		successfullyResolved = owner != null && successfullyResolved;
		bool ownerResolveResult = owner != null && owner.Resolve();

		if(!ownerResolveResult)
		{
			// member can not be resolved due to inaccessible owner
			return false;
		}

		if(ownerResolveResult && owner != null
				&& (owner is NodeDeclNode || owner is EdgeDeclNode || owner is VarDeclNode))
		{
			TypeNode ownerType = owner.DeclType;
			if(ownerType is ScopeOwner)
			{
				ScopeOwner o = (ScopeOwner)ownerType;
				res = o.FixupDefinition(functionUnresolved);

				functionDecl = resolver.Resolve(functionUnresolved, this);
				if(functionDecl == null)
				{
					functionUnresolved.ReportError("A function method of name " + functionUnresolved + " is not known."
							+ " Is it a misspelled function name? Or is a procedure call intended (this is not possible in an expression, an assignment target must be given as (param,...)=call in that case)?");
					return false;
				}

				successfullyResolved = functionDecl != null && successfullyResolved;
			}
			else
			{
				ReportError("Left hand side of '.' does not own a scope.");
				successfullyResolved = false;
			}
		}
		else
		{
			ReportError("Left hand side of '.' is neither a node nor an edge nor a variable"
					+ (owner != null && owner.DeclType != null ? " (type " + owner.DeclType.ToStringWithDeclarationCoords() + ")." : "."));
			successfullyResolved = false;
		}

		return successfullyResolved;
	}

	protected internal override bool CheckLocal()
	{
		return CheckSignatureAdhered(functionDecl, functionUnresolved, true);
	}

	public override TypeNode Type
	{
		get
		{
		Debug.Assert(IsResolved());
		return functionDecl.ResultType;
		}
	}

	protected internal override IR ConstructIR()
	{
		FunctionMethodInvocationExpr ci = new FunctionMethodInvocationExpr(owner.CheckIR(typeof(Entity)),
				functionDecl.resultType.CheckIR(typeof(Type)),
				functionDecl.CheckIR(typeof(Function)));
		foreach(ExprNode argument in arguments.ChildrenExact)
		{
			ExprNode argumentEvaluated = argument.Evaluate();
			ci.AddArgument(argumentEvaluated.CheckIR(typeof(Expression)));
		}
		return ci;
	}
}

}
