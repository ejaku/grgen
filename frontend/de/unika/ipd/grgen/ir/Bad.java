/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
	public Bad()
	{
		super("bad");
	}

	@Override
	public boolean isBad()
	{
		return true;
	}
}
