/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.executable;

import de.unika.ipd.grgen.ir.Ident;

/**
 * An external filter function.
 */
public class FilterFunctionExternal extends FilterFunction
{
	public FilterFunctionExternal(String name, Ident ident)
	{
		super(name, ident);
	}
}
