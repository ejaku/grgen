/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{
	using de.unika.ipd.grgen.ast;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using CountNodesFromIndexAccessFromToExpr = de.unika.ipd.grgen.ir.expr.graph.CountNodesFromIndexAccessFromToExpr;
	using Index = de.unika.ipd.grgen.ir.model.Index;
	using IndexAccessOrdering = de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding the count of nodes from an index by accessing a range from a certain value to a certain value (one or both may be optional).
	/// </summary>
	public class CountNodesFromIndexAccessFromToExprNode : FromIndexAccessFromToExprNode
	{
		static CountNodesFromIndexAccessFromToExprNode()
		{
			SetClassName(typeof(CountNodesFromIndexAccessFromToExprNode), "count nodes from index access from to expr");
		}

		public CountNodesFromIndexAccessFromToExprNode(Coords coords, BaseNode index, ExprNode fromExpr, bool fromExclusive, ExprNode toExpr, bool toExclusive)
			: base(coords, index, fromExpr, fromExclusive, toExpr, toExclusive)
		{
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = base.ResolveLocal();
			successfullyResolved &= Type.Resolve();
			return successfullyResolved;
		}

		protected internal override IdentNode Root
		{
			get
			{
				return NodeRoot;
			}
		}

		protected internal override string ShortSignature()
		{
			return "countNodesFromIndex" + FromPart() + ToPart() + "(" + ArgumentsPart() + ")";
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.intType;
			}
		}

		protected internal override IR ConstructIR()
		{
			if(fromExpr != null)
				fromExpr = fromExpr.Evaluate();
			if(toExpr != null)
				toExpr = toExpr.Evaluate();
			return new CountNodesFromIndexAccessFromToExpr(
					new IndexAccessOrdering(index.CheckIR<Index>(typeof(Index)), true,
							FromOperator(), fromExpr != null ? fromExpr.CheckIR<Expression>(typeof(Expression)) : null,
							ToOperator(), toExpr != null ? toExpr.CheckIR<Expression>(typeof(Expression)) : null),
					Type.IRType);
		}
	}

}
