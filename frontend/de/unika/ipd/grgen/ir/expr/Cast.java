/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author G. Veit Batz
 */

package de.unika.ipd.grgen.ir.expr;

import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.type.Type;

public class Cast extends Expression
{
	protected Expression expr;

	public Cast(Type type, Expression expr)
	{
		super("cast", type);
		this.expr = expr;
	}

	@Override
	public String getNodeLabel()
	{
		return "Cast to " + type;
	}

	public Expression getExpression()
	{
		return expr;
	}

	public Collection<Expression> getWalkableChildren()
	{
		List<Expression> vec = new ArrayList<Expression>();
		vec.add(expr);
		return vec;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		getExpression().collectNeededEntities(needs);
	}
}
