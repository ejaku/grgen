/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;

import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;

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
	@Override
	protected boolean resolveLocal() {
		return true;
	}

	/*
	 * This sets the symbol definition to the right place, if the definition is behind the actual position.
	 * TODO: extract and unify this method to a common place/code duplication
	 * better yet: move it to own pass before resolving
	 */
	public static boolean fixupDefinition(BaseNode elem, Scope scope) {
		if(!(elem instanceof IdentNode)) {
			return true;
		}
		IdentNode id = (IdentNode)elem;
		
		debug.report(NOTE, "Fixup " + id + " in scope " + scope);

		// Get the definition of the ident's symbol local to the owned scope.
		Symbol.Definition def = scope.getCurrDef(id.getSymbol());
		debug.report(NOTE, "definition is: " + def);

		// The result is true, if the definition's valid.
		boolean res = def.isValid();

		// If this definition is valid, i.e. it exists,
		// the definition of the ident is rewritten to this definition,
		// else, an error is emitted,
		// since this ident was supposed to be defined in this scope.
		if(res) {
			id.setSymDef(def);
		} else {
			id.reportError("Identifier \"" + id + "\" not declared in this scope: " + scope);
		}

		return res;
	}
	
	public static ExprNode getInvalid() {
		return INVALID;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	@Override
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

	protected ExprNode adjustType(TypeNode targetType, Coords errorCoords)
	{
		ExprNode expr = adjustType(targetType);

		if (expr == ConstNode.getInvalid()) {
			String msg;
			if (getType().isCastableTo(targetType)) {
				msg = "Assignment of " + getType() + " to " + targetType + " without a cast";
			} else {
				msg = "Incompatible assignment from " + getType() + " to " + targetType;
			}
			error.error(errorCoords, msg);
		}
		return expr;
	}

	/**
	 * Tries to simplify this node.
	 * @return The possibly simplified value of the expression.
	 */
	protected ExprNode evaluate() {
		return this;
	}
}
