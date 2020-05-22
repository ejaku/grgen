/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir.executable;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Identifiable;

/**
 * A graph action.
 */
public abstract class Action extends Identifiable
{
	public Action(String name, Ident ident)
	{
		super(name, ident);
	}
}
