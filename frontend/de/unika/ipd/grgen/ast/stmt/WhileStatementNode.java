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
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.WhileStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a while statement.
 */
public class WhileStatementNode extends NestingStatementNode
{
	static {
		setName(WhileStatementNode.class, "WhileStatement");
	}

	private ExprNode conditionExpr;

	public WhileStatementNode(Coords coords, ExprNode conditionExpr, CollectNode<EvalStatementNode> loopedStatements)
	{
		super(coords, loopedStatements);
		this.conditionExpr = conditionExpr;
		becomeParent(conditionExpr);
		this.statements = loopedStatements;
		becomeParent(this.statements);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(conditionExpr);
		children.add(statements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("condition");
		childrenNames.add("loopedStatements");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(!conditionExpr.getType().isEqual(BasicTypeNode.booleanType)) {
			conditionExpr.reportError("while condition must be of type boolean");
			return false;
		}
		return true;
	}

	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		WhileStatement ws = new WhileStatement(conditionExpr.checkIR(Expression.class));
		for(EvalStatementNode loopedStatement : statements.getChildren()) {
			ws.addLoopedStatement(loopedStatement.checkIR(EvalStatement.class));
		}
		return ws;
	}
}
