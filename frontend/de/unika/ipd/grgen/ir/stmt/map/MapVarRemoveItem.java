/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

public class MapVarRemoveItem extends ContainerVarProcedureMethodInvocationBase
{
	Expression keyExpr;

	public MapVarRemoveItem(Variable target, Expression keyExpr)
	{
		super("map var remove item", target);
		this.keyExpr = keyExpr;
	}

	public Expression getKeyExpr()
	{
		return keyExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		super.collectNeededEntities(needs);
		
		keyExpr.collectNeededEntities(needs);
	}
}
