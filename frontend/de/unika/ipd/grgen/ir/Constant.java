/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

public class Constant extends Expression {

	/** The value of the constant. */
	protected Object value;

	/**
	 * @param type The type of the constant.
	 * @param value The value of the constant.
	 */
	public Constant(Type type, Object value) {
		super("constant", type);
		this.value = value;
	}

	/** @return The value of the constant. */
	public Object getValue() {
		return value;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel() */
	public String getNodeLabel() {
		return getName() + " " + value;
	}

	public void collectNeededEntities(NeededEntities needs) { }
}
