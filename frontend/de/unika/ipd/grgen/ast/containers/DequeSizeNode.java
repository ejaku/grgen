/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.5
 * Copyright (C) 2003-2012 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.DequeSizeExpr;
import de.unika.ipd.grgen.parser.Coords;

public class DequeSizeNode extends ExprNode
{
	static {
		setName(DequeSizeNode.class, "deque size expression");
	}

	private ExprNode targetExpr;

	public DequeSizeNode(Coords coords, ExprNode targetExpr)
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
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof DequeTypeNode)) {
			targetExpr.reportError("This argument to deque size expression must be of type deque<T>");
			return false;
		}
		return true;

	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.intType;
	}

	@Override
	protected IR constructIR() {
		return new DequeSizeExpr(targetExpr.checkIR(Expression.class));
	}
}
