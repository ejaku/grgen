/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.deque;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ir.containers.DequeClear;
import de.unika.ipd.grgen.ir.containers.DequeVarClear;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.exprevals.Qualification;
import de.unika.ipd.grgen.parser.Coords;

public class DequeClearNode extends DequeProcedureMethodInvocationBaseNode
{
	static {
		setName(DequeClearNode.class, "deque clear statement");
	}

	public DequeClearNode(Coords coords, QualIdentNode target)
	{
		super(coords, target);
	}

	public DequeClearNode(Coords coords, VarDeclNode targetVar)
	{
		super(coords, targetVar);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target != null ? target : targetVar);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		if(target != null)
			return new DequeClear(target.checkIR(Qualification.class));
		else
			return new DequeVarClear(targetVar.checkIR(Variable.class));
	}
}
