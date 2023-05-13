/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.set;

import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.stmt.ContainerProcedureMethodInvocationBaseNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.parser.Coords;

public abstract class SetProcedureMethodInvocationBaseNode extends ContainerProcedureMethodInvocationBaseNode
{
	static {
		setName(SetProcedureMethodInvocationBaseNode.class, "set procedure method invocation base");
	}

	protected SetProcedureMethodInvocationBaseNode(Coords coords, QualIdentNode target)
	{
		super(coords, target);
	}

	protected SetProcedureMethodInvocationBaseNode(Coords coords, VarDeclNode targetVar)
	{
		super(coords, targetVar);
	}

	@Override
	protected SetTypeNode getTargetType()
	{
		return (SetTypeNode)super.getTargetType();
	}
}
