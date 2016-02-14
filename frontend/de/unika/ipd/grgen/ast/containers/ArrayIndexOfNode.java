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
import de.unika.ipd.grgen.ir.containers.ArrayIndexOfExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayIndexOfNode extends ExprNode
{
	static {
		setName(ArrayIndexOfNode.class, "array index of");
	}

	private ExprNode targetExpr;
	private ExprNode valueExpr;
	private ExprNode startIndexExpr;

	public ArrayIndexOfNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	public ArrayIndexOfNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr, ExprNode startIndexExpr)
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
		if(!(targetType instanceof ArrayTypeNode)) {
			targetExpr.reportError("This argument to array indexOf expression must be of type array<T>");
			return false;
		}
		TypeNode valueType = valueExpr.getType();
		ArrayTypeNode arrayType = ((ArrayTypeNode)targetExpr.getType());
		if (!valueType.isEqual(arrayType.valueType))
		{
			valueExpr = becomeParent(valueExpr.adjustType(arrayType.valueType, getCoords()));
			if(valueExpr == ConstNode.getInvalid()) {
				valueExpr.reportError("Argument (value) to "
						+ "array indexOf method must be of type " +arrayType.valueType.toString());
				return false;
			}
		}
		if(startIndexExpr!=null
			&& !startIndexExpr.getType().isEqual(BasicTypeNode.intType)) {
			startIndexExpr.reportError("Argument (start index) to array indexOf expression must be of type int");
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
			return new ArrayIndexOfExpr(targetExpr.checkIR(Expression.class),
					valueExpr.checkIR(Expression.class),
					startIndexExpr.checkIR(Expression.class));
		else
			return new ArrayIndexOfExpr(targetExpr.checkIR(Expression.class),
					valueExpr.checkIR(Expression.class));
	}
}
