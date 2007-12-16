/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */


/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;

import de.unika.ipd.grgen.parser.Coords;
import java.util.HashSet;
import java.util.Collection;

/**
 * Base class for all AST nodes representing expressions.
 */
public abstract class ExprNode extends BaseNode
{
	static {
		setName(ExprNode.class, "expression");
	}
	
	static private final ExprNode INVALID = new ExprNode(Coords.getInvalid())
	{
		/** @see de.unika.ipd.grgen.ast.BaseNode#doResolve() */
		protected boolean doResolve() {
			return getResolve();
		}
		public TypeNode getType() {
			return BasicTypeNode.errorType;
		}
		public String toString() {
			return "invalid expression";
		}
		public String getKindString() {
			return "invalid expression";
		}
	};
	
	static {
		setName(INVALID.getClass(), "invalid expression");
	}
	
	/**
	 * Make a new expression
	 */
	public ExprNode(Coords coords) {
		super(coords);
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
			return new CastNode(getCoords(), tgt, this);
		}

		/* in general we would have to compute a shortest path in the conceptual
		 * compatibility graph. But as it is very small we do it shortly
		 * and nicely with this little piece of code finding a compatibility
		 * with only one indirection */
		Collection<TypeNode> coll = new HashSet<TypeNode>();
		src.getCompatibleToTypes(coll);
		for (TypeNode t : coll) {
			if (t.isCompatibleTo(tgt)) {
				return new CastNode(getCoords(), tgt, new CastNode(getCoords(), t, this));
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
