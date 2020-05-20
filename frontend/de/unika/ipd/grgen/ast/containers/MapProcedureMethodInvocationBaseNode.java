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

public abstract class MapProcedureMethodInvocationBaseNode extends ContainerProcedureMethodInvocationBaseNode
{
	static {
		setName(MapProcedureMethodInvocationBaseNode.class, "map procedure method invocation base");
	}

	protected MapProcedureMethodInvocationBaseNode(Coords coords, QualIdentNode target)
	{
		super(coords, target);
	}

	protected MapProcedureMethodInvocationBaseNode(Coords coords, VarDeclNode targetVar)
	{
		super(coords, targetVar);
	}

	@Override
	protected MapTypeNode getTargetType()
	{
		return (MapTypeNode)super.getTargetType();
	}
}
