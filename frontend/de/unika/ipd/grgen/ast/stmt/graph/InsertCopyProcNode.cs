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
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using InsertCopyProc = de.unika.ipd.grgen.ir.stmt.graph.InsertCopyProc;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node for inserting a copy of the subgraph to the given main graph.
	/// </summary>
	public class InsertCopyProcNode : BuiltinProcedureInvocationBaseNode
	{
		static InsertCopyProcNode()
		{
			SetClassName(typeof(InsertCopyProcNode), "insert copy procedure");
		}

		private ExprNode graphExpr;
		private ExprNode nodeExpr;

		internal IList<TypeNode> returnTypes;

		public InsertCopyProcNode(Coords coords, ExprNode nodeSetExpr, ExprNode nodeExpr)
			: base(coords)
		{
			this.graphExpr = nodeSetExpr;
			BecomeParent(this.graphExpr);
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
				children.Add(graphExpr);
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
			TypeNode graphExprType = graphExpr.Type;
			if(!(graphExprType.Equals(BasicTypeNode.graphType)))
			{
				ReportError("The insertCopy procedure expects as 1. argument (subgraphToCopyAndInsertIntoTheCurrentGraph)"
						+ " a value of type graph"
						+ " (but is given a value of type " + graphExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			TypeNode nodeExprType = nodeExpr.Type;
			if(!(nodeExprType is NodeTypeNode))
			{
				ReportError("The insertCopy procedure expects as 2. argument (nodeToReturnCopyOf)"
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
			graphExpr = graphExpr.Evaluate();
			nodeExpr = nodeExpr.Evaluate();
			InsertCopyProc insertCopy = new InsertCopyProc(graphExpr.CheckIR<Expression>(typeof(Expression)),
					nodeExpr.CheckIR<Expression>(typeof(Expression)), nodeExpr.Type.IRType);
			return insertCopy;
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
