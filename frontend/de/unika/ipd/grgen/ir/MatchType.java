/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

public class MatchType extends Type {
	Type valueType;

	public MatchType(Type valueType) {
		super("match type", null);
		this.valueType = valueType;
	}

	public MatchType() {
		super("match type", null);
	}

	public Type getValueType() {
		return valueType;
	}

	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_MATCH;
	}
}
