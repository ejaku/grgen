/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.array;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.ContainerVarProcedureMethodInvocationBase;

public class ArrayVarAddAll extends ContainerVarProcedureMethodInvocationBase
{
	Expression valueExpr;

	public ArrayVarAddAll(Variable target, Expression valueExpr)
	{
		super("array var add item", target);
		this.valueExpr = valueExpr;
	}

	public Expression getValueExpr()
	{
		return valueExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		super.collectNeededEntities(needs);

		valueExpr.collectNeededEntities(needs);
	}
}