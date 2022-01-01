/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.LockStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a lock statement.
 */
public class LockStatementNode extends NestingStatementNode
{
	static {
		setName(LockStatementNode.class, "LockStatement");
	}

	private ExprNode lockObjectExpr;

	public LockStatementNode(Coords coords, ExprNode lockObjectExpr, CollectNode<EvalStatementNode> lockedStatements)
	{
		super(coords, lockedStatements);
		this.lockObjectExpr = lockObjectExpr;
		becomeParent(lockObjectExpr);
		this.statements = lockedStatements;
		becomeParent(this.statements);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(lockObjectExpr);
		children.add(statements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lockObject");
		childrenNames.add("lockedStatements");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(!lockObjectExpr.getType().isLockableType()) {
			lockObjectExpr.reportError("lock object must be a non-basic type (with execption of object)");
			return false;
		}
		return true;
	}

	@Override
	protected boolean resolveLocal()
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
		lockObjectExpr = lockObjectExpr.evaluate();
		LockStatement ls = new LockStatement(lockObjectExpr.checkIR(Expression.class));
		for(EvalStatementNode lockedStatement : statements.getChildren()) {
			ls.addStatement(lockedStatement.checkIR(EvalStatement.class));
		}
		return ls;
	}
}
