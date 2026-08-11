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
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using EdgesFromIndexAccessSameExpr = de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessSameExpr;
	using Index = de.unika.ipd.grgen.ir.model.Index;
	using IndexAccessEquality = de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding the edges from an index as array by accessing using a comparison for equality.
	/// </summary>
	public class EdgesFromIndexAccessSameAsArrayExprNode : FromIndexAccessSameExprNode
	{
		static EdgesFromIndexAccessSameAsArrayExprNode()
		{
			SetClassName(typeof(EdgesFromIndexAccessSameAsArrayExprNode), "edges from index access same as array expr");
		}

		private ArrayTypeNode arrayTypeNode;

		public EdgesFromIndexAccessSameAsArrayExprNode(Coords coords, BaseNode index, ExprNode expr)
			: base(coords, index, expr)
		{
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = base.ResolveLocal();
			arrayTypeNode = new ArrayTypeNode(Root);
			successfullyResolved &= arrayTypeNode.Resolve();
			return successfullyResolved;
		}

		protected internal override IdentNode Root
		{
			get
			{
				return EdgeRoot;
			}
		}

		protected internal override string ShortSignature()
		{
			return "edgesFromIndexSameAsArray(.,.)";
		}

		public override TypeNode Type
		{
			get
			{
				return arrayTypeNode;
			}
		}

		protected internal override IR ConstructIR()
		{
			expr = expr.Evaluate();
			return new EdgesFromIndexAccessSameExpr(
					new IndexAccessEquality(index.CheckIR(typeof(Index)), expr.CheckIR(typeof(Expression))),
					Type.IRType);
		}
	}

}
