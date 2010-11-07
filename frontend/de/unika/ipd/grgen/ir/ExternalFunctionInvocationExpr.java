/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

/**
 * An external function invocation is an expression.
 */
public class ExternalFunctionInvocationExpr extends Expression {
	/** The arguments of the function invocation expression. */
	protected List<Expression> arguments = new ArrayList<Expression>();

	/** The function of the function invocation expression. */
	protected ExternalFunction externalFunction;

	
	public ExternalFunctionInvocationExpr(Type type, ExternalFunction externalFunction) {
		super("external function invocation expr", type);
		
		this.externalFunction = externalFunction;
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
		for(Expression child : getWalkableChildren())
			child.collectNeededEntities(needs);
	}
}
