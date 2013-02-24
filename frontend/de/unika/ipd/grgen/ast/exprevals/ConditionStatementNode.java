/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.exprevals.ConditionStatement;
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a condition statement.
 */
public class ConditionStatementNode extends EvalStatementNode {
	static {
		setName(ConditionStatementNode.class, "ConditionStatement");
	}

	private ExprNode conditionExpr;
	CollectNode<EvalStatementNode> trueCaseStatements;
	CollectNode<EvalStatementNode> falseCaseStatements;

	public ConditionStatementNode(Coords coords, ExprNode conditionExpr,
			CollectNode<EvalStatementNode> trueCaseStatements, 
			CollectNode<EvalStatementNode> falseCaseStatements) {
		super(coords);
		this.conditionExpr = conditionExpr;
		becomeParent(conditionExpr);
		this.trueCaseStatements = trueCaseStatements;
		becomeParent(this.trueCaseStatements);
		this.falseCaseStatements = falseCaseStatements;
		if(falseCaseStatements!=null)
			becomeParent(this.falseCaseStatements);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(conditionExpr);
		children.add(trueCaseStatements);
		if(falseCaseStatements!=null)
			children.add(falseCaseStatements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("condition");
		childrenNames.add("trueCaseStatements");
		if(falseCaseStatements!=null)
			childrenNames.add("falseCaseStatements");
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
	
	@Override
	protected IR constructIR() {
		ConditionStatement cond = new ConditionStatement(conditionExpr.checkIR(Expression.class));
		for(EvalStatementNode trueCaseStatement : trueCaseStatements.children) 	
			cond.addTrueCaseStatement(trueCaseStatement.checkIR(EvalStatement.class));
		if(falseCaseStatements!=null)
			for(EvalStatementNode falseCaseStatement : falseCaseStatements.children) 	
				cond.addFalseCaseStatement(falseCaseStatement.checkIR(EvalStatement.class));
		return cond;
	}
}
