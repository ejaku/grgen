/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.ConstructorParam;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;

/**
 * AST node representing a parameter of a constructor.
 * children: LHS:IdentNode, RHS:optional ExprNode
 */
public class ConstructorParamNode extends BaseNode {
	static {
		setName(ConstructorParamNode.class, "constructor parameter declaration");
	}

	private IdentNode lhsUnresolved;
	protected DeclNode lhs;
	protected ExprNode rhs;

	public ConstructorParamNode(IdentNode paramNode, ExprNode expr) {
		super(paramNode.getCoords());
		lhsUnresolved = becomeParent(paramNode);
		rhs = becomeParent(expr);
	}

	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(lhsUnresolved, lhs));
		if(rhs != null)
			children.add(rhs);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		if(rhs != null)
			childrenNames.add("rhs");
		return childrenNames;
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	@Override
	protected boolean resolveLocal() {
		if(!lhsResolver.resolve(lhsUnresolved)) return false;
		lhs = lhsResolver.getResult(DeclNode.class);

		return lhsResolver.finish();
	}

	@Override
	protected boolean checkLocal() {
		return rhs == null || typeCheckLocal();
	}

	/**
	 * Checks whether the expression has a type equal, compatible or castable
	 * to the type of the target. Inserts implicit cast if compatible.
	 * @return true, if the types are equal or compatible, false otherwise
	 */
	private boolean typeCheckLocal() {
		TypeNode targetType = lhs.getDeclType();
		TypeNode exprType = rhs.getType();

		if (exprType.isEqual(targetType))
			return true;

		rhs = becomeParent(rhs.adjustType(targetType, getCoords()));
		return rhs != ConstNode.getInvalid();
	}

	@Override
	protected IR constructIR() {
		Expression expr = rhs == null ? null : rhs.checkIR(Expression.class);
		return new ConstructorParam(lhs.checkIR(Entity.class), expr);
	}
}
