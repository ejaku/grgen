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
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.stmt.ConditionStatement;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a condition statement.
 */
public class ConditionStatementNode extends NestingStatementNode
{
	static {
		setName(ConditionStatementNode.class, "ConditionStatement");
	}

	private ExprNode conditionExpr;
	CollectNode<EvalStatementNode> falseCaseStatements;

	public ConditionStatementNode(Coords coords, ExprNode conditionExpr,
			CollectNode<EvalStatementNode> trueCaseStatements,
			CollectNode<EvalStatementNode> falseCaseStatements)
	{
		super(coords, trueCaseStatements);
		this.conditionExpr = conditionExpr;
		becomeParent(conditionExpr);
		this.falseCaseStatements = falseCaseStatements;
		if(falseCaseStatements != null)
			becomeParent(this.falseCaseStatements);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(conditionExpr);
		children.add(statements);
		if(falseCaseStatements != null)
			children.add(falseCaseStatements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("condition");
		childrenNames.add("trueCaseStatements");
		if(falseCaseStatements != null)
			childrenNames.add("falseCaseStatements");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		TypeNode conditionExprType = conditionExpr.getType();
		if(!conditionExprType.isEqual(BasicTypeNode.booleanType)) {
			conditionExpr.reportError("The condition of the if statement must be of type boolean"
					+ " (but is of type " + conditionExprType + " [declared at " + conditionExprType.getCoords() + "]" + ").");
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
		conditionExpr = conditionExpr.evaluate();
		ConditionStatement cond = new ConditionStatement(conditionExpr.checkIR(Expression.class));
		for(EvalStatementNode trueCaseStatement : statements.getChildren()) {
			cond.addStatement(trueCaseStatement.checkIR(EvalStatement.class));
		}
		if(falseCaseStatements != null) {
			for(EvalStatementNode falseCaseStatement : falseCaseStatements.getChildren()) {
				cond.addFalseCaseStatement(falseCaseStatement.checkIR(EvalStatement.class));
			}
		}
		return cond;
	}
}
