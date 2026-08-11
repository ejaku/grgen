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
	using FunctionOrOperatorDeclBaseNode = de.unika.ipd.grgen.ast.decl.executable.FunctionOrOperatorDeclBaseNode;
	using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using ExternalFunction = de.unika.ipd.grgen.ir.executable.ExternalFunction;
	using Function = de.unika.ipd.grgen.ir.executable.Function;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ExternalFunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.ExternalFunctionInvocationExpr;
	using FunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.FunctionInvocationExpr;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	/// <summary>
	/// Invocation of a function or an external function
	/// </summary>
	public class FunctionOrExternalFunctionInvocationExprNode : FunctionInvocationBaseNode
	{
		static FunctionOrExternalFunctionInvocationExprNode()
		{
			SetClassName(typeof(FunctionOrExternalFunctionInvocationExprNode), "function or external function invocation expression");
		}

		private IdentNode functionOrExternalFunctionUnresolved;
		private ExternalFunctionDeclNode externalFunctionDecl;
		private FunctionDeclNode functionDecl;

		public FunctionOrExternalFunctionInvocationExprNode(IdentNode functionOrExternalFunctionUnresolved,
				CollectNode<ExprNode> arguments)
			: base(functionOrExternalFunctionUnresolved.Coords, arguments)
		{
			this.functionOrExternalFunctionUnresolved = BecomeParent(functionOrExternalFunctionUnresolved);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(functionOrExternalFunctionUnresolved, functionDecl, externalFunctionDecl));
				children.Add(arguments);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("function or external function");
				childrenNames.Add("arguments");
				return childrenNames;
			}
		}

		private static readonly DeclarationPairResolver<FunctionDeclNode, ExternalFunctionDeclNode> resolver =
				new DeclarationPairResolver<FunctionDeclNode, ExternalFunctionDeclNode>(typeof(FunctionDeclNode), typeof(ExternalFunctionDeclNode));

		protected internal override bool ResolveLocal()
		{
			if(!(functionOrExternalFunctionUnresolved is PackageIdentNode))
			{
				FixupDefinition(functionOrExternalFunctionUnresolved,
						functionOrExternalFunctionUnresolved.Scope);
			}
			Pair<FunctionDeclNode, ExternalFunctionDeclNode> resolved =
					resolver.Resolve(functionOrExternalFunctionUnresolved, this);
			if(resolved == null)
			{
				functionOrExternalFunctionUnresolved.ReportError("A function/external function of name " + functionOrExternalFunctionUnresolved + " is not known."
						+ " Is it a misspelled (external) function name? Or is a procedure call intended (this is not possible in an expression, an assignment target must be given as (param,...)=call in that case)?");
				return false;
			}
			functionDecl = resolved.fst;
			externalFunctionDecl = resolved.snd;
			return true;
		}

		protected internal override bool CheckLocal()
		{
			FunctionOrOperatorDeclBaseNode fb = functionDecl != null ? (de.unika.ipd.grgen.ast.decl.executable.FunctionDeclBaseNode)functionDecl : externalFunctionDecl;
			return CheckSignatureAdhered(fb, functionOrExternalFunctionUnresolved, false);
		}

		public override TypeNode Type
		{
			get
			{
				Debug.Assert(IsResolved());
				return functionDecl != null ? functionDecl.ResultType : externalFunctionDecl.ResultType;
			}
		}

		protected internal override IR ConstructIR()
		{
			if(functionDecl != null)
			{
				FunctionInvocationExpr fi = new FunctionInvocationExpr(
						functionDecl.resultType.CheckIR(typeof(Type)),
						functionDecl.CheckIR(typeof(Function)));
				foreach(ExprNode argument in arguments.ChildrenExact)
				{
					ExprNode argumentEvaluated = argument.Evaluate();
					fi.AddArgument(argumentEvaluated.CheckIR(typeof(Expression)));
				}
				return fi;
			}
			else
			{
				ExternalFunctionInvocationExpr efi = new ExternalFunctionInvocationExpr(
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

}
