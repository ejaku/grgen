/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
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
import de.unika.ipd.grgen.ir.exprevals.DoWhileStatement;
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a do while statement.
 */
public class DoWhileStatementNode extends EvalStatementNode {
	static {
		setName(DoWhileStatementNode.class, "DoWhileStatement");
	}

	CollectNode<EvalStatementNode> loopedStatements;
	private ExprNode conditionExpr;

	public DoWhileStatementNode(Coords coords, 
			CollectNode<EvalStatementNode> loopedStatements,
			ExprNode conditionExpr) {
		super(coords);
		this.loopedStatements = loopedStatements;
		becomeParent(this.loopedStatements);
		this.conditionExpr = conditionExpr;
		becomeParent(conditionExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(loopedStatements);
		children.add(conditionExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("loopedStatements");
		childrenNames.add("condition");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}
	
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		DoWhileStatement dws = new DoWhileStatement(conditionExpr.checkIR(Expression.class));
		for(EvalStatementNode loopedStatement : loopedStatements.children) 	
			dws.addLoopedStatement(loopedStatement.checkIR(EvalStatement.class));
		return dws;
	}
}
