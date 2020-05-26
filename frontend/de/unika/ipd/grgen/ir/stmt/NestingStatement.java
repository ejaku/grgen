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

import java.util.Collection;
import java.util.LinkedList;

/**
 * Represents a nesting statement in the IR.
 */
public abstract class NestingStatement extends EvalStatement
{
	protected Collection<EvalStatement> statements = new LinkedList<EvalStatement>();

	protected NestingStatement(String name)
	{
		super(name);
	}

	public void addStatement(EvalStatement loopedStatement)
	{
		statements.add(loopedStatement);
	}

	public Collection<EvalStatement> getStatements()
	{
		return statements;
	}
}
