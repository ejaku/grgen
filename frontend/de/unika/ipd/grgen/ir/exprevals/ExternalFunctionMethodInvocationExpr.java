/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import de.unika.ipd.grgen.ir.*;

/**
 * An external function method invocation is an expression.
 */
public class ExternalFunctionMethodInvocationExpr extends Expression {
	/** The owner of the function method. */
	private Entity owner;

	/** The arguments of the function method invocation expression. */
	protected List<Expression> arguments = new ArrayList<Expression>();

	/** The function of the function method invocation expression. */
	protected ExternalFunction externalFunction;


	public ExternalFunctionMethodInvocationExpr(Entity owner, Type type, ExternalFunction externalFunction) {
		super("external function method invocation expr", type);

		this.owner = owner;
		this.externalFunction = externalFunction;
	}

	public Entity getOwner() {
		return owner;
	}

	/** @return The number of arguments. */
	public int arity() {
		return arguments.size();
	}

	public ExternalFunction getExternalFunc() {
		return externalFunction;
	}

	/**
	 * Get the ith argument.
	 * @param index The index of the argument
	 * @return The argument, if <code>index</code> was valid, <code>null</code> if not.
	 */
	public Expression getArgument(int index) {
		return index >= 0 || index < arguments.size() ? arguments.get(index) : null;
	}

	/** Adds an argument e to the expression. */
	public void addArgument(Expression e) {
		arguments.add(e);
	}

	public Collection<Expression> getWalkableChildren() {
		return arguments;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		if(!isGlobalVariable(owner))
			needs.add((GraphEntity) owner);
		for(Expression child : getWalkableChildren())
			child.collectNeededEntities(needs);
	}
}
