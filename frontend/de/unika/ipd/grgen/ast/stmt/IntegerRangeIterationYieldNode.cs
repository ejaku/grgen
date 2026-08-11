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
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using IntegerRangeIterationYield = de.unika.ipd.grgen.ir.stmt.IntegerRangeIterationYield;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node representing an integer range iteration.
	/// </summary>
	public class IntegerRangeIterationYieldNode : NestingStatementNode
	{
		static IntegerRangeIterationYieldNode()
		{
			SetClassName(typeof(IntegerRangeIterationYieldNode), "IntegerRangeIterationYield");
		}

		internal BaseNode iterationVariableUnresolved;
		internal ExprNode leftExpr;
		internal ExprNode rightExpr;

		internal VarDeclNode iterationVariable;

		public IntegerRangeIterationYieldNode(Coords coords, BaseNode iterationVariable, ExprNode left, ExprNode right,
				CollectNode<EvalStatementNode> accumulationStatements)
			: base(coords, accumulationStatements)
		{
			this.iterationVariableUnresolved = iterationVariable;
			BecomeParent(this.iterationVariableUnresolved);
			this.leftExpr = left;
			BecomeParent(this.leftExpr);
			this.rightExpr = right;
			BecomeParent(this.rightExpr);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(iterationVariableUnresolved, iterationVariable));
				children.Add(leftExpr);
				children.Add(rightExpr);
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
				childrenNames.Add("iterationVariable");
				childrenNames.Add("left");
				childrenNames.Add("right");
				childrenNames.Add("accumulationStatements");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = true;

			if(iterationVariableUnresolved is VarDeclNode)
				iterationVariable = (VarDeclNode)iterationVariableUnresolved;
			else
			{
				ReportError("Error in resolving the iteration variable of the for integer range loop.");
				successfullyResolved = false;
			}

			if(!iterationVariable.Resolve())
				successfullyResolved = false;

			return successfullyResolved;
		}

		protected internal override bool CheckLocal()
		{
			TypeNode iterationVariableType = iterationVariable.DeclType;
			if(!iterationVariableType.IsEqual(BasicTypeNode.intType))
			{
				ReportError("The for integer range loop expects an iteration variable of type int"
						+ " (but is given " + iterationVariableType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			TypeNode leftExprType = leftExpr.Type;
			if(!leftExprType.IsEqual(BasicTypeNode.intType))
			{
				ReportError("The for integer range loop expects a left bound of type int"
						+ " (but is given " + leftExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			TypeNode rightExprType = rightExpr.Type;
			if(!rightExprType.IsEqual(BasicTypeNode.intType))
			{
				ReportError("The for integer range loop expects a right bound of type int"
						+ " (but is given " + rightExprType.ToStringWithDeclarationCoords() + ").");
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
			leftExpr = leftExpr.Evaluate();
			rightExpr = rightExpr.Evaluate();
			IntegerRangeIterationYield cay = new IntegerRangeIterationYield(iterationVariable.CheckIR(typeof(Variable)),
					leftExpr.CheckIR(typeof(Expression)), rightExpr.CheckIR(typeof(Expression)));
			foreach(EvalStatementNode accumulationStatement in statements.ChildrenExact)
				cay.AddStatement(accumulationStatement.CheckIR(typeof(EvalStatement)));
			return cay;
		}
	}

}
