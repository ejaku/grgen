/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 */
package de.unika.ipd.grgen.ir.stmt;

import java.util.HashSet;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;
import de.unika.ipd.grgen.ir.type.MatchType;

/**
 * Represents an assignment statement in the IR.
 */
public class Assignment extends AssignmentBase
{
	/** The lhs of the assignment. */
	private Qualification target;

	public Assignment(Qualification target, Expression expr)
	{
		super("assignment");
		this.target = target;
		this.expr = expr;
	}

	protected Assignment(String name, Qualification target, Expression expr)
	{
		super(name);
		this.target = target;
		this.expr = expr;
	}

	public Qualification getTarget()
	{
		return target;
	}

	@Override
	public String toString()
	{
		return getTarget() + " = " + getExpression();
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		Entity entity = target.getOwner();
		if(!isGlobalVariable(entity)
				&& !(entity.getType() instanceof MatchType)
				&& !(entity.getType() instanceof DefinedMatchType)) {
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

		getExpression().collectNeededEntities(needs);
	}
}
