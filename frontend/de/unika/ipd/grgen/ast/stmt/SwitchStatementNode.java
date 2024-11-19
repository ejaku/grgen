/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.stmt.CaseStatement;
import de.unika.ipd.grgen.ir.stmt.SwitchStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a switch statement.
 */
public class SwitchStatementNode extends EvalStatementNode
{
	static {
		setName(SwitchStatementNode.class, "SwitchStatement");
	}

	private ExprNode switchExpr;
	CollectNode<CaseStatementNode> cases;

	public SwitchStatementNode(Coords coords, ExprNode switchExpr, CollectNode<CaseStatementNode> cases)
	{
		super(coords);
		this.switchExpr = switchExpr;
		becomeParent(switchExpr);
		this.cases = cases;
		becomeParent(this.cases);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(switchExpr);
		children.add(cases);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("switchExpr");
		childrenNames.add("cases");
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
		TypeNode switchExprType = switchExpr.getType();
		if(!(switchExprType.isEqual(BasicTypeNode.byteType))
				&& !(switchExprType.isEqual(BasicTypeNode.shortType))
				&& !(switchExprType.isEqual(BasicTypeNode.intType))
				&& !(switchExprType.isEqual(BasicTypeNode.longType))
				&& !(switchExprType.isEqual(BasicTypeNode.booleanType))
				&& !(switchExprType.isEqual(BasicTypeNode.stringType))
				&& !(switchExprType instanceof EnumTypeNode)) {
			reportError("The expression switched upon must be of type byte or short or int or long or boolean or string or enum,"
					+ " but is of type " + switchExprType.toStringWithDeclarationCoords() + ".");
			return false;
		}
		boolean defaultVisited = false;
		for(CaseStatementNode caseStmt : cases.getChildren()) {
			ExprNode caseConstantExpr = caseStmt.caseConstantExpr;
			if(caseConstantExpr != null) {
				// just to be sure, the syntax as-such is not allowing non-constants 
				if(!(caseConstantExpr.evaluate() instanceof ConstNode)) {
					caseStmt.reportError("A case statement of a switch statement expects a constant expression.");
					return false;
				}
				TypeNode caseConstantExprType = caseConstantExpr.getType();
				if(!(caseConstantExprType.isCompatibleTo(switchExprType))) {
					caseStmt.reportError("The type " + caseConstantExprType.toStringWithDeclarationCoords() + " of the case expression"
							+ " is not compatible to the type " + switchExprType.toStringWithDeclarationCoords() + " of the switch expression.");
					return false;
				}
			} else {
				if(defaultVisited) {
					caseStmt.reportError("Only one else branch allowed per switch.");
					return false;
				}
				defaultVisited = true;
			}
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
		switchExpr = switchExpr.evaluate();
		SwitchStatement switchStmt = new SwitchStatement(switchExpr.checkIR(Expression.class));
		for(EvalStatementNode statement : cases.getChildren()) {
			switchStmt.addStatement(statement.checkIR(CaseStatement.class));
		}
		return switchStmt;
	}
}
