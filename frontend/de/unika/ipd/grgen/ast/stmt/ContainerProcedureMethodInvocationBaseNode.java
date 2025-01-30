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

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
import de.unika.ipd.grgen.parser.Coords;

public abstract class ContainerProcedureMethodInvocationBaseNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(ContainerProcedureMethodInvocationBaseNode.class, "container procedure method invocation base");
	}

	protected QualIdentNode target;
	protected VarDeclNode targetVar;

	protected ContainerProcedureMethodInvocationBaseNode(Coords coords, QualIdentNode target)
	{
		super(coords);
		this.target = becomeParent(target);
	}

	protected ContainerProcedureMethodInvocationBaseNode(Coords coords, VarDeclNode targetVar)
	{
		super(coords);
		this.targetVar = becomeParent(targetVar);
	}

	protected ContainerTypeNode getTargetType()
	{
		if(target != null) {
			TypeNode targetType = target.getDecl().getDeclType();
			return (ContainerTypeNode)targetType;
		} else {
			TypeNode targetType = targetVar.getDeclType();
			return (ContainerTypeNode)targetType;
		}
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

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}
}
