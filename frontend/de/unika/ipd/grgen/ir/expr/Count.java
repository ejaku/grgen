/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr;

import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.type.Type;

public class Count extends Expression
{
	private Rule iterated;

	public Count(Rule iterated, Type type)
	{
		super("count", type);
		this.iterated = iterated;
	}

	public Rule getIterated()
	{
		return iterated;
	}
}
