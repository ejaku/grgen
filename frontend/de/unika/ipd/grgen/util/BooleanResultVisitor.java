/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * A visitor that returns a boolean value.
 * They are occurring rather often, so they're an own class.
 */
public abstract class BooleanResultVisitor implements ResultVisitor<Boolean>
{
	private boolean result;

	/**
	 * Make a new one.
	 * @param def The value, the result is initialized.
	 */
	public BooleanResultVisitor(boolean init)
	{
		result = init;
	}

	protected void setResult(boolean value)
	{
		result = value;
	}

	/**
	 * @see de.unika.ipd.grgen.util.ResultVisitor#getResult()
	 */
	public Boolean getResult()
	{
		return new Boolean(result);
	}

	public boolean booleanResult()
	{
		return result;
	}
}
