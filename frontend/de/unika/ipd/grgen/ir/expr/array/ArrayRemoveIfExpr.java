/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

public class ArrayRemoveIfExpr extends ArrayFunctionMethodInvocationBaseExpr implements ArrayPerElementMethod
{
	private Variable arrayAccessVar;
	
	private Variable indexVar;
	private Variable elementVar;
	private Expression conditionExpr;

	public ArrayRemoveIfExpr(Expression targetExpr, Variable arrayAccessVar,
			Variable indexVar, Variable elementVar,
			Expression conditionExpr, ArrayType resultingType)
	{
		super("array remove if expr", resultingType, targetExpr);
		this.arrayAccessVar = arrayAccessVar;
		this.indexVar = indexVar;
		this.elementVar = elementVar;
		this.conditionExpr = conditionExpr;
	}

	public Variable getArrayAccessVar()
	{
		return arrayAccessVar;
	}

	public Variable getIndexVar()
	{
		return indexVar;
	}

	@Override
	public Variable getElementVar()
	{
		return elementVar;
	}

	public Expression getConditionExpr()
	{
		return conditionExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		super.collectNeededEntities(needs);
		needs.add(this);
		conditionExpr.collectNeededEntities(needs);
		if(needs.variables != null)
		{
			if(arrayAccessVar != null)
				needs.variables.remove(arrayAccessVar);
			if(indexVar != null)
				needs.variables.remove(indexVar);
			needs.variables.remove(elementVar);
		}
	}
}
