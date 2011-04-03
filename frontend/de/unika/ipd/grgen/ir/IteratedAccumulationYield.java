/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;


/**
 * Represents an accumulation yielding of an iterated match def variable in the IR.
 */
public class IteratedAccumulationYield extends EvalStatement {

	private Variable accumulationVar;
	private Rule iterated;
	private String accumulationOp;

	public IteratedAccumulationYield(Variable accumulationVar, Rule iterated, String accumulationOp) {
		super("iterated accumulation yield");
		this.accumulationVar = accumulationVar;
		this.iterated = iterated;
		this.accumulationOp = accumulationOp;
	}

	public Variable getAccumulationVar() {
		return accumulationVar;
	}

	public Rule getIterated() {
		return iterated;
	}
	
	public String getAccumulationOp() {
		return accumulationOp;
	}
	
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(accumulationVar);
	}
}
