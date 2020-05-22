/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.deque;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.DequeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.deque.DequeLastIndexOfExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class DequeLastIndexOfNode extends DequeFunctionMethodInvocationBaseExprNode
{
	static {
		setName(DequeLastIndexOfNode.class, "deque last index of");
	}

	private ExprNode valueExpr;

	public DequeLastIndexOfNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr)
	{
		super(coords, targetExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		TypeNode valueType = valueExpr.getType();
		DequeTypeNode dequeType = getTargetType();
		if(!valueType.isEqual(dequeType.valueType)) {
			valueExpr = becomeParent(valueExpr.adjustType(dequeType.valueType, getCoords()));
			if(valueExpr == ConstNode.getInvalid()) {
				valueExpr.reportError("Argument (value) to deque lastIndexOf method must be of type "
						+ dequeType.valueType.toString());
				return false;
			}
		}
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.intType;
	}

	@Override
	protected IR constructIR()
	{
		return new DequeLastIndexOfExpr(targetExpr.checkIR(Expression.class),
				valueExpr.checkIR(Expression.class));
	}
}
