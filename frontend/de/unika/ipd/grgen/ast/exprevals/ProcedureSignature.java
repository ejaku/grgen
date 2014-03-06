/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
public class ProcedureSignature extends Base {

	/** Result types. */
	private TypeNode[] resTypes;

	/** Argument types. */
	private TypeNode[] opTypes;

	/**
	 * Make a new procedure signature.
	 * @param resTypes The result types.
	 * @param opTypes The operand types.
	 */
	public ProcedureSignature(TypeNode[] resTypes, TypeNode[] opTypes) {
		this.resTypes = resTypes;
		this.opTypes = opTypes;
	}

	/**
	 * Get the result types of this procedure signature.
	 * @return The result type.
	 */
	protected TypeNode[] getResultTypes() {
		return resTypes;
	}

	/**
	 * Get the operand types of this procedure signature.
	 * @return The operand types.
	 */
	protected TypeNode[] getOperandTypes() {
		return opTypes;
	}

	/**
	 * Get the number of implicit type casts needed for calling this
	 * procedure signature with the given operands.
	 * @param ops The operands
	 * @return The number of implicit type casts needed to apply the operands
	 * to this procedure signature. <code>Integer.MAX_VALUE</code> is returned,
	 * if the operands cannot be applied to this procedure signature.
	 */
	protected int getDistance(TypeNode[] ops) {
		int res = Integer.MAX_VALUE;

		if(ops.length == opTypes.length) {
			res = 0;
			for(int i = 0; i < opTypes.length; i++) {
				debug.report(NOTE, "" + i + ": arg type: " + ops[i]
					+ ", op type: " + opTypes[i]);

				boolean equal = ops[i].isEqual(opTypes[i]);
				boolean compatible = ops[i].isCompatibleTo(opTypes[i]);

				/* Compute indirect compatiblity interms of the "compatibility
				 * distance". Note that the below procedure only test indirect
				 * compatibility of distance two. If you need more you have to
				 * implement it!!! */
				int compatDist = ops[i].compatibilityDist(opTypes[i]);

				debug.report(NOTE, "equal: " + equal + ", compatible: " + compatible);

				if (equal)
					continue;
				else if (compatible)
					res++;
				else if (compatDist > 0)
					res += compatDist;
				else {
					res = Integer.MAX_VALUE;
					break;
				}

				/*
				if(!compatible) {
					res = Integer.MAX_VALUE;
					break;
				} else if(!equal && compatible)
					res++;
				 */
			}
		}

		return res;
	}

}
