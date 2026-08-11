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
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using WhileStatement = de.unika.ipd.grgen.ir.stmt.WhileStatement;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node representing a while statement.
	/// </summary>
	public class WhileStatementNode : NestingStatementNode
	{
		static WhileStatementNode()
		{
			SetClassName(typeof(WhileStatementNode), "WhileStatement");
		}

		private ExprNode conditionExpr;

		public WhileStatementNode(Coords coords, ExprNode conditionExpr, CollectNode<EvalStatementNode> loopedStatements)
			: base(coords, loopedStatements)
		{
			this.conditionExpr = conditionExpr;
			BecomeParent(conditionExpr);
			this.statements = loopedStatements;
			BecomeParent(this.statements);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(conditionExpr);
				children.Add(statements);
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
				childrenNames.Add("condition");
				childrenNames.Add("loopedStatements");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			TypeNode conditionExprType = conditionExpr.Type;
			if(!conditionExprType.IsEqual(BasicTypeNode.booleanType))
			{
				conditionExpr.ReportError("The condition of the while loop must be of type boolean"
						+ " (but is of type " + conditionExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			return true;
		}

		protected internal override bool ResolveLocal()
		{
			return true;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			conditionExpr = conditionExpr.Evaluate();
			WhileStatement ws = new WhileStatement(conditionExpr.CheckIR(typeof(Expression)));
			foreach(EvalStatementNode loopedStatement in statements.ChildrenExact)
				ws.AddStatement(loopedStatement.CheckIR(typeof(EvalStatement)));
			return ws;
		}
	}

}
