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
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Graphof = de.unika.ipd.grgen.ir.expr.graph.Graphof;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding the containing graph of some node/edge.
	/// </summary>
	public class GraphofExprNode : BuiltinFunctionInvocationBaseNode
	{
		static GraphofExprNode()
		{
			SetClassName(typeof(GraphofExprNode), "graphof");
		}

		private ExprNode entity;

		public GraphofExprNode(Coords coords, ExprNode entity)
			: base(coords)
		{
			this.entity = entity;
			BecomeParent(this.entity);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(entity);
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
				childrenNames.Add("entity");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"/>
		protected internal override bool CheckLocal()
		{
			if(entity.Type is NodeTypeNode || entity.Type is EdgeTypeNode)
			{
				if(!UnitNode.Root.Model.IsGraphofDefined())
				{
					string nodeOrEdge = entity.Type is NodeTypeNode ? "node" : "edge";
					ReportError("The function graphof applied to an argument of " + nodeOrEdge + " type expects a model with graph containment support, but the required node edge graph; declaration is missing in the model specification.");
					return false;
				}
				return true;
			}

			ReportError("The function graphof expects as argument (entityToFetchContainingGraphOf) a value of type node or edge"
					+ " (but is given a value of type " + entity.Type.TypeName + ").");
			return false;
		}

		protected internal override IR ConstructIR()
		{
			entity = entity.Evaluate();
			return new Graphof(entity.CheckIR(typeof(Expression)), Type.IRType);
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
