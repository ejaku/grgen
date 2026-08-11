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
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using IndexSizeExpr = de.unika.ipd.grgen.ir.expr.graph.IndexSizeExpr;
	using Index = de.unika.ipd.grgen.ir.model.Index;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding the size of an index, i.e. the number of elements/count of elements stored in the index.
	/// </summary>
	public class IndexSizeExprNode : FromIndexAccessExprNode
	{
		static IndexSizeExprNode()
		{
			SetClassName(typeof(IndexSizeExprNode), "index size expr");
		}

		public IndexSizeExprNode(Coords coords, BaseNode index)
			: base(coords, index)
		{
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
			return true; // do not call checkLocal of super / ensure it is not called (to prevent an invalid node/edge type check)
		}

		protected internal override IdentNode Root
		{
			get
			{
				return null;
			}
		}

		protected internal override string ShortSignature()
		{
			return "indexSize(.)";
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
			return new IndexSizeExpr(index.CheckIR<Index>(typeof(Index)),
					Type.IRType);
		}
	}

}
