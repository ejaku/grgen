/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MemberInit;
import de.unika.ipd.grgen.ir.MapInit;
import de.unika.ipd.grgen.ir.SetInit;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a member initialization.
 * children: LHS:IdentNode, RHS:ExprNode
 */
public class MemberInitNode extends BaseNode {
	static {
		setName(MemberInitNode.class, "member init");
	}

	private BaseNode lhsUnresolved;
	private DeclNode lhs;
	private ExprNode rhs;

	/**
	 * @param coords The source code coordinates of = operator.
	 * @param member The member to be initialized.
	 * @param expr The expression, that is assigned.
	 */
	public MemberInitNode(Coords coords, IdentNode member, ExprNode expr) {
		super(coords);
		this.lhsUnresolved = member;
		becomeParent(this.lhsUnresolved);
		this.rhs = expr;
		becomeParent(this.rhs);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(lhsUnresolved, lhs));
		children.add(rhs);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("rhs");
		return childrenNames;
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		//Resolver rhsResolver = new OneOfResolver(new Resolver[] {new DeclResolver(DeclNode.class), new MemberInitResolver(DeclNode.class)});
		//successfullyResolved = rhsResolver.resolve(this, RHS) && successfullyResolved;
		if(!lhsResolver.resolve(lhsUnresolved)) return false;
		lhs = lhsResolver.getResult(DeclNode.class);
		return lhsResolver.finish();
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal() {
		return typeCheckLocal();
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

	/**
	 * Construct the intermediate representation from a member init.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		if(rhs instanceof MapInitNode) {
			MapInit mapInit = rhs.checkIR(MapInit.class);
			mapInit.setMember(lhs.checkIR(Entity.class));
			return mapInit;
		} else if(rhs instanceof SetInitNode) {
			SetInit setInit = rhs.checkIR(SetInit.class);
			setInit.setMember(lhs.checkIR(Entity.class));
			return setInit;
		} else {
			return new MemberInit(lhs.checkIR(Entity.class), rhs.checkIR(Expression.class));
		}
	}
}
