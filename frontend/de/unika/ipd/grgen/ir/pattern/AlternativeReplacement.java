/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.pattern;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Identifiable;

public class AlternativeReplacement extends Identifiable implements OrderedReplacement
{
	Alternative alternative;

	public AlternativeReplacement(String name, Ident ident,
			Alternative alternative)
	{
		super(name, ident);
		this.alternative = alternative;
	}

	public Alternative getAlternative()
	{
		return alternative;
	}
}
