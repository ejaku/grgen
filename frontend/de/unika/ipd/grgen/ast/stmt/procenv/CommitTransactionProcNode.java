/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.procenv;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.procenv.CommitTransactionProc;
import de.unika.ipd.grgen.parser.Coords;

public class CommitTransactionProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(CommitTransactionProcNode.class, "commit transaction procedure");
	}

	private ExprNode transactionIdExpr;

	public CommitTransactionProcNode(Coords coords, ExprNode transactionIdExpr)
	{
		super(coords);

		this.transactionIdExpr = becomeParent(transactionIdExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(transactionIdExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("transactionIdExpr");
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
		TypeNode transactionIdExprType = transactionIdExpr.getType();
		if(!transactionIdExprType.isEqual(BasicTypeNode.intType)) {
			transactionIdExpr.reportError("The Transaction::commit procedure expects as argument (transaction id)"
					+ " a value of type int"
					+ " (but is given a value of type " + transactionIdExprType.toStringWithDeclarationCoords() + ").");
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
		transactionIdExpr = transactionIdExpr.evaluate();
		return new CommitTransactionProc(transactionIdExpr.checkIR(Expression.class));
	}
}
