/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ir;
using BaseInternalObjectType = de.unika.ipd.grgen.ir.model.type.BaseInternalObjectType;
using InternalObjectType = de.unika.ipd.grgen.ir.model.type.InternalObjectType;

public class InternalObjectInit : Expression
{
	private BaseInternalObjectType objectType;

	public IList<AttributeInitialization> attributeInitializations = new List<AttributeInitialization>();

	public InternalObjectInit(BaseInternalObjectType objectType)
		: base("internal object init", objectType)
	{
		this.objectType = objectType;
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.Add(this);
		if(objectType is InternalObjectType)
			needs.NeedsGraph();
		foreach(Expression attributeInitializationExpression in AttributeInitializationExpressions)
			attributeInitializationExpression.CollectNeededEntities(needs);
	}

	public virtual void AddAttributeInitialization(AttributeInitialization ai)
	{
		this.attributeInitializations.Add(ai);
	}

	public virtual ICollection<Expression> AttributeInitializationExpressions
	{
		get
		{
		IList<Expression> expressions = new List<Expression>();
		foreach(AttributeInitialization attributeInitialization in attributeInitializations)
			expressions.Add(attributeInitialization.expr);
		return expressions;
		}
	}

	public virtual BaseInternalObjectType BaseInternalObjectType
	{
		get
		{
		return objectType;
		}
	}

	public virtual string AnonymousInternalObjectInitName
	{
		get
		{
		return "internal_object_init_" + Id;
		}
	}
}

}
