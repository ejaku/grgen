/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * EnumExpression.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

public class EnumExpression extends Constant {

	private final EnumItem item;

	public EnumExpression(EnumType type, EnumItem item) {
		super(type, item.getValue().getValue());
		this.item = item;
		setName("enum expression");
	}

	public EnumItem getEnumItem() {
		return item;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel() */
	public String getNodeLabel() {
		return item + " " + getValue();
	}
}

