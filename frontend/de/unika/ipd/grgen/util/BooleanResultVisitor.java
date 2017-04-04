/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
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
