/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import java.util.HashSet;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Variable;

public abstract class ContainerQualProcedureMethodInvocationBase extends BuiltinProcedureInvocationBase
{
	protected Qualification target;

	protected ContainerQualProcedureMethodInvocationBase(String name, Qualification target)
	{
		super(name);
		this.target = target;
	}

	public Qualification getTarget()
	{
		return target;
	}
	
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		Entity entity = target.getOwner();
		if(!isGlobalVariable(entity)) {
			if(entity instanceof GraphEntity)
				needs.add((GraphEntity)entity);
			else
				needs.add((Variable)entity);
		}
		
		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.collectNeededEntities(needs);
		needs.variables = varSet;

		if(getNext() != null) {
			getNext().collectNeededEntities(needs);
		}
	}
}
