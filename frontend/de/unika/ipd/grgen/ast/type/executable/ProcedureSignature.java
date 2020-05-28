/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.type.executable;

import java.util.Vector;

import de.unika.ipd.grgen.ast.type.TypeNode;

/**
 * Procedure abstraction.
 */
public interface ProcedureSignature
{
	/**
	 * Get the result(/return) types of this procedure signature.
	 * @return The result(/return) type.
	 */
	Vector<TypeNode> getResultTypes();

	/**
	 * Get the parameter(/operand) types of this procedure signature.
	 * @return The parameter(/operand) types.
	 */
	Vector<TypeNode> getParameterTypes();

	/**
	 * Get the number of implicit type casts needed for calling this
	 * procedure signature with the given arguments(/operands).
	 * @param argumentTypes The types of the arguments(/operands)
	 * @return The number of implicit type casts needed to apply the arguments(/operands)
	 * to this procedure signature. <code>Integer.MAX_VALUE</code> is returned,
	 * if the arguments(/operands) cannot be applied to this procedure signature.
	 */
	int getDistance(Vector<TypeNode> argumentTypes);
}
