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
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using ThisExpr = de.unika.ipd.grgen.ir.expr.graph.ThisExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ThisExprNode : ExprNode
	{
		static ThisExprNode()
		{
			SetClassName(typeof(ThisExprNode), "this");
		}

		public ThisExprNode(Coords coords)
			: base(coords)
		{
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			return new ThisExpr();
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.graphType;
			}
		}
	}

}
