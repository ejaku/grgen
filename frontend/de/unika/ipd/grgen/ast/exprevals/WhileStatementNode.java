/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.WhileStatement;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a while statement.
 */
public class WhileStatementNode extends EvalStatementNode {
	static {
		setName(WhileStatementNode.class, "WhileStatement");
	}

	private ExprNode conditionExpr;
	CollectNode<EvalStatementNode> loopedStatements;

	public WhileStatementNode(Coords coords, ExprNode conditionExpr,
			CollectNode<EvalStatementNode> loopedStatements) {
		super(coords);
		this.conditionExpr = conditionExpr;
		becomeParent(conditionExpr);
		this.loopedStatements = loopedStatements;
		becomeParent(this.loopedStatements);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(conditionExpr);
		children.add(loopedStatements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("condition");
		childrenNames.add("loopedStatements");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		WhileStatement ws = new WhileStatement(conditionExpr.checkIR(Expression.class));
		for(EvalStatementNode loopedStatement : loopedStatements.children) 	
			ws.addLoopedStatement(loopedStatement.checkIR(EvalStatement.class));
		return ws;
	}
}
