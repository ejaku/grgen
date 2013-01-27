/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

// fakes class which represents a match access and behaves like a nop
// the match access handling in the frontend is reduced to syntax checking, which is basically a nop
// all further work is done in the sequence generation and esp. checking of the backend
// doing correct resolving and type checking is sth I think would be a lot of effort
// which is simply not needed for a construct which can only appear in the sequences, not requiring any presets
public class MatchAccess extends Expression {
	public MatchAccess(UntypedExecVarType type) {
		super("match access", type);
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
	}
}

