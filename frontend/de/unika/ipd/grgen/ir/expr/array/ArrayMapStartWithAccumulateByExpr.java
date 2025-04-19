/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

public class ArrayMapStartWithAccumulateByExpr extends ArrayFunctionMethodInvocationBaseExpr implements ArrayPerElementMethod
{
	private Variable initArrayAccessVar;
	private Expression initExpr;

	private Variable arrayAccessVar;
	private Variable previousAccumulationAccessVar;
	private Variable indexVar;
	private Variable elementVar;
	private Expression mappingExpr;

	public ArrayMapStartWithAccumulateByExpr(Expression targetExpr,
			Variable initArrayAccessVar, Expression initExpr,
			Variable arrayAccessVar, Variable previousAccumulationAccessVar,
			Variable indexVar, Variable elementVar,
			Expression mappingExpr, ArrayType resultingType)
	{
		super("array map start with accumulate by expr", resultingType, targetExpr);
		this.initArrayAccessVar = initArrayAccessVar;
		this.initExpr = initExpr;
		this.arrayAccessVar = arrayAccessVar;
		this.previousAccumulationAccessVar = previousAccumulationAccessVar;
		this.indexVar = indexVar;
		this.elementVar = elementVar;
		this.mappingExpr = mappingExpr;
	}

	public Variable getInitArrayAccessVar()
	{
		return initArrayAccessVar;
	}

	public Expression getInitExpr()
	{
		return initExpr;
	}

	public Variable getArrayAccessVar()
	{
		return arrayAccessVar;
	}

	public Variable getPreviousAccumulationAccessVar()
	{
		return previousAccumulationAccessVar;
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

	public Expression getMappingExpr()
	{
		return mappingExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		super.collectNeededEntities(needs);
		needs.add(this);
		initExpr.collectNeededEntities(needs);
		mappingExpr.collectNeededEntities(needs);
		if(needs.variables != null) {
			if(initArrayAccessVar != null)
				needs.variables.remove(initArrayAccessVar);
			if(arrayAccessVar != null)
				needs.variables.remove(arrayAccessVar);
			needs.variables.remove(previousAccumulationAccessVar);
			if(indexVar != null)
				needs.variables.remove(indexVar);
			needs.variables.remove(elementVar);
		}
	}
}
