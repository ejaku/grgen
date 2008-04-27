/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;

import de.unika.ipd.grgen.parser.Coords;

/**
 * Base class for all AST nodes representing expressions.
 */
public abstract class ExprNode extends BaseNode {
	static {
		setName(ExprNode.class, "expression");
	}

	private static final ExprNode INVALID = new InvalidExprNode();

	/**
	 * Make a new expression
	 */
	public ExprNode(Coords coords) {
		super(coords);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		return true;
	}

	public static ExprNode getInvalid() {
		return INVALID;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	public Color getNodeColor()	{
		return Color.PINK;
	}

	/**
	 * Get the type of the expression.
	 * @return The type of this expression node.
	 */
	public abstract TypeNode getType();

	/**
	 * Adjust the type of the expression.
	 * The type can be adjusted by inserting an implicit cast.
	 * @param type The type the expression should be adjusted to. It must be
	 * compatible with the type of the expression.
	 * @return A new expression, that is of a valid type and represents
	 * this expression, if <code>type</code> was compatible with the type of
	 * this expression, an invalid expression otherwise (one of an error type).
	 */
	protected ExprNode adjustType(TypeNode tgt)	{
		TypeNode src = getType();

		if(src.isEqual(tgt)) {
			return this;
		}

		if( src.isCompatibleTo(tgt) ) {
			return new CastNode(getCoords(), tgt, this, this);
		}

		/* in general we would have to compute a shortest path in the conceptual
		 * compatibility graph. But as it is very small we do it shortly
		 * and nicely with this little piece of code finding a compatibility
		 * with only one indirection */
		for (TypeNode t : src.getCompatibleToTypes()) {
			if (t.isCompatibleTo(tgt)) {
				return new CastNode(getCoords(), tgt, new CastNode(getCoords(), t, this, this), this);
			}
		}

		return ConstNode.getInvalid();
	}

	/**
	 * Check, if the expression is constant.
	 * @return True, if the expression can be evaluated to a constant.
	 */
	public boolean isConst() {
		return false;
	}

	/**
	 * Try to evaluate and return a constant version
	 * of this expression
	 */
	public ConstNode getConst()	{
		ExprNode expr = evaluate();
		if(expr instanceof ConstNode) {
			return (ConstNode)expr;
		} else {
			return ConstNode.getInvalid();
		}
	}

	/**
	 * This method is only called, if the expression is constant, so you don't
	 * have to check for it.
	 * @return The value of the expression.
	 */
	public ExprNode evaluate() {
		return this;
	}
}
