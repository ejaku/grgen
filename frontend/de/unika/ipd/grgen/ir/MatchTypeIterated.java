/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

public class MatchTypeIterated extends MatchType implements ContainedInPackage {
	private Rule iterated;

	public MatchTypeIterated(Rule action, Rule iterated) {
		super(action, iterated.getIdent());
		this.iterated = iterated;
	}

	public Rule getIterated() {
		return iterated;
	}
}
