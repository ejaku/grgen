/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.stmt.CaseStatement;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a case statement from a switch statement.
 */
public class CaseStatementNode extends NestingStatementNode
{
	static {
		setName(CaseStatementNode.class, "CaseStatement");
	}

	ExprNode caseConstantExpr; // null for the "else" (aka default) case

	public CaseStatementNode(Coords coords, ExprNode caseConstExpr,
			CollectNode<EvalStatementNode> statements)
	{
		super(coords, statements);
		this.caseConstantExpr = caseConstExpr;
		becomeParent(caseConstExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		if(caseConstantExpr != null)
			children.add(caseConstantExpr);
		children.add(statements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		if(caseConstantExpr != null)
			childrenNames.add("caseConstant");
		childrenNames.add("statements");
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
		if(caseConstantExpr != null)
			caseConstantExpr = caseConstantExpr.evaluate();
		CaseStatement caseStmt = new CaseStatement(
				caseConstantExpr != null ? caseConstantExpr.checkIR(Expression.class) : null);
		for(EvalStatementNode statement : statements.getChildren()) {
			caseStmt.addStatement(statement.checkIR(EvalStatement.class));
		}
		return caseStmt;
	}
}
