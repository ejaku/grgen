/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.graph;

import java.util.Collection;
import java.util.LinkedList;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;

/**
 * Represents a for index access for a certain value in the IR.
 */
public class ForIndexAccessEquality extends EvalStatement
{
	private Variable iterationVar;
	private IndexAccessEquality iae;
	private Collection<EvalStatement> statements = new LinkedList<EvalStatement>();

	public ForIndexAccessEquality(Variable iterationVar,
			IndexAccessEquality iae)
	{
		super("for index access equality");
		this.iterationVar = iterationVar;
		this.iae = iae;
	}

	public void addLoopedStatement(EvalStatement loopedStatement)
	{
		statements.add(loopedStatement);
	}

	public Variable getIterationVar()
	{
		return iterationVar;
	}

	public IndexAccessEquality getIndexAcccessEquality()
	{
		return iae;
	}

	public Collection<EvalStatement> getLoopedStatements()
	{
		return statements;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		iae.collectNeededEntities(needs);
		for(EvalStatement loopedStatement : statements) {
			loopedStatement.collectNeededEntities(needs);
		}
		if(needs.variables != null)
			needs.variables.remove(iterationVar);
	}
}
