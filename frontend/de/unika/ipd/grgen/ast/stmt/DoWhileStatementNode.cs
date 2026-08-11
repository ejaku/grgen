/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using DoWhileStatement = de.unika.ipd.grgen.ir.stmt.DoWhileStatement;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node representing a do while statement.
	/// </summary>
	public class DoWhileStatementNode : NestingStatementNode
	{
		static DoWhileStatementNode()
		{
			SetClassName(typeof(DoWhileStatementNode), "DoWhileStatement");
		}

		private ExprNode conditionExpr;

		public DoWhileStatementNode(Coords coords,
				CollectNode<EvalStatementNode> loopedStatements,
				ExprNode conditionExpr)
			 : base(coords, loopedStatements)
		{
			this.statements = loopedStatements;
			BecomeParent(this.statements);
			this.conditionExpr = conditionExpr;
			BecomeParent(conditionExpr);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(statements);
				children.Add(conditionExpr);
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
				childrenNames.Add("loopedStatements");
				childrenNames.Add("condition");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			return true;
		}

		protected internal override bool CheckLocal()
		{
			TypeNode conditionExprType = conditionExpr.Type;
			if(!conditionExprType.IsEqual(BasicTypeNode.booleanType))
			{
				conditionExpr.ReportError("The condition of the do-while loop must be of type boolean"
						+ " (but is of type " + conditionExprType.ToStringWithDeclarationCoords() + ").");
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
			conditionExpr = conditionExpr.Evaluate();
			DoWhileStatement dws = new DoWhileStatement(conditionExpr.CheckIR(typeof(Expression)));
			foreach(EvalStatementNode loopedStatement in statements.ChildrenExact)
				dws.AddStatement(loopedStatement.CheckIR(typeof(EvalStatement)));
			return dws;
		}
	}

}
