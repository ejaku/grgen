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
	using GraphMergeProc = de.unika.ipd.grgen.ir.stmt.graph.GraphMergeProc;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class GraphMergeProcNode : BuiltinProcedureInvocationBaseNode
	{
		static GraphMergeProcNode()
		{
			SetClassName(typeof(GraphMergeProcNode), "graph merge procedure");
		}

		private ExprNode targetExpr;
		private ExprNode sourceExpr;
		private ExprNode sourceNameExpr;

		public GraphMergeProcNode(Coords coords, ExprNode targetExpr, ExprNode sourceExpr, ExprNode sourceNameExpr)
			: base(coords)
		{

			this.targetExpr = targetExpr;
			BecomeParent(targetExpr);
			this.sourceExpr = sourceExpr;
			BecomeParent(sourceExpr);
			this.sourceNameExpr = sourceNameExpr;
			if(sourceNameExpr != null)
				BecomeParent(sourceNameExpr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(targetExpr);
				children.Add(sourceExpr);
				if(sourceNameExpr != null)
					children.Add(sourceNameExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("target");
				childrenNames.Add("source");
				if(sourceNameExpr != null)
					childrenNames.Add("sourceName");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			return true;
		}

		protected internal override bool CheckLocal()
		{
			TypeNode targetExprType = targetExpr.Type;
			if(!(targetExprType is NodeTypeNode))
			{
				ReportError("The merge procedure expects as 1. argument (target)"
						+ " a value of type Node"
						+ " (but is given a value of type " + targetExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			TypeNode sourceExprType = sourceExpr.Type;
			if(!(sourceExprType is NodeTypeNode))
			{
				ReportError("The merge procedure expects as 2. argument (source)"
						+ " a value of type Node"
						+ " (but is given a value of type " + sourceExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			if(sourceNameExpr != null)
			{
				TypeNode sourceNameExprType = sourceNameExpr.Type;
				if(!(sourceNameExprType.Equals(BasicTypeNode.stringType)))
				{
					ReportError("The merge procedure expects as 3. argument (sourceName)"
							+ " a value of type string"
							+ " (but is given a value of type " + sourceNameExprType.ToStringWithDeclarationCoords() + ").");
					return false;
				}
			}
			return true;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			targetExpr = targetExpr.Evaluate();
			sourceExpr = sourceExpr.Evaluate();
			if(sourceNameExpr != null)
				sourceNameExpr = sourceNameExpr.Evaluate();
			return new GraphMergeProc(targetExpr.CheckIR<Expression>(typeof(Expression)), sourceExpr.CheckIR<Expression>(typeof(Expression)),
					sourceNameExpr != null ? sourceNameExpr.CheckIR<Expression>(typeof(Expression)) : null);
		}
	}

}
