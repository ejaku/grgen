/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.numeric
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using CeilExpr = de.unika.ipd.grgen.ir.expr.numeric.CeilExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class CeilExprNode : BuiltinFunctionInvocationBaseNode
	{
		static CeilExprNode()
		{
			SetClassName(typeof(CeilExprNode), "ceil expr");
		}

		private ExprNode argumentExpr;

		public CeilExprNode(Coords coords, ExprNode argumentExpr)
			: base(coords)
		{

			this.argumentExpr = BecomeParent(argumentExpr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(argumentExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("arg");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			if(argumentExpr.Type.IsEqual(BasicTypeNode.doubleType))
				return true;
			ReportError("The function Math::ceil() expects as argument a value of type double"
					+ " (but is given a value of type " + argumentExpr.Type.TypeName + ").");
			return false;
		}

		protected internal override IR ConstructIR()
		{
			argumentExpr = argumentExpr.Evaluate();
			return new CeilExpr(argumentExpr.CheckIR<Expression>(typeof(Expression)));
		}

		public override TypeNode Type
		{
			get
			{
				return argumentExpr.Type;
			}
		}
	}

}
