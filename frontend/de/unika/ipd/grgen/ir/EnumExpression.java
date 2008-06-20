/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * EnumExpression.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

public class EnumExpression extends Constant {
	private EnumItem item;

	// Constructor for later initialization when EnumType and EnumItem have been constructed.
	// See EnumTypeNode.constructIR().
	public EnumExpression(int value) {
		super(null, value);
		setName("enum expression");
	}

	public EnumExpression(EnumType type, EnumItem item) {
		super(type, item.getValue().getValue());
		this.item = item;
		setName("enum expression");
	}

	public void lateInit(EnumType type, EnumItem item) {
		this.type = type;
		this.item = item;
	}

	public EnumItem getEnumItem() {
		return item;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel() */
	public String getNodeLabel() {
		return item + " " + getValue();
	}
}

