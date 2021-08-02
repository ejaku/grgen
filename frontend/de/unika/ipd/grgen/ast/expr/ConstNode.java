/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Constant;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Constant expressions.
 * A constant is 0-ary operator.
 */
public abstract class ConstNode extends OperatorNode
{
	/** The value of the constant. */
	protected Object value;

	/** A name for the constant. */
	protected String name;

	private static final ConstNode INVALID = new InvalidConstNode(
		Coords.getBuiltin(), "invalid const", "invalid value");

	public static final ConstNode getInvalid()
	{
		return INVALID;
	}

	/**
	 * @param coords The source code coordinates.
	 */
	public ConstNode(Coords coords, String name, Object value)
	{
		super(coords, OperatorDeclNode.Operator.CONST);
		this.value = value;
		this.name = name;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	/**
	 * Get the value of the constant.
	 * @return The value.
	 */
	public Object getValue()
	{
		return value;
	}

	/**
	 * Include the constants value in its string representation.
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString()
	{
		return OperatorDeclNode.getName(getOperator()) + " " + value.toString();
	}

	@Override
	public String getNodeLabel()
	{
		return toString();
	}

	/**
	 * Just a convenience function.
	 * @return The IR object.
	 */
	protected Constant getConstant()
	{
		return checkIR(Constant.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		return new Constant(getType().getType(), value);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.expr.ExprNode#getType()
	 */
	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.errorType;
	}

	/**
	 * Cast this constant to a new type.
	 * @param type The new type.
	 * @return A new constant with the corresponding value and a new type.
	 */
	public final ConstNode castTo(TypeNode type)
	{
		ConstNode res = getInvalid();

		if(getType().isEqual(type)) {
			res = this;
		} else if(getType().isCastableTo(type)) {
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
