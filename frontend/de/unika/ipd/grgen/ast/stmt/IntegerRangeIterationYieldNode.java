/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.IntegerRangeIterationYield;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an integer range iteration.
 */
public class IntegerRangeIterationYieldNode extends NestingStatementNode
{
	static {
		setName(IntegerRangeIterationYieldNode.class, "IntegerRangeIterationYield");
	}

	BaseNode iterationVariableUnresolved;
	ExprNode leftExpr;
	ExprNode rightExpr;

	VarDeclNode iterationVariable;

	public IntegerRangeIterationYieldNode(Coords coords, BaseNode iterationVariable, ExprNode left, ExprNode right,
			CollectNode<EvalStatementNode> accumulationStatements)
	{
		super(coords, accumulationStatements);
		this.iterationVariableUnresolved = iterationVariable;
		becomeParent(this.iterationVariableUnresolved);
		this.leftExpr = left;
		becomeParent(this.leftExpr);
		this.rightExpr = right;
		becomeParent(this.rightExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iterationVariableUnresolved, iterationVariable));
		children.add(leftExpr);
		children.add(rightExpr);
		children.add(statements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterationVariable");
		childrenNames.add("left");
		childrenNames.add("right");
		childrenNames.add("accumulationStatements");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;

		if(iterationVariableUnresolved instanceof VarDeclNode) {
			iterationVariable = (VarDeclNode)iterationVariableUnresolved;
		} else {
			reportError("Error in resolving the iteration variable of the for integer range loop.");
			successfullyResolved = false;
		}

		if(!iterationVariable.resolve())
			successfullyResolved = false;

		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal()
	{
		TypeNode iterationVariableType = iterationVariable.getDeclType();
		if(!iterationVariableType.isEqual(BasicTypeNode.intType)) {
			reportError("The for integer range loop expects an iteration variable of type int"
					+ " (but is given " + iterationVariableType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode leftExprType = leftExpr.getType();
		if(!leftExprType.isEqual(BasicTypeNode.intType)) {
			reportError("The for integer range loop expects a left bound of type int"
					+ " (but is given " + leftExprType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode rightExprType = rightExpr.getType();
		if(!rightExprType.isEqual(BasicTypeNode.intType)) {
			reportError("The for integer range loop expects a right bound of type int"
					+ " (but is given " + rightExprType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		return true;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		leftExpr = leftExpr.evaluate();
		rightExpr = rightExpr.evaluate();
		IntegerRangeIterationYield cay = new IntegerRangeIterationYield(iterationVariable.checkIR(Variable.class),
				leftExpr.checkIR(Expression.class), rightExpr.checkIR(Expression.class));
		for(EvalStatementNode accumulationStatement : statements.getChildren()) {
			cay.addStatement(accumulationStatement.checkIR(EvalStatement.class));
		}
		return cay;
	}
}
