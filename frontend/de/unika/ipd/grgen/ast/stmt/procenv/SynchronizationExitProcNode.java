/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.procenv.SynchronizationExitProc;
import de.unika.ipd.grgen.parser.Coords;

public class SynchronizationExitProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(SynchronizationExitProcNode.class, "synchronization exit procedure");
	}

	private ExprNode criticalSectionObjectExpr;

	public SynchronizationExitProcNode(Coords coords, ExprNode criticalSectionObjectExpr)
	{
		super(coords);

		this.criticalSectionObjectExpr = becomeParent(criticalSectionObjectExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(criticalSectionObjectExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("criticalSectionObjectExpr");
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
		if(!criticalSectionObjectExpr.getType().isLockableType()) {
			criticalSectionObjectExpr.reportError("Argument (critical section object) to synchronization exit statement must be not a basic type (with exception of type object).");
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
		criticalSectionObjectExpr = criticalSectionObjectExpr.evaluate();
		return new SynchronizationExitProc(criticalSectionObjectExpr.checkIR(Expression.class));
	}
}
