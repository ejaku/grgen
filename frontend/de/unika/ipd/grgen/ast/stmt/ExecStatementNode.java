/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.exprevals.ExecStatement;
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.IR;

/**
 * AST node representing an embedded exec statement.
 */
public class ExecStatementNode extends EvalStatementNode
{
	static {
		setName(ExecStatementNode.class, "ExecStatement");
	}

	ExecNode exec;

	public int context;

	public ExecStatementNode(ExecNode exec, int context)
	{
		super(exec.getCoords());
		this.exec = exec;
		becomeParent(this.exec);
		this.context = context;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(exec);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("exec");
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
		if((context & BaseNode.CONTEXT_COMPUTATION) == BaseNode.CONTEXT_COMPUTATION) {
			if((context & BaseNode.CONTEXT_METHOD) == BaseNode.CONTEXT_METHOD) {
				reportError("exec not allowed in method");
				return false;
			} else if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION) {
				reportError("exec not allowed in function");
				return false;
			}
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	public boolean noExecStatement(boolean inEvalHereContext)
	{
		if(inEvalHereContext) {
			reportError("An exec inside an evalhere is forbidden"
					+ " (you may move it outside the evalhere, but note that it is then executed at the end of rewriting).");
		} else {
			reportError("An exec inside an eval is forbidden in an alternative or iterated -- move it outside the eval"
					+ " (so it becomes a deferred exec, executed at the end of rewriting, on the by-then current graph and the local entities valid at the end of its local rewriting).");
		}
		return false;
	}

	@Override
	protected IR constructIR()
	{
		ExecStatement ws = new ExecStatement(exec.checkIR(Exec.class));
		return ws;
	}
}
