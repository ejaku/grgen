/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.stmt.graph
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using InsertInducedSubgraphProc = de.unika.ipd.grgen.ir.stmt.graph.InsertInducedSubgraphProc;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding an inserted node of the insertion of an induced subgraph of a node set.
	/// </summary>
	public class InsertInducedSubgraphProcNode : BuiltinProcedureInvocationBaseNode
	{
		static InsertInducedSubgraphProcNode()
		{
			SetClassName(typeof(InsertInducedSubgraphProcNode), "insert induced subgraph procedure");
		}

		private ExprNode nodeSetExpr;
		private ExprNode nodeExpr;

		internal IList<TypeNode> returnTypes;

		public InsertInducedSubgraphProcNode(Coords coords, ExprNode nodeSetExpr, ExprNode nodeExpr)
			: base(coords)
		{
			this.nodeSetExpr = nodeSetExpr;
			BecomeParent(this.nodeSetExpr);
			this.nodeExpr = nodeExpr;
			BecomeParent(this.nodeExpr);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(nodeSetExpr);
				children.Add(nodeExpr);
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
				childrenNames.Add("nodeSetExpr");
				childrenNames.Add("nodeExpr");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			TypeNode nodeSetExprType = nodeSetExpr.Type;
			if(!(nodeSetExprType is SetTypeNode))
			{
				nodeSetExpr.ReportError("The insertInducedSubgraph procedure expects as 1. argument (setOfNodes)"
						+ " a value of type set<Node>"
						+ " (but is given a value of type " + nodeSetExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			SetTypeNode type = (SetTypeNode)nodeSetExprType;
			if(!(type.valueType is NodeTypeNode))
			{
				nodeSetExpr.ReportError("The insertInducedSubgraph procedure expects as 1. argument (setOfNodes)"
						+ " a value of type set<Node>"
						+ " (but is given a value of type " + nodeSetExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			TypeNode nodeExprType = nodeExpr.Type;
			if(!(nodeExprType is NodeTypeNode))
			{
				nodeExpr.ReportError("The insertInducedSubgraph procedure expects as 2. argument (node)"
						+ " a value of type Node"
						+ " (but is given a value of type " + nodeExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			return true;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			nodeSetExpr = nodeSetExpr.Evaluate();
			nodeExpr = nodeExpr.Evaluate();
			InsertInducedSubgraphProc insertInduced = new InsertInducedSubgraphProc(
					nodeSetExpr.CheckIR<Expression>(typeof(Expression)), nodeExpr.CheckIR<Expression>(typeof(Expression)),
					nodeExpr.Type.IRType);
			return insertInduced;
		}

		public override IList<TypeNode> Type
		{
			get
			{
				if(returnTypes == null)
				{
					returnTypes = new List<TypeNode>();
					returnTypes.Add(nodeExpr.Type);
				}
				return returnTypes;
			}
		}
	}

}
