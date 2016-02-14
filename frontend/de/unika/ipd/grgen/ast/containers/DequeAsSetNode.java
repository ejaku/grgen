/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.DequeAsSetExpr;
import de.unika.ipd.grgen.parser.Coords;

public class DequeAsSetNode extends ExprNode
{
	static {
		setName(DequeAsSetNode.class, "deque as set expression");
	}

	private ExprNode targetExpr;

	public DequeAsSetNode(Coords coords, ExprNode targetExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		getType().resolve(); // call to ensure the set type exists
		return true;
	}
	
	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof DequeTypeNode)) {
			targetExpr.reportError("This argument to deque as set expression must be of type deque<T>");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return SetTypeNode.getSetType(((DequeTypeNode)targetExpr.getType()).valueTypeUnresolved);
	}

	@Override
	protected IR constructIR() {
		return new DequeAsSetExpr(targetExpr.checkIR(Expression.class), getType().getType());
	}
}
