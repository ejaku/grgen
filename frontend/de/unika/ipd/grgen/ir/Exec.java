/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Rubino Geiss
 */

package de.unika.ipd.grgen.ir;

import java.util.Collections;
import java.util.EnumSet;
import java.util.LinkedHashSet;
import java.util.Set;

import de.unika.ipd.grgen.ir.NeededEntities.Needs;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.ImperativeStmt;

/**
 * A XGRS in an exec statement.
 */
public class Exec extends IR implements ImperativeStmt
{
	private Set<Expression> parameters = new LinkedHashSet<Expression>();
	private Set<Entity> neededEntities;
	private Set<Entity> neededEntitiesForComputation;
	private String xgrsString;
	private int lineNr;

	public Exec(String xgrsString, Set<Expression> parameters, int lineNr)
	{
		super("exec");
		this.xgrsString = xgrsString;
		this.parameters = parameters;
		this.lineNr = lineNr;
	}

	/** Returns XGRS as an String */
	public String getXGRSString()
	{
		return xgrsString;
	}

	public int getLineNr()
	{
		return lineNr;
	}

	/** Returns Parameters */
	public Set<Expression> getArguments()
	{
		return Collections.unmodifiableSet(parameters);
	}

	public Set<Entity> getNeededEntities(boolean forComputation)
	{
		if(forComputation) {
			if(neededEntitiesForComputation == null) {
				NeededEntities needs = new NeededEntities(EnumSet.of(Needs.ALL_ENTITIES, Needs.COMPUTATION_CONTEXT));
				for(Expression param : getArguments()) {
					param.collectNeededEntities(needs);
				}
				neededEntitiesForComputation = needs.entities;
			}
			return neededEntitiesForComputation;
		} else {
			if(neededEntities == null) {
				NeededEntities needs = new NeededEntities(EnumSet.of(Needs.ALL_ENTITIES));
				for(Expression param : getArguments()) {
					param.collectNeededEntities(needs);
				}
				neededEntities = needs.entities;
			}
			return neededEntities;
		}
	}
}
