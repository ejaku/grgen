/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.util.Base;

/**
 * Function abstraction.
 */
public class FunctionSignature extends Base {

	/** Result type of the function. */
	private TypeNode resType;

	/** Argument types. */
	private TypeNode[] opTypes;

	/**
	 * Make a new function signature.
	 * @param resType The result type.
	 * @param opTypes The operand types.
	 */
  public FunctionSignature(TypeNode resType, TypeNode[] opTypes) {
		this.resType = resType;
		this.opTypes = opTypes;
  }

	/**
	 * Get the arity of the function.
	 * @return The arity.
	 */
	public int getArity() {
		return opTypes.length;
	}

	/**
	 * Get the result type of this function signature.
	 * @return The result type.
	 */
	public TypeNode getResultType() {
		return resType;
	}

	/**
	 * Get the operand types of this function signature.
	 * @return The operand types.
	 */
	public TypeNode[] getOperandTypes() {
		return opTypes;
	}

	/**
	 * Checks, if this function can be applied to th given operands.
	 * @param ops The operands.
	 * @return true, if the operands are valid for this function, false,
	 * if not.
	 */
	public boolean isApplicable(TypeNode[] ops) {
		return getDistance(ops) != Integer.MAX_VALUE;
	}

	/**
	 * Get the number of implicit type casts needed for calling this
	 * function signature with the given operands.
	 * @param ops The operands
	 * @return The number of implicit type casts needed to apply the operands
	 * to this function signature. <code>Integer.MAX_VALUE</code> is returned,
	 * if the operands cannot be applied to this functions signature.
	 */
	public int getDistance(TypeNode[] ops) {
		int res = Integer.MAX_VALUE;

		if(ops.length == opTypes.length) {
			res = 0;
			for(int i = 0; i < opTypes.length; i++) {
				debug.report(NOTE, "" + i + ": arg type: " + ops[i]
					+ ", op type: " + opTypes[i]);

				boolean equal = ops[i].isEqual(opTypes[i]);
				boolean compatible = ops[i].isCompatibleTo(opTypes[i]);

				/* Compute indirect compatiblity interms of the "compatibility
				 * distance". Note that the below function only test indirect
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
