/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Qualification;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.containers.SetClear;
import de.unika.ipd.grgen.ir.containers.SetVarClear;
import de.unika.ipd.grgen.parser.Coords;

public class SetClearNode extends ProcedureMethodInvocationBaseNode
{
	static {
		setName(SetClearNode.class, "set clear statement");
	}

	private QualIdentNode target;
	private VarDeclNode targetVar;

	public SetClearNode(Coords coords, QualIdentNode target)
	{
		super(coords);
		this.target = becomeParent(target);
	}

	public SetClearNode(Coords coords, VarDeclNode targetVar)
	{
		super(coords);
		this.targetVar = becomeParent(targetVar);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target!=null ? target : targetVar);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		if(target!=null)
			return new SetClear(target.checkIR(Qualification.class));
		else
			return new SetVarClear(targetVar.checkIR(Variable.class));
	}
}
