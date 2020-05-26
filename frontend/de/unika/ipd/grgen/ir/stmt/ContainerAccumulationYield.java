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
import de.unika.ipd.grgen.ir.pattern.Variable;

/**
 * Represents an accumulation yielding of a container variable in the IR.
 */
public class ContainerAccumulationYield extends NestingStatement
{
	private Variable iterationVar;
	private Variable indexVar;
	private Variable containerVar;

	public ContainerAccumulationYield(Variable iterationVar, Variable indexVar,
			Variable containerVar)
	{
		super("container accumulation yield");
		this.iterationVar = iterationVar;
		this.indexVar = indexVar;
		this.containerVar = containerVar;
	}

	public Variable getIterationVar()
	{
		return iterationVar;
	}

	public Variable getIndexVar()
	{
		return indexVar;
	}

	public Variable getContainer()
	{
		return containerVar;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(containerVar))
			needs.add(containerVar);
		for(EvalStatement accumulationStatement : statements) {
			accumulationStatement.collectNeededEntities(needs);
		}
		if(needs.variables != null)
			needs.variables.remove(iterationVar);
		if(indexVar != null)
			if(needs.variables != null)
				needs.variables.remove(indexVar);
	}
}
