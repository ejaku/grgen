/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.parser.Coords;

public abstract class ArrayProcedureMethodInvocationBaseNode extends ContainerProcedureMethodInvocationBaseNode
{
	static {
		setName(ArrayProcedureMethodInvocationBaseNode.class, "array procedure method invocation base");
	}

	protected ArrayProcedureMethodInvocationBaseNode(Coords coords, QualIdentNode target)
	{
		super(coords, target);
	}

	protected ArrayProcedureMethodInvocationBaseNode(Coords coords, VarDeclNode targetVar)
	{
		super(coords, targetVar);
	}

	@Override
	protected ArrayTypeNode getTargetType()
	{
		if(target != null) {
			TypeNode targetType = target.getDecl().getDeclType();
			return (ArrayTypeNode)targetType;
		} else {
			TypeNode targetType = targetVar.getDeclType();
			return (ArrayTypeNode)targetType;
		}
	}
}
