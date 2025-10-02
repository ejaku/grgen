/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr;

import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
import de.unika.ipd.grgen.parser.Coords;

public abstract class ContainerInitNode extends ExprNode
{
	static {
		setName(ContainerInitNode.class, "container init");
	}

	public ContainerInitNode(Coords coords)
	{
		super(coords);
	}
	
	@Override
	public TypeNode getType()
	{
		return getContainerType();
	}

	public abstract ContainerTypeNode getContainerType();

	public abstract boolean isInitInModel();

	protected static boolean isEnumValue(ExprNode expr)
	{
		if(!(expr instanceof DeclExprNode))
			return false;
		if(!(((DeclExprNode)expr).isEnumValue()))
			return false;
		return true;
	}
}
