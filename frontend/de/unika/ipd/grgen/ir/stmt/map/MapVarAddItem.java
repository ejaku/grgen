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
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.ContainerVarProcedureMethodInvocationBase;

public class MapVarAddItem extends ContainerVarProcedureMethodInvocationBase
{
	Expression keyExpr;
	Expression valueExpr;

	public MapVarAddItem(Variable target, Expression keyExpr, Expression valueExpr)
	{
		super("map var add item", target);
		this.keyExpr = keyExpr;
		this.valueExpr = valueExpr;
	}

	public Expression getKeyExpr()
	{
		return keyExpr;
	}

	public Expression getValueExpr()
	{
		return valueExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(target))
			needs.add(target);

		keyExpr.collectNeededEntities(needs);
		valueExpr.collectNeededEntities(needs);

		if(getNext() != null) {
			getNext().collectNeededEntities(needs);
		}
	}
}
