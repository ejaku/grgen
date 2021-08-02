/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.util.report;

/**
 * An empty location.
 */
public class EmptyLocation implements Location
{
	private static final EmptyLocation EMPTY;

	static {
		EMPTY = new EmptyLocation();
	}

	/**
	 * Return the empty string always.
	 * @see de.unika.ipd.grgen.util.report.Location#getLocation()
	 */
	@Override
	public String getLocation()
	{
		return "<nowhere>";
	}

	/**
	 * This location is never valid.
	 * @see de.unika.ipd.grgen.util.report.Location#hasLocation()
	 */
	@Override
	public boolean hasLocation()
	{
		return false;
	}

	/**
	 * Get a new empty location
	 * @return an empty location
	 */
	public static EmptyLocation getEmptyLocation()
	{
		return EMPTY;
	}
}
