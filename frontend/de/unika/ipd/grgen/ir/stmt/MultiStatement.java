/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import java.util.Collection;
import java.util.LinkedList;

import de.unika.ipd.grgen.ir.NeededEntities;

/**
 * Represents a multi statement in the IR.
 */
public class MultiStatement extends EvalStatement
{
	private Collection<EvalStatement> statements = new LinkedList<EvalStatement>();

	public MultiStatement()
	{
		super("multi statement");
	}

	public void addStatement(EvalStatement loopedStatement)
	{
		statements.add(loopedStatement);
	}

	public Collection<EvalStatement> getStatements()
	{
		return statements;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		for(EvalStatement statement : statements) {
			statement.collectNeededEntities(needs);
		}
	}
}
