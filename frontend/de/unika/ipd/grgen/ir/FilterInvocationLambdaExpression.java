/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.type.Type;

public class FilterInvocationLambdaExpression extends FilterInvocationBase
{
	String plainName;
	String entity;
	Type entityType;

	Variable initArrayAccessVar;
	Expression initExpr;
	
	Variable arrayAccessVar;
	Variable previousAccumulationAccessVar;
	Variable indexVar;
	Variable elementVar;
	Expression lambdaExpr;

	public FilterInvocationLambdaExpression(String name, Ident ident, String plainName, String entity, Type entityType, Rule iteratedAction, 
			Variable initArrayAccessVar, Expression initExpr,
			Variable arrayAccessVar, Variable previousAccumulationAccessVar,
			Variable indexVar, Variable elementVar, Expression lambdaExpr)
	{
		super(name, ident, iteratedAction);
		this.plainName = plainName;
		this.entity = entity;
		this.entityType = entityType;
		this.initArrayAccessVar = initArrayAccessVar;
		this.initExpr = initExpr;
		this.arrayAccessVar = arrayAccessVar;
		this.previousAccumulationAccessVar = previousAccumulationAccessVar;
		this.indexVar = indexVar;
		this.elementVar = elementVar;
		this.lambdaExpr = lambdaExpr;
	}

	public Variable getInitArrayAccessVariable()
	{
		return initArrayAccessVar;
	}

	public Expression getInitExpression()
	{
		return initExpr;
	}

	public Variable getArrayAccessVariable()
	{
		return arrayAccessVar;
	}

	public Variable getPreviousAccumulationAccessVariable()
	{
		return previousAccumulationAccessVar;
	}

	public Variable getIndexVariable()
	{
		return indexVar;
	}

	public Variable getElementVariable()
	{
		return elementVar;
	}

	public Expression getLambdaExpression()
	{
		return lambdaExpr;
	}
	
	public String getFilterName()
	{
		return plainName;
	}
	
	public String getFilterEntity()
	{
		return entity;
	}
	
	public Type getFilterEntityType()
	{
		return entityType;
	}
	
	public void collectNeededEntities(NeededEntities needs)
	{
		if(initExpr != null)
			initExpr.collectNeededEntities(needs);
		lambdaExpr.collectNeededEntities(needs);
		if(needs.variables != null) {
			if(initArrayAccessVar != null)
				needs.variables.remove(initArrayAccessVar);
			if(arrayAccessVar != null)
				needs.variables.remove(arrayAccessVar);
			if(previousAccumulationAccessVar != null)
				needs.variables.remove(previousAccumulationAccessVar);
			if(indexVar != null)
				needs.variables.remove(indexVar);
			needs.variables.remove(elementVar);
		}
	}
}
