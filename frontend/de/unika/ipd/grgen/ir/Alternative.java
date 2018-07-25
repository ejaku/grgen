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

import java.util.Collection;
import java.util.Vector;


/**
 * Represents an alternative statement in the IR.
 */
public class Alternative extends Identifiable {
	public Alternative(Ident ident) {
		super("alternative", ident);
	}

	Vector<Rule> alternativeCases = new Vector<Rule>();

	/** Was the replacement code already called by means of an alternative replacement declaration? */
	public boolean wasReplacementAlreadyCalled;


	public Collection<Rule> getAlternativeCases() {
		return alternativeCases;
	}

	public void addAlternativeCase(Rule alternativeCaseRule)
	{
		alternativeCases.add(alternativeCaseRule);
	}

	public String getNameOfGraph() {
		return getIdent().toString();
	}
}
