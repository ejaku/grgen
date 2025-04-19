/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;

/**
 * Represents a for over an ordered index access in the IR.
 * deprecated, TODO: purge
 */
public class ForIndexAccessOrdering extends EvalStatement
{
	private Variable iterationVar;
	private IndexAccessOrdering iao;
	private Collection<EvalStatement> statements = new LinkedList<EvalStatement>();

	public ForIndexAccessOrdering(Variable iterationVar, IndexAccessOrdering iao)
	{
		super("for index access ordering");
		this.iterationVar = iterationVar;
		this.iao = iao;
	}

	public void addLoopedStatement(EvalStatement loopedStatement)
	{
		statements.add(loopedStatement);
	}

	public Variable getIterationVar()
	{
		return iterationVar;
	}

	public IndexAccessOrdering getIndexAccessOrdering()
	{
		return iao;
	}

	public Collection<EvalStatement> getLoopedStatements()
	{
		return statements;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		iao.collectNeededEntities(needs);
		for(EvalStatement loopedStatement : statements) {
			loopedStatement.collectNeededEntities(needs);
		}
		if(needs.variables != null)
			needs.variables.remove(iterationVar);
	}
}
