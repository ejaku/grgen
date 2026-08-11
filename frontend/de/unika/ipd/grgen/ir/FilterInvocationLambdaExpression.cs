/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir
{
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using Type = de.unika.ipd.grgen.ir.type.Type;

public class FilterInvocationLambdaExpression : FilterInvocationBase
{
	internal string plainName;
	internal string entity;
	internal Type entityType;

	internal Variable initArrayAccessVar;
	internal Expression initExpr;

	internal Variable arrayAccessVar;
	internal Variable previousAccumulationAccessVar;
	internal Variable indexVar;
	internal Variable elementVar;
	internal Expression lambdaExpr;

	public FilterInvocationLambdaExpression(string name, Ident ident, string plainName, string entity, Type entityType, Rule iteratedAction,
			Variable initArrayAccessVar, Expression initExpr,
			Variable arrayAccessVar, Variable previousAccumulationAccessVar,
			Variable indexVar, Variable elementVar, Expression lambdaExpr)
		: base(name, ident, iteratedAction)
	{
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

	public virtual Variable InitArrayAccessVariable
	{
		get
		{
		return initArrayAccessVar;
		}
	}

	public virtual Expression InitExpression
	{
		get
		{
		return initExpr;
		}
	}

	public virtual Variable ArrayAccessVariable
	{
		get
		{
		return arrayAccessVar;
		}
	}

	public virtual Variable PreviousAccumulationAccessVariable
	{
		get
		{
		return previousAccumulationAccessVar;
		}
	}

	public virtual Variable IndexVariable
	{
		get
		{
		return indexVar;
		}
	}

	public virtual Variable ElementVariable
	{
		get
		{
		return elementVar;
		}
	}

	public virtual Expression LambdaExpression
	{
		get
		{
		return lambdaExpr;
		}
	}

	public virtual string FilterName
	{
		get
		{
		return plainName;
		}
	}

	public virtual string FilterEntity
	{
		get
		{
		return entity;
		}
	}

	public virtual Type FilterEntityType
	{
		get
		{
		return entityType;
		}
	}

	public virtual void CollectNeededEntities(NeededEntities needs)
	{
		if(initExpr != null)
			initExpr.CollectNeededEntities(needs);
		lambdaExpr.CollectNeededEntities(needs);
		if(needs.variables != null)
		{
			if(initArrayAccessVar != null)
				needs.variables.Remove(initArrayAccessVar);
			if(arrayAccessVar != null)
				needs.variables.Remove(arrayAccessVar);
			if(previousAccumulationAccessVar != null)
				needs.variables.Remove(previousAccumulationAccessVar);
			if(indexVar != null)
				needs.variables.Remove(indexVar);
			needs.variables.Remove(elementVar);
		}
	}
}

}
