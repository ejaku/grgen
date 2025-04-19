/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.pattern.Variable;

/**
 * Represents an accumulation yielding of a matches variable in the IR.
 */
public class MatchesAccumulationYield extends BlockNestingStatement
{
	private Variable iterationVar;
	private Variable matchesVar;

	public MatchesAccumulationYield(Variable iterationVar, Variable matchesVar)
	{
		super("matches accumulation yield");
		this.iterationVar = iterationVar;
		this.matchesVar = matchesVar;
	}

	public Variable getIterationVar()
	{
		return iterationVar;
	}

	public Variable getMatchesVar()
	{
		return matchesVar;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(matchesVar))
			needs.add(matchesVar);
		for(EvalStatement accumulationStatement : statements) {
			accumulationStatement.collectNeededEntities(needs);
		}
		if(needs.variables != null)
			needs.variables.remove(iterationVar);
	}
}
