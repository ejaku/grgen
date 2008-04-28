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

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Constant;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Constant expressions.
 * A constant is 0-ary operator.
 */
public abstract class ConstNode extends OpNode {
	/** The value of the constant. */
	protected Object value;

	/** A name for the constant. */
	protected String name;

	private static final ConstNode INVALID = new InvalidConstNode(
		Coords.getBuiltin(), "invalid const", "invalid value");

	public static final ConstNode getInvalid() {
		return INVALID;
	}

	/**
	 * @param coords The source code coordinates.
	 */
	public ConstNode(Coords coords, String name, Object value) {
		super(coords, OperatorSignature.CONST);
		this.value = value;
		this.name = name;
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		return true;
	}

	/**
	 * Get the value of the constant.
	 * @return The value.
	 */
	public Object getValue() {
		return value;
	}

	/**
	 * Include the constants value in its string representation.
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return OperatorSignature.getName(getOpId()) + " " + value.toString();
	}

	public String getNodeLabel() {
		return toString();
	}

	/**
	 * Just a convenience function.
	 * @return The IR object.
	 */
	protected Constant getConstant() {
		return checkIR(Constant.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		return new Constant(getType().getType(), value);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.ExprNode#isConstant()
	 */
	public boolean isConst() {
		return true;
	}

	/**
	 * Check, if the constant is valid.
	 * @return true, if the constant is valid.
	 */
	protected boolean isValid() {
		return true;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.ExprNode#getType()
	 */
	public TypeNode getType() {
		return BasicTypeNode.errorType;
	}

	/**
	 * Cast this constant to a new type.
	 * @param type The new type.
	 * @return A new constant with the corresponding value and a new type.
	 */
	public final ConstNode castTo(TypeNode type) {
		ConstNode res = getInvalid();

		if (getType().isEqual(type)) {
			res = this;
		} else if (getType().isCastableTo(type)) {
			res = doCastTo(type);
		}

		return res;
	}

	/**
	 * Implement this method to implement casting.
	 * You don't have to check for types that are not castable to the
	 * type of this constant.
	 * @param type The new type.
	 * @return A constant of the new type.
	 */
	protected abstract ConstNode doCastTo(TypeNode type);
}
