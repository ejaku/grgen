/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.type;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.executable.Rule;

public class MatchTypeIterated extends MatchType
{
	private Rule iterated;

	public MatchTypeIterated(Ident iteratedIdent)
	{
		super(iteratedIdent);
	}

	public void setIterated(Rule iterated)
	{
		this.iterated = iterated;
	}

	public Rule getIterated()
	{
		return iterated;
	}
}
