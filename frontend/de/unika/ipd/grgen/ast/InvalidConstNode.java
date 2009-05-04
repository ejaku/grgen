/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;


class InvalidConstNode extends ConstNode
{
	static {
		setName(InvalidConstNode.class, "invalid const");
	}

	public InvalidConstNode(Coords coords, String name, Object value) {
		super(coords, name, value);
	}

	protected boolean isValid() {
		return false;
	}

	protected ConstNode doCastTo(TypeNode type) {
		return this;
	}

	public String toString() {
		return "invalid const";
	}
}

