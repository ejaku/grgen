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

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.Cast;
import de.unika.ipd.grgen.parser.Coords;


/**
 * A cast operator for expressions.
 */
public class CastNode extends ExprNode {
	static {
		setName(DeclExprNode.class, "cast expression");
	}

	// target type of the cast
	BasicTypeNode type; 
	BaseNode typeUnresolved;
    // expression to be casted
	ExprNode expr;

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
	public CastNode(Coords coords, BaseNode targetType, ExprNode expr) {
		super(coords);
		this.typeUnresolved = targetType;
		becomeParent(this.typeUnresolved);
		this.expr = expr;
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
	public CastNode(Coords coords, TypeNode targetType, ExprNode expr, boolean resolvedAndChecked) {
		this(coords, targetType, expr);
		assert resolvedAndChecked;
		resolve();
		check();
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(typeUnresolved, type));
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
		DeclarationTypeResolver<BasicTypeNode> typeResolver = new DeclarationTypeResolver<BasicTypeNode>(BasicTypeNode.class);
		type = typeResolver.resolve(typeUnresolved, this);
		successfullyResolved = type!=null && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		successfullyResolved = (type!=null ? type.resolve() : false) && successfullyResolved;
		successfullyResolved = expr.resolve() && successfullyResolved;
		return successfullyResolved;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 * A cast node is valid, if the second child is an expression node
	 * and the first node is a type node identifier.
	 */
	protected boolean checkLocal() {
		return (new SimpleChecker(ExprNode.class)).check(expr, error)
			& typeCheckLocal();
	}

	/**
	 * Check the types of this cast.
	 * Check if the expression can be casted to the given type.
	 * @see de.unika.ipd.grgen.ast.BaseNode#typeCheckLocal()
	 */
	protected boolean typeCheckLocal() {
		Collection<TypeNode> castableToTypes = new HashSet<TypeNode>();

		expr.getType().getCastableToTypes(castableToTypes);

		boolean result = castableToTypes.contains(type);
		if(!result) {
			reportError("Illegal cast from \"" + expr.getType() + "\" to \"" + type + "\"");
		}

		return result;
	}

	/**
	 * This method is only called, if the expression is constant, so you don't
	 * have to check for it.
	 * @return The value of the expression.
	 */
	public ExprNode evaluate() {
		assert isResolved();
		
		if(expr.isConst()) {
			return expr.getConst().castTo(type);
		} else {
			return this;
		}
	}

	/**
	 * @see de.unika.ipd.grgen.ast.ExprNode#getType()
	 */
	public TypeNode getType() {
		assert isResolved();
		
		return type;
	}

	protected IR constructIR() {
		Type type = (Type) this.type.checkIR(Type.class);
		Expression expr = (Expression) this.expr.checkIR(Expression.class);

		return new Cast(type, expr);
	}
}

