/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.numeric
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using MinExpr = de.unika.ipd.grgen.ir.expr.numeric.MinExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class MinExprNode : BuiltinFunctionInvocationBaseNode
	{
		static MinExprNode()
		{
			SetClassName(typeof(MinExprNode), "min expr");
		}

		private ExprNode leftExpr;
		private ExprNode rightExpr;

		public MinExprNode(Coords coords, ExprNode leftExpr, ExprNode rightExpr)
			 : base(coords)
		{

			this.leftExpr = BecomeParent(leftExpr);
			this.rightExpr = BecomeParent(rightExpr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(leftExpr);
				children.Add(rightExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("left");
				childrenNames.Add("right");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			if(!leftExpr.Type.IsNumericType())
			{
				ReportError("The function Math::min() expects as 1. argument a value of type " + TypeNode.NumericTypesAsString
						+ " (but is given a value of type " + leftExpr.Type.TypeName + ").");
				return false;
			}
			if(!rightExpr.Type.IsNumericType())
			{
				ReportError("The function Math::min() expects as 2. argument a value of type " + TypeNode.NumericTypesAsString
						+ " (but is given a value of type " + rightExpr.Type.TypeName + ").");
				return false;
			}
			if(!rightExpr.Type.IsEqual(leftExpr.Type))
			{
				ReportError("The function Math::min() expects the 1. and 2. argument to be of the same type"
						+ " (but they are values of type " + leftExpr.Type.TypeName + " and " + rightExpr.Type.TypeName + ").");
				return false;
			}
			return true;
		}

		protected internal override IR ConstructIR()
		{
			leftExpr = leftExpr.Evaluate();
			rightExpr = rightExpr.Evaluate();
			return new MinExpr(leftExpr.CheckIR<Expression>(typeof(Expression)), rightExpr.CheckIR<Expression>(typeof(Expression)));
		}

		public override TypeNode Type
		{
			get
			{
				return leftExpr.Type;
			}
		}
	}

}
