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

import java.util.Collection;
import java.util.HashSet;

import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.Cast;


/**
 * A cast operator for expressions.
 */
public class CastNode extends ExprNode
{
	/** The type child index. */
	private final static int TYPE = 0;
	
	/** The expression child index. */
	private final static int EXPR = 1;
	
	/** The resolver for the type */
	static private final Resolver typeResolver = new DeclTypeResolver(BasicTypeNode.class);

	/**
	 * Make a new cast node.
	 * @param coords The source code coordinates.
	 */
	public CastNode(Coords coords) {
		super(coords);
	}
	
	/**
	 * Make a new cast node with a target type and an expression
	 * @param coords The source code coordinates.
	 * @param targetType The target type.
	 * @param expr The expression to be casted.
	 */
	public CastNode(Coords coords, TypeNode targetType, BaseNode expr) {
		this(coords);
		addChild(targetType);
		addChild(expr);
	}
	
	/**
	 * Make a new cast node with a target type and an expression, which is immediately marked as resolved
	 * Only to be called by type adjusting, after tree was already resolved
	 * @param coords The source code coordinates.
	 * @param targetType The target type.
	 * @param expr The expression to be casted.
	 * @param resolveResult Resolution result (should be true)
	 */
	public CastNode(Coords coords, TypeNode targetType, BaseNode expr, boolean resolveResult) {
		this(coords);
		addChild(targetType);
		addChild(expr);
		nodeResolvedSetResult(resolveResult); 
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		successfullyResolved = typeResolver.resolve(this, TYPE) && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = getChild(TYPE).resolve() && successfullyResolved;
		successfullyResolved = getChild(EXPR).resolve() && successfullyResolved;
		return successfullyResolved;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#doCheck() */
	protected boolean doCheck() {
		assert(isResolved());
		if(!resolutionResult()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean successfullyChecked = getCheck();
		if(successfullyChecked) {
			successfullyChecked = getTypeCheck();
		}
		successfullyChecked = getChild(TYPE).doCheck() && successfullyChecked;
		successfullyChecked = getChild(EXPR).doCheck() && successfullyChecked;
	
		return successfullyChecked;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 * A cast node is valid, if the second child is an expression node
	 * and the first node is a type node identifier.
	 */
	protected boolean check() {
		return checkChild(TYPE, BasicTypeNode.class)
			&& checkChild(EXPR, ExprNode.class);
	}
	
	/**
	 * Check the types of this cast.
	 * Check if the expression can be casted to the given type.
	 * @see de.unika.ipd.grgen.ast.BaseNode#typeCheck()
	 */
	protected boolean typeCheck()
	{
		Collection<TypeNode> castableToTypes = new HashSet<TypeNode>();
		ExprNode exp = (ExprNode) getChild(EXPR);
		
		BaseNode n = getChild(TYPE);
		if( !(n instanceof BasicTypeNode) ) {
			reportError("Only primitive types are allowed for casts, but got \"" + n + "\"");
			return false;
		} else {
			BasicTypeNode bt = (BasicTypeNode) getChild(TYPE);
			exp.getType().getCastableToTypes(castableToTypes);
			
			boolean result = castableToTypes.contains(bt);
			if(!result) {
				reportError("Illegal cast from \"" + exp.getType() + "\" to \"" + bt + "\"");
			}
			
			return result;
		}
	}
	
	/**
	 * This method is only called, if the expression is constant, so you don't
	 * have to check for it.
	 * @return The value of the expression.
	 */
	public ExprNode evaluate()
	{
		ExprNode expr = (ExprNode) getChild(EXPR);
		TypeNode type = (TypeNode) getChild(TYPE);
		
		if(expr.isConst()) {
			return expr.getConst().castTo(type);
		} else {
			return this;
		}
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.ExprNode#getType()
	 */
	public TypeNode getType()
	{
		TypeNode type = (TypeNode) getChild(TYPE);
		return type;
	}
	
	protected IR constructIR()
	{
		Type type = (Type) getChild(TYPE).checkIR(Type.class);
		Expression expr = (Expression) getChild(EXPR).checkIR(Expression.class);
		
		return new Cast(type, expr);
	}
}


