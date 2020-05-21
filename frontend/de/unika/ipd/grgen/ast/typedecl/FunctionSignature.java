/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.typedecl;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.util.Base;

/**
 * Function abstraction.
 * TODO: use it or remove it
 */
public class FunctionSignature extends Base
{
	/** Result type of the function. */
	private TypeNode resultType;

	/** Parameter types. */
	private TypeNode[] parameterTypes;

	/**
	 * Make a new function signature.
	 * @param resType The result type.
	 * @param opTypes The operand types.
	 */
	public FunctionSignature(TypeNode resType, TypeNode[] opTypes)
	{
		this.resultType = resType;
		this.parameterTypes = opTypes;
	}

	/**
	 * Get the result type of this function signature.
	 * @return The result type.
	 */
	public TypeNode getResultType()
	{
		return resultType;
	}

	/**
	 * Get the operand types of this function signature.
	 * @return The operand types.
	 */
	public TypeNode[] getOperandTypes()
	{
		return parameterTypes;
	}

	/**
	 * Get the number of implicit type casts needed for calling this
	 * function signature with the given operands.
	 * @param argumentTypes The operands
	 * @return The number of implicit type casts needed to apply the operands
	 * to this function signature. <code>Integer.MAX_VALUE</code> is returned,
	 * if the operands cannot be applied to this functions signature.
	 */
	protected int getDistance(TypeNode[] argumentTypes)
	{
		if(argumentTypes.length != parameterTypes.length)
			return Integer.MAX_VALUE;

		int distance = 0;
		for(int i = 0; i < parameterTypes.length; i++) {
			debug.report(NOTE, "" + i + ": arg type: " + argumentTypes[i] + ", operand type: " + parameterTypes[i]);

			boolean equal = argumentTypes[i].isEqual(parameterTypes[i]);
			boolean compatible = argumentTypes[i].isCompatibleTo(parameterTypes[i]);
			debug.report(NOTE, "equal: " + equal + ", compatible: " + compatible);

			int compatibilityDistance = argumentTypes[i].compatibilityDistance(parameterTypes[i]);

			if(compatibilityDistance == Integer.MAX_VALUE)
				return Integer.MAX_VALUE;

			distance += compatibilityDistance;
		}

		return distance;
	}
}
