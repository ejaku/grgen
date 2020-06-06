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
import de.unika.ipd.grgen.ir.executable.ExternalFunction;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * An external function method invocation is an expression.
 */
public class ExternalFunctionMethodInvocationExpr extends FunctionInvocationBaseExpr
{
	/** The owner of the function method. */
	private Expression owner;

	/** The function of the function method invocation expression. */
	protected ExternalFunction externalFunction;

	public ExternalFunctionMethodInvocationExpr(Expression owner, Type type, ExternalFunction externalFunction)
	{
		super("external function method invocation expr", type);

		this.owner = owner;
		this.externalFunction = externalFunction;
	}

	public Expression getOwner()
	{
		return owner;
	}

	public ExternalFunction getExternalFunc()
	{
		return externalFunction;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		owner.collectNeededEntities(needs);
		for(Expression child : getWalkableChildren()) {
			child.collectNeededEntities(needs);
		}
	}
}
