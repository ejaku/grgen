/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.invocation;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.executable.Function;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * A function method invocation is an expression.
 */
public class FunctionMethodInvocationExpr extends FunctionInvocationBaseExpr
{
	/** The owner of the function method. */
	private Entity owner;

	/** The function of the function method invocation expression. */
	protected Function function;

	public FunctionMethodInvocationExpr(Entity owner, Type type, Function function)
	{
		super("function method invocation expr", type);

		this.owner = owner;
		this.function = function;
	}

	public Entity getOwner()
	{
		return owner;
	}

	public Function getFunction()
	{
		return function;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(owner))
			needs.add((GraphEntity)owner);
		for(Expression child : getWalkableChildren()) {
			child.collectNeededEntities(needs);
		}
	}
}
