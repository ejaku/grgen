/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.pattern;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.executable.Rule;

public class IteratedReplacement extends Identifiable implements OrderedReplacement
{
	Rule iterated;

	public IteratedReplacement(String name, Ident ident,
			Rule iterated)
	{
		super(name, ident);
		this.iterated = iterated;
	}

	public Rule getIterated()
	{
		return iterated;
	}
}
