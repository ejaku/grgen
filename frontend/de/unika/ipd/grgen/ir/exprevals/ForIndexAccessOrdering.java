/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
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
 * Represents a for over an ordered index access in the IR.
 */
public class ForIndexAccessOrdering extends EvalStatement {

	private Variable iterationVar;
	private IndexAccessOrdering iao;
	private Collection<EvalStatement> loopedStatements = new LinkedList<EvalStatement>();

	public ForIndexAccessOrdering(Variable iterationVar, IndexAccessOrdering iao) {
		super("for index access ordering");
		this.iterationVar = iterationVar;
		this.iao = iao;
	}

	public void addLoopedStatement(EvalStatement loopedStatement) {
		loopedStatements.add(loopedStatement);
	}

	public Variable getIterationVar() {
		return iterationVar;
	}

	public IndexAccessOrdering getIndexAccessOrdering() {
		return iao;
	}

	public Collection<EvalStatement> getLoopedStatements() {
		return loopedStatements;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		iao.collectNeededEntities(needs);
		for(EvalStatement loopedStatement : loopedStatements)
			loopedStatement.collectNeededEntities(needs);
		if(needs.variables != null)
			needs.variables.remove(iterationVar);
	}
}
