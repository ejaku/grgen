/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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

