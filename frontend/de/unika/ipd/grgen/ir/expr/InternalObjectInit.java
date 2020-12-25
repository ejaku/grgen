/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.type.InternalObjectType;

public class InternalObjectInit extends Expression
{
	private InternalObjectType objectType;

	public Vector<AttributeInitialization> attributeInitializations = new Vector<AttributeInitialization>();

	public InternalObjectInit(InternalObjectType objectType)
	{
		super("internal object init", objectType);
		this.objectType = objectType;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		for(Expression attributeInitializationExpression : getAttributeInitializationExpressions()) {
			attributeInitializationExpression.collectNeededEntities(needs);
		}
	}

	public void addAttributeInitialization(AttributeInitialization ai)
	{
		this.attributeInitializations.add(ai);
	}

	public Collection<Expression> getAttributeInitializationExpressions()
	{
		Vector<Expression> expressions = new Vector<Expression>();
		for(AttributeInitialization attributeInitialization : attributeInitializations) {
			expressions.add(attributeInitialization.expr);
		}
		return expressions;
	}

	public InternalObjectType getInternalObjectType()
	{
		return objectType;
	}
	
	public String getAnonymousInternalObjectInitName()
	{
		return "internal_object_init_" + getId();
	}
}
