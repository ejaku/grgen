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
import java.util.Vector;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
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
	static {
		setName(DeclExprNode.class, "cast expression");
	}
	
	BaseNode type; // target type of the cast
	BaseNode expr; // expression to be casted
		
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
	public CastNode(Coords coords, BaseNode targetType, BaseNode expr) {
		super(coords);
		this.type = targetType==null ? NULL : targetType;
		becomeParent(this.type);
		this.expr = expr==null ? NULL : expr;
		becomeParent(this.expr);
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
		this(coords, targetType, expr);
		nodeResolvedSetResult(resolveResult); 
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(type);
		children.add(expr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("type"); 
		childrenNames.add("expr");
		return childrenNames;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		Resolver typeResolver = new DeclTypeResolver(BasicTypeNode.class);
		BaseNode resolved = typeResolver.resolve(type);
		successfullyResolved = resolved!=null && successfullyResolved;
		if(resolved!=null && resolved!=type) {
			becomeParent(resolved);
			type = resolved;
		}
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
			
		}
		
		successfullyResolved = type.resolve() && successfullyResolved;
		successfullyResolved = expr.resolve() && successfullyResolved;
		return successfullyResolved;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#check() */
	protected boolean check() {
		if(!resolutionResult()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean successfullyChecked = checkLocal();
		if(successfullyChecked) {
			successfullyChecked = typeCheckLocal();
		}
		nodeCheckedSetResult(successfullyChecked);
		
		successfullyChecked = type.check() && successfullyChecked;
		successfullyChecked = expr.check() && successfullyChecked;
		return successfullyChecked;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 * A cast node is valid, if the second child is an expression node
	 * and the first node is a type node identifier.
	 */
	protected boolean checkLocal() {
		return (new SimpleChecker(BasicTypeNode.class)).check(type, error)
			&& (new SimpleChecker(ExprNode.class)).check(expr, error);
	}
	
	/**
	 * Check the types of this cast.
	 * Check if the expression can be casted to the given type.
	 * @see de.unika.ipd.grgen.ast.BaseNode#typeCheckLocal()
	 */
	protected boolean typeCheckLocal()
	{
		Collection<TypeNode> castableToTypes = new HashSet<TypeNode>();
		ExprNode exp = (ExprNode) expr;
		
		BaseNode n = type;
		if( !(n instanceof BasicTypeNode) ) {
			reportError("Only primitive types are allowed for casts, but got \"" + n + "\"");
			return false;
		} else {
			BasicTypeNode bt = (BasicTypeNode) type;
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
		ExprNode expr = (ExprNode) this.expr;
		TypeNode type = (TypeNode) this.type;
		
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
		TypeNode type = (TypeNode) this.type;
		return type;
	}
	
	protected IR constructIR()
	{
		Type type = (Type) this.type.checkIR(Type.class);
		Expression expr = (Expression) this.expr.checkIR(Expression.class);
		
		return new Cast(type, expr);
	}
	
	// debug guards to protect again accessing wrong elements
	public void addChild(BaseNode n) {
		assert(false);
	}
	public void setChild(int pos, BaseNode n) {
		assert(false);
	}
	public BaseNode getChild(int i) {
		assert(false);
		return null;
	}
	public int children() {
		assert(false);
		return 0;
	}
	public BaseNode replaceChild(int i, BaseNode n) {
		assert(false);
		return null;
	}
}

