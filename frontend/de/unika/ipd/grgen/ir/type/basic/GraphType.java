/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.type.basic;

import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * A graph type.
 */
public class GraphType extends PrimitiveType
{
	public GraphType(Ident ident)
	{
		super("graph type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	@Override
	public int classify()
	{
		return IS_GRAPH;
	}

	public static Type getType()
	{
		return BasicTypeNode.graphType.checkIR(Type.class);
	}
}
