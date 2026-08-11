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
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using MinMaxFromIndexExpr = de.unika.ipd.grgen.ir.expr.graph.MinMaxFromIndexExpr;
	using Index = de.unika.ipd.grgen.ir.model.Index;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding the bottom edge from an index with the lowest value or the top edge from an index with the highest value.
	/// </summary>
	public class MinMaxEdgeFromIndexExprNode : FromIndexAccessExprNode
	{
		static MinMaxEdgeFromIndexExprNode()
		{
			SetClassName(typeof(MinMaxEdgeFromIndexExprNode), "min/max edge from index expr");
		}

		internal bool isMin;

		public MinMaxEdgeFromIndexExprNode(Coords coords, BaseNode index, bool isMin)
			: base(coords, index)
		{
			this.isMin = isMin;
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(indexUnresolved, index));
				return children;
			}
		}

		/// <summary>
		/// returns names of the children, same order as in getChildren </summary>
		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("index");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			return base.ResolveLocal();
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			return base.CheckLocal();
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
			return isMin ? "minEdgeFromIndex(.)" : "maxEdgeFromIndex(.)";
		}

		public override TypeNode Type
		{
			get
			{
				return Root.Decl.GetDeclType();
			}
		}

		protected internal override IR ConstructIR()
		{
			return new MinMaxFromIndexExpr(index.CheckIR(typeof(Index)), isMin,
					Type.IRType);
		}
	}

}
