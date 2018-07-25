/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

public class AlternativeReplacement extends Identifiable implements OrderedReplacement{
	Alternative alternative;

	public AlternativeReplacement(String name, Ident ident,
			Alternative alternative) {
		super(name, ident);
		this.alternative = alternative;
	}

	public Alternative getAlternative() {
		return alternative;
	}
}
