/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfOrderedExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayIndexOfOrderedNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayIndexOfOrderedNode.class, "array index of ordered");
	}

	private ExprNode valueExpr;

	public ArrayIndexOfOrderedNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr)
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
		ArrayTypeNode arrayType = getTargetType();
		if(!valueType.isEqual(arrayType.valueType)) {
			ExprNode valueExprOld = valueExpr;
			valueExpr = becomeParent(valueExpr.adjustType(arrayType.valueType, getCoords()));
			if(valueExpr == ConstNode.getInvalid()) {
				valueExprOld.reportError("The array function method indexOfOrdered expects as 1. argument (valueToSearchFor) a value of type " + arrayType.valueType.toStringWithDeclarationCoords()
						+ " (but is given a value of type " + valueType.toStringWithDeclarationCoords() + ").");
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
		targetExpr = targetExpr.evaluate();
		valueExpr = valueExpr.evaluate();
		return new ArrayIndexOfOrderedExpr(targetExpr.checkIR(Expression.class),
				valueExpr.checkIR(Expression.class));
	}
}
