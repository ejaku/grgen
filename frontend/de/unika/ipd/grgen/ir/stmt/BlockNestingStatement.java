/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import java.util.Collection;
import java.util.LinkedList;

import de.unika.ipd.grgen.ir.NestingStatement;

/**
 * Represents a block nesting statement in the IR (non top-level statement containing nested statements).
 */
public abstract class BlockNestingStatement extends EvalStatement implements NestingStatement
{
	protected Collection<EvalStatement> statements = new LinkedList<EvalStatement>();

	protected BlockNestingStatement(String name)
	{
		super(name);
	}

	@Override
	public void addStatement(EvalStatement loopedStatement)
	{
		statements.add(loopedStatement);
	}

	@Override
	public Collection<EvalStatement> getStatements()
	{
		return statements;
	}
}
