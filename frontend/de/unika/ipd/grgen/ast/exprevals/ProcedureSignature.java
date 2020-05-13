/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.util.Base;

/**
 * Procedure abstraction.
 */
public class ProcedureSignature extends Base
{
	/** Result types. */
	private TypeNode[] resultTypes;

	/** Parameter types. */
	private TypeNode[] parameterTypes;

	/**
	 * Make a new procedure signature.
	 * @param resTypes The result types.
	 * @param opTypes The operand types.
	 */
	public ProcedureSignature(TypeNode[] resTypes, TypeNode[] opTypes)
	{
		this.resultTypes = resTypes;
		this.parameterTypes = opTypes;
	}

	/**
	 * Get the result types of this procedure signature.
	 * @return The result type.
	 */
	protected TypeNode[] getResultTypes()
	{
		return resultTypes;
	}

	/**
	 * Get the operand types of this procedure signature.
	 * @return The operand types.
	 */
	protected TypeNode[] getOperandTypes()
	{
		return parameterTypes;
	}

	/**
	 * Get the number of implicit type casts needed for calling this
	 * procedure signature with the given operands.
	 * @param argumentTypes The operands
	 * @return The number of implicit type casts needed to apply the operands
	 * to this procedure signature. <code>Integer.MAX_VALUE</code> is returned,
	 * if the operands cannot be applied to this procedure signature.
	 */
	protected int getDistance(TypeNode[] argumentTypes)
	{
		if(argumentTypes.length == parameterTypes.length)
			return Integer.MAX_VALUE;

		int distance = 0;
		for(int i = 0; i < parameterTypes.length; i++) {
			debug.report(NOTE, "" + i + ": arg type: " + argumentTypes[i] + ", op type: " + parameterTypes[i]);

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
