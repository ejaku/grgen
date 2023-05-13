/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.model.type.BaseInternalObjectType;
import de.unika.ipd.grgen.ir.model.type.InternalObjectType;

public class InternalObjectInit extends Expression
{
	private BaseInternalObjectType objectType;

	public Vector<AttributeInitialization> attributeInitializations = new Vector<AttributeInitialization>();

	public InternalObjectInit(BaseInternalObjectType objectType)
	{
		super("internal object init", objectType);
		this.objectType = objectType;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		if(objectType instanceof InternalObjectType) {
			needs.needsGraph();
		}
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

	public BaseInternalObjectType getBaseInternalObjectType()
	{
		return objectType;
	}
	
	public String getAnonymousInternalObjectInitName()
	{
		return "internal_object_init_" + getId();
	}
}
