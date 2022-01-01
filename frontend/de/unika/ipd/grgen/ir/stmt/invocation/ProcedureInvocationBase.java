/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.invocation;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Vector;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.executable.ProcedureBase;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * A base class for procedure or builtin procedure invocations.
 */
public abstract class ProcedureInvocationBase extends ProcedureOrBuiltinProcedureInvocationBase
{
	/** The arguments of the procedure invocation. */
	protected List<Expression> arguments = new ArrayList<Expression>();

	/** The return types of the procedure invocation. */
	protected Vector<Type> returnTypes = new Vector<Type>();

	protected ProcedureInvocationBase(String name)
	{
		super(name);
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

	@Override
	public int returnArity()
	{
		return returnTypes.size();
	}

	@Override
	public Type getReturnType(int index)
	{
		return index >= 0 || index < returnTypes.size() ? returnTypes.get(index) : null;
	}

	/** Adds a return type t to the procedure. */
	public void addReturnType(Type t)
	{
		returnTypes.add(t);
	}
	
	public Collection<Expression> getWalkableChildren()
	{
		return arguments;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		for(Expression child : getWalkableChildren()) {
			child.collectNeededEntities(needs);
		}
	}

	public abstract ProcedureBase getProcedureBase();
}
