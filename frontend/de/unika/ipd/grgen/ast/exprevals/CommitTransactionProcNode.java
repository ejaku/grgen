/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
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
import de.unika.ipd.grgen.ir.exprevals.CommitTransactionProc;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class CommitTransactionProcNode extends ProcedureInvocationBaseNode {
	static {
		setName(CommitTransactionProcNode.class, "commit transaction procedure");
	}

	private ExprNode transactionIdExpr;


	public CommitTransactionProcNode(Coords coords, ExprNode transactionIdExpr) {
		super(coords);

		this.transactionIdExpr = becomeParent(transactionIdExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(transactionIdExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("transactionIdExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		if(!transactionIdExpr.getType().isEqual(BasicTypeNode.intType)) {
			transactionIdExpr.reportError("Argument (transaction id) to commitTransaction statement must be of type int");
			return false;
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		return new CommitTransactionProc(transactionIdExpr.checkIR(Expression.class));
	}
}
