/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ir.expr.array
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;

public class ArrayMapExpr : ArrayFunctionMethodInvocationBaseExpr, ArrayPerElementMethod
{
	private Variable arrayAccessVar;

	private Variable indexVar;
	private Variable elementVar;
	private Expression mappingExpr;

	public ArrayMapExpr(Expression targetExpr, Variable arrayAccessVar, Variable indexVar, Variable elementVar, Expression mappingExpr, ArrayType resultingType)
		: base("array map expr", resultingType, targetExpr)
	{
		this.arrayAccessVar = arrayAccessVar;
		this.indexVar = indexVar;
		this.elementVar = elementVar;
		this.mappingExpr = mappingExpr;
	}

	public virtual Variable ArrayAccessVar
	{
		get
		{
			return arrayAccessVar;
		}
	}

	public virtual Variable IndexVar
	{
		get
		{
			return indexVar;
		}
	}

	public virtual Variable ElementVar
	{
		get
		{
			return elementVar;
		}
	}

	public virtual Expression MappingExpr
	{
		get
		{
			return mappingExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		base.CollectNeededEntities(needs);
		needs.Add(this);
		mappingExpr.CollectNeededEntities(needs);
		if(needs.variables != null)
		{
			if(arrayAccessVar != null)
				needs.variables.Remove(arrayAccessVar);
			if(indexVar != null)
				needs.variables.Remove(indexVar);
			needs.variables.Remove(elementVar);
		}
	}
}

}
