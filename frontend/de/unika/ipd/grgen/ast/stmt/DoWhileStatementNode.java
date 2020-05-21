/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ir.stmt.DoWhileStatement;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a do while statement.
 */
public class DoWhileStatementNode extends NestingStatementNode
{
	static {
		setName(DoWhileStatementNode.class, "DoWhileStatement");
	}

	private ExprNode conditionExpr;

	public DoWhileStatementNode(Coords coords,
			CollectNode<EvalStatementNode> loopedStatements,
			ExprNode conditionExpr)
	{
		super(coords, loopedStatements);
		this.statements = loopedStatements;
		becomeParent(this.statements);
		this.conditionExpr = conditionExpr;
		becomeParent(conditionExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(statements);
		children.add(conditionExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("loopedStatements");
		childrenNames.add("condition");
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
		if(!conditionExpr.getType().isEqual(BasicTypeNode.booleanType)) {
			conditionExpr.reportError("do-while condition must be of type boolean");
			return false;
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		DoWhileStatement dws = new DoWhileStatement(conditionExpr.checkIR(Expression.class));
		for(EvalStatementNode loopedStatement : statements.getChildren()) {
			dws.addLoopedStatement(loopedStatement.checkIR(EvalStatement.class));
		}
		return dws;
	}
}
