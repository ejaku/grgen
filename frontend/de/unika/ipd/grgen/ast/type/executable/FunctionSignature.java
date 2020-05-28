/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.type.executable;

import java.util.Vector;

import de.unika.ipd.grgen.ast.type.TypeNode;

/**
 * Function abstraction.
 */
public interface FunctionSignature
{
	/**
	 * Get the result(/return) type of this function signature.
	 * @return The result(/return) type.
	 */
	public TypeNode getResultType();

	/**
	 * Get the parameter(/operand) types of this function signature.
	 * @return The parameter(/operand) types.
	 */
	public Vector<TypeNode> getParameterTypes();

	/**
	 * Get the number of implicit type casts needed for calling this
	 * function signature with the given arguments(/operands).
	 * @param argumentTypes The types of the arguments(/operands)
	 * @return The number of implicit type casts needed to apply the arguments(/operands)
	 * to this function signature. <code>Integer.MAX_VALUE</code> is returned,
	 * if the arguments(/operands) cannot be applied to this functions signature.
	 */
	int getDistance(Vector<TypeNode> argumentTypes);
}
