/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.util;

public class MutableInteger
{
	int value;

	public MutableInteger (int v) {
		value = v;
	}

	public int getValue()
    {
    	return value;
    }

	public void setValue(int value)
    {
    	this.value = value;
    }
}
