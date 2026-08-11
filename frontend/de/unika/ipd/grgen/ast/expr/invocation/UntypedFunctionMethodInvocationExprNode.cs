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
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using UntypedFunctionMethodInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.UntypedFunctionMethodInvocationExpr;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	/// <summary>
	/// Invocation of a function method on an untyped target - result untyped
	/// </summary>
	public class UntypedFunctionMethodInvocationExprNode : FunctionInvocationBaseNode
	{
		static UntypedFunctionMethodInvocationExprNode()
		{
			SetClassName(typeof(UntypedFunctionMethodInvocationExprNode), "untyped function method invocation expression");
		}

		public UntypedFunctionMethodInvocationExprNode(Coords coords, CollectNode<ExprNode> arguments)
			: base(coords, arguments)
		{
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(arguments);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("arguments");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			return true;
		}

		protected internal override bool CheckLocal()
		{
			return true;
		}

		public override TypeNode Type
		{
			get
			{
				Debug.Assert(IsResolved());
				return BasicTypeNode.untypedType;
			}
		}

		protected internal override IR ConstructIR()
		{
			UntypedFunctionMethodInvocationExpr ufmi = new UntypedFunctionMethodInvocationExpr(
					BasicTypeNode.untypedType.CheckIR(typeof(Type)));
			foreach(ExprNode argument in arguments.ChildrenExact)
			{
				ExprNode argumentEvaluated = argument.Evaluate();
				ufmi.AddArgument(argumentEvaluated.CheckIR(typeof(Expression)));
			}
			return ufmi;
		}
	}

}
