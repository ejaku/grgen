/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.pattern.Variable;

/**
 * Represents an accumulation yielding of an iterated match def variable in the IR.
 */
public class IteratedAccumulationYield extends NestingStatement
{
	private Variable iterationVar;
	private Rule iterated;

	public IteratedAccumulationYield(Variable accumulationVar, Rule iterated)
	{
		super("iterated accumulation yield");
		this.iterationVar = accumulationVar;
		this.iterated = iterated;
	}

	public Variable getIterationVar()
	{
		return iterationVar;
	}

	public Rule getIterated()
	{
		return iterated;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		for(EvalStatement accumulationStatement : statements) {
			accumulationStatement.collectNeededEntities(needs);
		}
		if(needs.variables != null)
			needs.variables.remove(iterationVar);
	}
}
