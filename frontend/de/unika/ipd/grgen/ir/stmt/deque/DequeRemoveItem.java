/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.deque;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.stmt.ContainerQualProcedureMethodInvocationBase;

public class DequeRemoveItem extends ContainerQualProcedureMethodInvocationBase
{
	Expression indexExpr;

	public DequeRemoveItem(Qualification target, Expression indexExpr)
	{
		super("deque remove item", target);
		this.indexExpr = indexExpr;
	}

	public Expression getIndexExpr()
	{
		return indexExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		super.collectNeededEntities(needs);
		
		if(indexExpr != null)
			indexExpr.collectNeededEntities(needs);
	}
}
