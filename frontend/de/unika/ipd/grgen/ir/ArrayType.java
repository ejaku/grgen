/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

public class ArrayType extends Type {
	Type valueType;

	public ArrayType(Type valueType) {
		super("array type", null);
		this.valueType = valueType;
	}

	public Type getValueType() {
		return valueType;
	}
	
	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_SET;
	}
}
