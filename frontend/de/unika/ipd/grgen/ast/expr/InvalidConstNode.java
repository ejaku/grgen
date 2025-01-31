/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr;

import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.parser.Coords;

public class InvalidConstNode extends ConstNode
{
	static {
		setName(InvalidConstNode.class, "invalid const");
	}

	public InvalidConstNode(Coords coords, String name, Object value)
	{
		super(coords, name, value);
	}

	@Override
	protected ConstNode doCastTo(TypeNode type)
	{
		return this;
	}

	@Override
	public String toString()
	{
		return "invalid const";
	}
}
