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

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.BlockNestingStatement;

/**
 * Represents a for lookup of a neighborhood function in the IR.
 */
public class ForFunction extends BlockNestingStatement
{
	private Variable iterationVar;
	private Expression function;

	public ForFunction(Variable iterationVar,
			Expression function)
	{
		super("for function");
		this.iterationVar = iterationVar;
		this.function = function;
	}

	public void addLoopedStatement(EvalStatement loopedStatement)
	{
		statements.add(loopedStatement);
	}

	public Variable getIterationVar()
	{
		return iterationVar;
	}

	public Expression getFunction()
	{
		return function;
	}

	public Collection<EvalStatement> getLoopedStatements()
	{
		return statements;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		function.collectNeededEntities(needs);
		for(EvalStatement loopedStatement : statements) {
			loopedStatement.collectNeededEntities(needs);
		}
		if(needs.variables != null)
			needs.variables.remove(iterationVar);
	}
}
