/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.IntegerRangeIterationYield;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an integer range iteration.
 */
public class IntegerRangeIterationYieldNode extends EvalStatementNode {
	static {
		setName(IntegerRangeIterationYieldNode.class, "IntegerRangeIterationYield");
	}

	BaseNode iterationVariableUnresolved;
	ExprNode leftExpr;
	ExprNode rightExpr;

	VarDeclNode iterationVariable;
	CollectNode<EvalStatementNode> accumulationStatements;

	public IntegerRangeIterationYieldNode(Coords coords, BaseNode iterationVariable, 
			ExprNode left, ExprNode right, CollectNode<EvalStatementNode> accumulationStatements) {
		super(coords);
		this.iterationVariableUnresolved = iterationVariable;
		becomeParent(this.iterationVariableUnresolved);
		this.leftExpr = left;
		becomeParent(this.leftExpr);
		this.rightExpr = right;
		becomeParent(this.rightExpr);
		this.accumulationStatements = accumulationStatements;
		becomeParent(this.accumulationStatements);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iterationVariableUnresolved, iterationVariable));
		children.add(leftExpr);
		children.add(rightExpr);
		children.add(accumulationStatements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterationVariable");
		childrenNames.add("left");
		childrenNames.add("right");
		childrenNames.add("accumulationStatements");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;

		if(iterationVariableUnresolved instanceof VarDeclNode) {
			iterationVariable = (VarDeclNode)iterationVariableUnresolved;
		} else {
			reportError("error in resolving iteration variable of integer range iteration.");
			successfullyResolved = false;
		}

		if(!iterationVariable.resolve())
			successfullyResolved = false;

		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal() {
		if(!iterationVariable.getDeclType().isEqual(BasicTypeNode.intType)) {
			reportError("integer range iteration variable must be of type int");
			return false;
		}
		if(!leftExpr.getType().isEqual(BasicTypeNode.intType)) {
			reportError("left bound in integer range iteration must be of type int");
			return false;
		}
		if(!rightExpr.getType().isEqual(BasicTypeNode.intType)) {
			reportError("right bound in integer range iteration must be of type int");
			return false;
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		IntegerRangeIterationYield cay = new IntegerRangeIterationYield(
				iterationVariable.checkIR(Variable.class),
				leftExpr.checkIR(Expression.class),
				rightExpr.checkIR(Expression.class));
		for(EvalStatementNode accumulationStatement : accumulationStatements.children) 	
			cay.addAccumulationStatement(accumulationStatement.checkIR(EvalStatement.class));
		return cay;
	}
}
