/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.map;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.stmt.ContainerQualProcedureMethodInvocationBase;

public class MapAddItem extends ContainerQualProcedureMethodInvocationBase
{
	Expression keyExpr;
	Expression valueExpr;

	public MapAddItem(Qualification target, Expression keyExpr, Expression valueExpr)
	{
		super("map add item", target);
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
		super.collectNeededEntities(needs);
		
		keyExpr.collectNeededEntities(needs);
		valueExpr.collectNeededEntities(needs);
	}
}
