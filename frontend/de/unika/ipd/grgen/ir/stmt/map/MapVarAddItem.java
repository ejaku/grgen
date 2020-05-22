/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.map;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.executable.ProcedureBase;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.ProcedureInvocationBase;

public class MapVarAddItem extends ProcedureInvocationBase
{
	Variable target;
	Expression keyExpr;
	Expression valueExpr;

	public MapVarAddItem(Variable target, Expression keyExpr, Expression valueExpr)
	{
		super("map var add item");
		this.target = target;
		this.keyExpr = keyExpr;
		this.valueExpr = valueExpr;
	}

	public Variable getTarget()
	{
		return target;
	}

	public Expression getKeyExpr()
	{
		return keyExpr;
	}

	public Expression getValueExpr()
	{
		return valueExpr;
	}

	public ProcedureBase getProcedureBase()
	{
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure method
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(target))
			needs.add(target);

		getKeyExpr().collectNeededEntities(needs);
		getValueExpr().collectNeededEntities(needs);

		if(getNext() != null) {
			getNext().collectNeededEntities(needs);
		}
	}
}
