/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir;

/**
 * A bad IR element.
 * This used in case of an error.
 */
public class Bad extends IR
{
	private static final IR bad = new Bad();

	private Bad()
	{
		super("bad");
	}

	/** @return A bad ir object. */
	public static IR getBadObject()
	{
		return bad;
	}

	@Override
	public boolean isBad()
	{
		return true;
	}
}
