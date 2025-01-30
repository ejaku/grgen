/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayLastIndexOfExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayLastIndexOfNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayLastIndexOfNode.class, "array last index of");
	}

	private ExprNode valueExpr;
	private ExprNode startIndexExpr;

	public ArrayLastIndexOfNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr)
	{
		super(coords, targetExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	public ArrayLastIndexOfNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr, ExprNode startIndexExpr)
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
		ArrayTypeNode arrayType = getTargetType();
		if(!valueType.isEqual(arrayType.valueType)) {
			ExprNode valueExprOld = valueExpr;
			valueExpr = becomeParent(valueExpr.adjustType(arrayType.valueType, getCoords()));
			if(valueExpr == ConstNode.getInvalid()) {
				valueExprOld.reportError("The array function method lastIndexOf expects as 1. argument (valueToSearchFor) a value of type " + arrayType.valueType.toStringWithDeclarationCoords()
						+ " (but is given a value of type " + valueType.toStringWithDeclarationCoords() + ").");
				return false;
			}
		}
		if(startIndexExpr != null && !startIndexExpr.getType().isEqual(BasicTypeNode.intType)) {
			startIndexExpr.reportError("The array function method lastIndexOf expects as 2. argument (start index) a value of type int"
					+ " (but is given a value of type " + startIndexExpr.getType().getTypeName() + ").");
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
			return new ArrayLastIndexOfExpr(targetExpr.checkIR(Expression.class),
					valueExpr.checkIR(Expression.class),
					startIndexExpr.checkIR(Expression.class));
		} else {
			return new ArrayLastIndexOfExpr(targetExpr.checkIR(Expression.class),
					valueExpr.checkIR(Expression.class));
		}
	}
}
