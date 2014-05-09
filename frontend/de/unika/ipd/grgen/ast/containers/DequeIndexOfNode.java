/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.containers.DequeIndexOfExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class DequeIndexOfNode extends ExprNode
{
	static {
		setName(DequeIndexOfNode.class, "deque index of");
	}

	private ExprNode targetExpr;
	private ExprNode valueExpr;
	private ExprNode startIndexExpr;

	public DequeIndexOfNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	public DequeIndexOfNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr, ExprNode startIndexExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
		this.valueExpr = becomeParent(valueExpr);
		this.startIndexExpr = becomeParent(startIndexExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(valueExpr);
		if(startIndexExpr!=null)
			children.add(startIndexExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("valueExpr");
		if(startIndexExpr!=null)
			childrenNames.add("startIndex");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof DequeTypeNode)) {
			targetExpr.reportError("This argument to deque indexOf expression must be of type deque<T>");
			return false;
		}
		TypeNode valueType = valueExpr.getType();
		DequeTypeNode dequeType = ((DequeTypeNode)targetExpr.getType());
		if (!valueType.isEqual(dequeType.valueType))
		{
			valueExpr = becomeParent(valueExpr.adjustType(dequeType.valueType, getCoords()));
			if(valueExpr == ConstNode.getInvalid()) {
				valueExpr.reportError("Argument (value) to "
						+ "deque indexOf method must be of type " +dequeType.valueType.toString());
				return false;
			}
		}
		if(startIndexExpr!=null
			&& !startIndexExpr.getType().isEqual(BasicTypeNode.intType)) {
			startIndexExpr.reportError("Argument (start index) to deque indexOf expression must be of type int");
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
		if(startIndexExpr!=null)
			return new DequeIndexOfExpr(targetExpr.checkIR(Expression.class),
					valueExpr.checkIR(Expression.class),
					startIndexExpr.checkIR(Expression.class));
		else
			return new DequeIndexOfExpr(targetExpr.checkIR(Expression.class),
					valueExpr.checkIR(Expression.class));
	}
}
