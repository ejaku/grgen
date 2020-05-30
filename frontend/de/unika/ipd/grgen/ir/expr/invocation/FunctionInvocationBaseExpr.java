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

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * Base class for real function calls, i.e. calls of user-defined functions.
 */
public class FunctionInvocationBaseExpr extends FunctionOrBuiltinFunctionInvocationExpr
{
	/** The arguments of the function invocation expression. */
	protected List<Expression> arguments = new ArrayList<Expression>();

	public FunctionInvocationBaseExpr(String name, Type type)
	{
		super(name, type);
	}

	/** @return The number of arguments. */
	public int arity()
	{
		return arguments.size();
	}

	/**
	 * Get the ith argument.
	 * @param index The index of the argument
	 * @return The argument, if <code>index</code> was valid, <code>null</code> if not.
	 */
	public Expression getArgument(int index)
	{
		return index >= 0 || index < arguments.size() ? arguments.get(index) : null;
	}

	/** Adds an argument e to the expression. */
	public void addArgument(Expression e)
	{
		arguments.add(e);
	}

	public Collection<Expression> getWalkableChildren()
	{
		return arguments;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
		for(Expression child : getWalkableChildren()) {
			child.collectNeededEntities(needs);
		}
	}
}
