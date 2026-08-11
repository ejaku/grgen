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
using ExternalFunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.ExternalFunctionDeclNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using ExternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using ExternalFunction = de.unika.ipd.grgen.ir.executable.ExternalFunction;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using ExternalFunctionMethodInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.ExternalFunctionMethodInvocationExpr;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// Invocation of an external function method
/// </summary>
public class ExternalFunctionMethodInvocationExprNode : FunctionInvocationBaseNode
{
	static ExternalFunctionMethodInvocationExprNode()
	{
		SetClassName(typeof(ExternalFunctionMethodInvocationExprNode), "external function method invocation expression");
	}

	private ExprNode owner;

	private IdentNode externalFunctionUnresolved;
	private ExternalFunctionDeclNode externalFunctionDecl;

	public ExternalFunctionMethodInvocationExprNode(ExprNode owner, IdentNode externalFunctionUnresolved,
			CollectNode<ExprNode> arguments)
		: base(externalFunctionUnresolved.Coords, arguments)
	{
		this.owner = BecomeParent(owner);
		this.externalFunctionUnresolved = BecomeParent(externalFunctionUnresolved);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(owner);
		children.Add(GetValidVersion(externalFunctionUnresolved, externalFunctionDecl));
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
		childrenNames.Add("external function method");
		childrenNames.Add("arguments");
		return childrenNames;
		}
	}

	private static readonly DeclarationResolver<ExternalFunctionDeclNode> resolver =
			new DeclarationResolver<ExternalFunctionDeclNode>(typeof(ExternalFunctionDeclNode));

	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = true;
		TypeNode ownerType = owner.Type;
		if(ownerType is ExternalObjectTypeNode)
		{
			if(ownerType is ScopeOwner)
			{
				ScopeOwner o = (ScopeOwner)ownerType;
				o.FixupDefinition(externalFunctionUnresolved);

				externalFunctionDecl = resolver.Resolve(externalFunctionUnresolved, this);
				if(externalFunctionDecl == null)
				{
					externalFunctionUnresolved.ReportError("An external function method of name " + externalFunctionUnresolved + " is not known."
							+ " Is it a misspelled function name? Or is a procedure call intended (this is not possible in an expression, an assignment target must be given as (param,...)=call in that case)?");
					return false;
				}

				successfullyResolved = externalFunctionDecl != null && successfullyResolved;
			}
			else
			{
				ReportError("Left hand side of '.' does not own a scope.");
				successfullyResolved = false;
			}
		}
		else
		{
			ReportError("Left hand side of '.' is not an external type.");
			successfullyResolved = false;
		}

		return successfullyResolved;
	}

	protected internal override bool CheckLocal()
	{
		return CheckSignatureAdhered(externalFunctionDecl, externalFunctionUnresolved, true);
	}

	public override TypeNode Type
	{
		get
		{
		Debug.Assert(IsResolved());
		return externalFunctionDecl.ResultType;
		}
	}

	protected internal override IR ConstructIR()
	{
		owner = owner.Evaluate();
		ExternalFunctionMethodInvocationExpr efi = new ExternalFunctionMethodInvocationExpr(
				owner.CheckIR(typeof(Expression)),
				externalFunctionDecl.resultType.CheckIR(typeof(Type)),
				externalFunctionDecl.CheckIR(typeof(ExternalFunction)));
		foreach(ExprNode argument in arguments.ChildrenExact)
		{
			ExprNode argumentEvaluated = argument.Evaluate();
			efi.AddArgument(argumentEvaluated.CheckIR(typeof(Expression)));
		}
		return efi;
	}
}

}
