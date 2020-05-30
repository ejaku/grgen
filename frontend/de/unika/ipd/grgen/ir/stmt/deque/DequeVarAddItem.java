/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.deque;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.ContainerVarProcedureMethodInvocationBase;

public class DequeVarAddItem extends ContainerVarProcedureMethodInvocationBase
{
	Expression valueExpr;
	Expression indexExpr;

	public DequeVarAddItem(Variable target, Expression valueExpr, Expression indexExpr)
	{
		super("deque var add item", target);
		this.valueExpr = valueExpr;
		this.indexExpr = indexExpr;
	}

	public Expression getValueExpr()
	{
		return valueExpr;
	}

	public Expression getIndexExpr()
	{
		return indexExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(target))
			needs.add(target);

		valueExpr.collectNeededEntities(needs);

		if(indexExpr != null)
			indexExpr.collectNeededEntities(needs);

		if(getNext() != null) {
			getNext().collectNeededEntities(needs);
		}
	}
}
