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
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using StringTypeNode = de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using EdgeByNameExpr = de.unika.ipd.grgen.ir.expr.graph.EdgeByNameExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node retrieving an edge from a name.
	/// </summary>
	public class EdgeByNameExprNode : BuiltinFunctionInvocationBaseNode
	{
		static EdgeByNameExprNode()
		{
			SetClassName(typeof(EdgeByNameExprNode), "edge by name expr");
		}

		private ExprNode name;
		private ExprNode edgeType;

		public EdgeByNameExprNode(Coords coords, ExprNode name, ExprNode edgeType)
			: base(coords)
		{
			this.name = name;
			BecomeParent(this.name);
			this.edgeType = edgeType;
			BecomeParent(this.edgeType);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(name);
				children.Add(edgeType);
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
				childrenNames.Add("name");
				childrenNames.Add("edgeType");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			return true;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			if(!(name.Type is StringTypeNode))
			{
				ReportError("The function edgeByName expects as 1. argument (nameToSearchFor) a value of type string"
						+ " (but is given a value of type " + name.Type.TypeName + ").");
				return false;
			}
			if(!(edgeType.Type is EdgeTypeNode))
			{
				ReportError("The function edgeByName expects as 2. argument (typeToObtain) a value of type edge type"
						+ " (but is given a value of type " + edgeType.Type.TypeName + ").");
				return false;
			}
			return true;
		}

		protected internal override IR ConstructIR()
		{
			name = name.Evaluate();
			edgeType = edgeType.Evaluate();
			return new EdgeByNameExpr(name.CheckIR(typeof(Expression)),
					edgeType.CheckIR(typeof(Expression)), Type.IRType);
		}

		public override TypeNode Type
		{
			get
			{
				return edgeType.Type;
			}
		}
	}

}
