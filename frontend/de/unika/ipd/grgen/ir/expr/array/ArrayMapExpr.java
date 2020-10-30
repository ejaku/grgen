/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.expr.array;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.type.container.ArrayType;

public class ArrayMapExpr extends ArrayFunctionMethodInvocationBaseExpr
{
	private Variable elementVar;
	private Expression mappingExpr;

	public ArrayMapExpr(Expression targetExpr, Variable elementVar, Expression mappingExpr, ArrayType resultingType)
	{
		super("array map expr", resultingType, targetExpr);
		this.elementVar = elementVar;
		this.mappingExpr = mappingExpr;
	}

	public Variable getElementVar()
	{
		return elementVar;
	}

	public Expression getMappingExpr()
	{
		return mappingExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		super.collectNeededEntities(needs);
		needs.add(this);
		mappingExpr.collectNeededEntities(needs);
		if(needs.variables != null)
			needs.variables.remove(elementVar);
	}
}
