/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import java.util.Collection;
import java.util.LinkedList;

import de.unika.ipd.grgen.ir.*;

/**
 * Represents an accumulation yielding of a matches variable in the IR.
 */
public class MatchesAccumulationYield extends EvalStatement {

	private Variable iterationVar;
	private Variable matchesVar;
	private Collection<EvalStatement> accumulationStatements = new LinkedList<EvalStatement>();

	public MatchesAccumulationYield(Variable iterationVar, Variable matchesVar) {
		super("matches accumulation yield");
		this.iterationVar = iterationVar;
		this.matchesVar = matchesVar;
	}

	public void addAccumulationStatement(EvalStatement accumulationStatement) {
		accumulationStatements.add(accumulationStatement);
	}

	public Variable getIterationVar() {
		return iterationVar;
	}

	public Variable getMatches() {
		return matchesVar;
	}

	public Collection<EvalStatement> getAccumulationStatements() {
		return accumulationStatements;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(matchesVar))
			needs.add(matchesVar);
		for(EvalStatement accumulationStatement : accumulationStatements)
			accumulationStatement.collectNeededEntities(needs);
		if(needs.variables != null)
			needs.variables.remove(iterationVar);
	}
}
