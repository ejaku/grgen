/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.map;

import java.util.HashSet;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.executable.ProcedureBase;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.ProcedureInvocationBase;

public class MapRemoveItem extends ProcedureInvocationBase
{
	Qualification target;
	Expression keyExpr;

	public MapRemoveItem(Qualification target, Expression keyExpr)
	{
		super("map remove item");
		this.target = target;
		this.keyExpr = keyExpr;
	}

	public Qualification getTarget()
	{
		return target;
	}

	public Expression getKeyExpr()
	{
		return keyExpr;
	}

	public ProcedureBase getProcedureBase()
	{
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure method
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		Entity entity = target.getOwner();
		if(!isGlobalVariable(entity))
			needs.add((GraphEntity)entity);

		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.collectNeededEntities(needs);
		needs.variables = varSet;

		getKeyExpr().collectNeededEntities(needs);

		if(getNext() != null) {
			getNext().collectNeededEntities(needs);
		}
	}
}