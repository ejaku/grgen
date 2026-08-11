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
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using DefinedSubgraphExpr = de.unika.ipd.grgen.ir.expr.graph.DefinedSubgraphExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding the defined subgraph of an edge set.
	/// </summary>
	public class DefinedSubgraphExprNode : BuiltinFunctionInvocationBaseNode
	{
		static DefinedSubgraphExprNode()
		{
			SetClassName(typeof(DefinedSubgraphExprNode), "defined subgraph expr");
		}

		private ExprNode edgeSetExpr;

		public DefinedSubgraphExprNode(Coords coords, ExprNode edgeSetExpr)
			: base(coords)
		{
			this.edgeSetExpr = edgeSetExpr;
			BecomeParent(this.edgeSetExpr);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(edgeSetExpr);
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
				childrenNames.Add("edgeSetExpr");
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
			if(!(edgeSetExpr.Type is SetTypeNode))
			{
				edgeSetExpr.ReportError("The function definedSubgraph expects as argument a value of type set"
						+ " (but is given a value of type " + edgeSetExpr.Type.TypeName + ").");
				return false;
			}
			SetTypeNode type = (SetTypeNode)edgeSetExpr.Type;
			if(!(type.valueType is EdgeTypeNode))
			{
				edgeSetExpr.ReportError("The function definedSubgraph expects as argument a value of type set<Edge|UEdge|AEdge>"
						+ " (but is given a value of type " + edgeSetExpr.Type.TypeName + ").");
				return false;
			}
			EdgeTypeNode edgeValueType = (EdgeTypeNode)type.valueType;
			if(edgeValueType != EdgeTypeNode.arbitraryEdgeType
					&& edgeValueType != EdgeTypeNode.directedEdgeType
					&& edgeValueType != EdgeTypeNode.undirectedEdgeType)
			{
				edgeSetExpr.ReportError("The function definedSubgraph expects as argument a value of type set<Edge|UEdge|AEdge>"
						+ " (but is given a value of type " + edgeSetExpr.Type.TypeName + ").");
				return false;
			}
			return true;
		}

		protected internal override IR ConstructIR()
		{
			edgeSetExpr = edgeSetExpr.Evaluate();
			return new DefinedSubgraphExpr(edgeSetExpr.CheckIR<Expression>(typeof(Expression)), Type.IRType);
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
