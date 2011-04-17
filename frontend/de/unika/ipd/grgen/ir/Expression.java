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


/**
 * Abstract base class for expression nodes
 */
public abstract class Expression extends IR
{
	private static final String[] childrenNames = { "type" };

	/** The type of the expression. */
	protected Type type;

	public Expression(String name, Type type) {
		super(name);
		setChildrenNames(childrenNames);
		this.type = type;
	}

	/** @return The type of the expression. */
	public Type getType() {
		return type;
	}

	/**
	 * Method collectElementsAndVars extracts the nodes, edges, and variables
	 * occurring in this Expression.
	 * @param needs A NeededEntities instance aggregating the needed elements.
	 */
	public abstract void collectNeededEntities(NeededEntities needs);
}
