/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.map;

import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.stmt.ContainerProcedureMethodInvocationBaseNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
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
