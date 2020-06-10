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
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.deque.DequeIndexOfExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class DequeIndexOfNode extends DequeFunctionMethodInvocationBaseExprNode
{
	static {
		setName(DequeIndexOfNode.class, "deque index of");
	}

	private ExprNode valueExpr;
	private ExprNode startIndexExpr;

	public DequeIndexOfNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr)
	{
		super(coords, targetExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	public DequeIndexOfNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr, ExprNode startIndexExpr)
	{
		super(coords, targetExpr);
		this.valueExpr = becomeParent(valueExpr);
		this.startIndexExpr = becomeParent(startIndexExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(valueExpr);
		if(startIndexExpr != null)
			children.add(startIndexExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("valueExpr");
		if(startIndexExpr != null)
			childrenNames.add("startIndex");
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
				valueExpr.reportError("Argument (value) to deque indexOf method must be of type "
						+ dequeType.valueType.toString());
				return false;
			}
		}
		if(startIndexExpr != null && !startIndexExpr.getType().isEqual(BasicTypeNode.intType)) {
			startIndexExpr.reportError("Argument (start index) to deque indexOf expression must be of type int");
			return false;
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
		targetExpr = targetExpr.evaluate();
		valueExpr = valueExpr.evaluate();
		if(startIndexExpr != null) {
			startIndexExpr = startIndexExpr.evaluate();
			return new DequeIndexOfExpr(targetExpr.checkIR(Expression.class),
					valueExpr.checkIR(Expression.class),
					startIndexExpr.checkIR(Expression.class));
		} else {
			return new DequeIndexOfExpr(targetExpr.checkIR(Expression.class),
					valueExpr.checkIR(Expression.class));
		}
	}
}
