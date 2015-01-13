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
import de.unika.ipd.grgen.ir.exprevals.CaseStatement;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.SwitchStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a switch statement.
 */
public class SwitchStatementNode extends EvalStatementNode {
	static {
		setName(SwitchStatementNode.class, "SwitchStatement");
	}

	private ExprNode switchExpr;
	CollectNode<CaseStatementNode> cases;
	
	public SwitchStatementNode(Coords coords, ExprNode switchExpr,
			CollectNode<CaseStatementNode> cases) {
		super(coords);
		this.switchExpr = switchExpr;
		becomeParent(switchExpr);
		this.cases = cases;
		becomeParent(this.cases);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(switchExpr);
		children.add(cases);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("switchExpr");
		childrenNames.add("cases");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		if(!(switchExpr.getType().isEqual(BasicTypeNode.byteType))
			&& !(switchExpr.getType().isEqual(BasicTypeNode.shortType))
			&& !(switchExpr.getType().isEqual(BasicTypeNode.intType))
			&& !(switchExpr.getType().isEqual(BasicTypeNode.longType))
			&& !(switchExpr.getType().isEqual(BasicTypeNode.booleanType))
			&& !(switchExpr.getType().isEqual(BasicTypeNode.stringType))
			&& !(switchExpr.getType() instanceof EnumTypeNode)) {
			reportError("the expression switched upon must be of type byte or short or int or long or boolean or string or enum");
			return false;
		}
		boolean defaultVisited = false;
		for(CaseStatementNode caseStmt : cases.getChildren()) {
			if(caseStmt.caseConstantExpr!=null) {
				// just to be sure, the syntax as-such is not allowing non-constants 
				if(!(caseStmt.caseConstantExpr.evaluate() instanceof ConstNode)) {
					caseStmt.reportError("the case of the switch statement must be a constant expression");
					return false;
				}
				if(!(caseStmt.caseConstantExpr.getType().isCompatibleTo(switchExpr.getType()))) {
					caseStmt.reportError("the type of the case is not compatible to the type of the switch expression; case: " + caseStmt.caseConstantExpr.getType().toString() + " switch: " + switchExpr.getType().toString());
					return false;
				}
			} else {
				if(defaultVisited) {
					caseStmt.reportError("only one else branch allowed per switch");
					return false;
				}
				defaultVisited = true;
			}
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}
	
	@Override
	protected IR constructIR() {
		SwitchStatement switchStmt = new SwitchStatement(switchExpr.checkIR(Expression.class));
		for(EvalStatementNode statement : cases.children) 	
			switchStmt.addStatement(statement.checkIR(CaseStatement.class));
		return switchStmt;
	}
}
