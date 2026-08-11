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
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using SourceExpr = de.unika.ipd.grgen.ir.expr.graph.SourceExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding the source node of an edge.
	/// </summary>
	public class SourceExprNode : BuiltinFunctionInvocationBaseNode
	{
		static SourceExprNode()
		{
			SetClassName(typeof(SourceExprNode), "source expr");
		}

		private ExprNode edge;

		private IdentNode nodeTypeUnresolved;
		private NodeTypeNode nodeType;

		public SourceExprNode(Coords coords, ExprNode edge, IdentNode nodeType)
			: base(coords)
		{
			this.edge = edge;
			BecomeParent(this.edge);
			this.nodeTypeUnresolved = nodeType;
			BecomeParent(this.nodeTypeUnresolved);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(edge);
				children.Add(GetValidVersion(nodeTypeUnresolved, nodeType));
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
				childrenNames.Add("edge");
				childrenNames.Add("nodeType");
				return childrenNames;
			}
		}

		private static readonly DeclarationTypeResolver<NodeTypeNode> nodeTypeResolver =
				new DeclarationTypeResolver<NodeTypeNode>(typeof(NodeTypeNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			nodeType = nodeTypeResolver.Resolve(nodeTypeUnresolved, this);
			return nodeType != null && Type.Resolve();
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			if(!(edge.Type is EdgeTypeNode))
			{
				ReportError("The function source expects as argument (edgeToGetSourceNodeFrom) a value of type edge"
						+ " (but is given a value of type " + edge.Type.TypeName + ").");
				return false;
			}
			return true;
		}

		protected internal override IR ConstructIR()
		{
			edge = edge.Evaluate();
			return new SourceExpr(edge.CheckIR<Expression>(typeof(Expression)), Type.IRType);
		}

		public override TypeNode Type
		{
			get
			{
				return nodeType;
			}
		}
	}

}
