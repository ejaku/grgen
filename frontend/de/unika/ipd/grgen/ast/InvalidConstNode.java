/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;


public class InvalidConstNode extends ConstNode
{
	static {
		setName(InvalidConstNode.class, "invalid const");
	}

	public InvalidConstNode(Coords coords, String name, Object value) {
		super(coords, name, value);
	}

	@Override
	protected ConstNode doCastTo(TypeNode type) {
		return this;
	}

	@Override
	public String toString() {
		return "invalid const";
	}
}

