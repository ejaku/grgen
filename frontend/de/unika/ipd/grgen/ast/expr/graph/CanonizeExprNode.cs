/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using CanonizeExpr = de.unika.ipd.grgen.ir.expr.graph.CanonizeExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class CanonizeExprNode : BuiltinFunctionInvocationBaseNode
	{
		static CanonizeExprNode()
		{
			SetClassName(typeof(CanonizeExprNode), "canonize expr");
		}

		private ExprNode graphExpr;

		public CanonizeExprNode(Coords coords, ExprNode graphExpr)
			: base(coords)
		{

			this.graphExpr = BecomeParent(graphExpr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(graphExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("graph");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			if(graphExpr.Type.IsEqual(BasicTypeNode.graphType))
				return true;
			else
			{
				ReportError("The function canonize expects as argument a value of type graph"
						+ " (but is given a value of type " + graphExpr.Type.TypeName + ").");
				return false;
			}
		}

		protected internal override IR ConstructIR()
		{
			graphExpr = graphExpr.Evaluate();
			return new CanonizeExpr(graphExpr.CheckIR<Expression>(typeof(Expression)));
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.stringType;
			}
		}
	}

}
