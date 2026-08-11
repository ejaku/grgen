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
	using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using NodesFromIndexAccessSameExpr = de.unika.ipd.grgen.ir.expr.graph.NodesFromIndexAccessSameExpr;
	using Index = de.unika.ipd.grgen.ir.model.Index;
	using IndexAccessEquality = de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding the nodes from an index by accessing using a comparison for equality.
	/// </summary>
	public class NodesFromIndexAccessSameExprNode : FromIndexAccessSameExprNode
	{
		static NodesFromIndexAccessSameExprNode()
		{
			SetClassName(typeof(NodesFromIndexAccessSameExprNode), "nodes from index access same expr");
		}

		private SetTypeNode setTypeNode;

		public NodesFromIndexAccessSameExprNode(Coords coords, BaseNode index, ExprNode expr)
			: base(coords, index, expr)
		{
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = base.ResolveLocal();
			setTypeNode = new SetTypeNode(Root);
			successfullyResolved &= setTypeNode.Resolve();
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
			return "nodesFromIndexSame(.,.)";
		}

		public override TypeNode Type
		{
			get
			{
				return setTypeNode;
			}
		}

		protected internal override IR ConstructIR()
		{
			expr = expr.Evaluate();
			return new NodesFromIndexAccessSameExpr(
					new IndexAccessEquality(index.CheckIR(typeof(Index)), expr.CheckIR(typeof(Expression))),
					Type.IRType);
		}
	}

}
