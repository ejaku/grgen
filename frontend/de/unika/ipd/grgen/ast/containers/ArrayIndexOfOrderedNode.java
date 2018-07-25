/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.containers.ArrayIndexOfOrderedExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayIndexOfOrderedNode extends ExprNode
{
	static {
		setName(ArrayIndexOfOrderedNode.class, "array index of ordered");
	}

	private ExprNode targetExpr;
	private ExprNode valueExpr;

	public ArrayIndexOfOrderedNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof ArrayTypeNode)) {
			targetExpr.reportError("This argument to array indexOfOrdered expression must be of type array<T>");
			return false;
		}
		TypeNode valueType = valueExpr.getType();
		ArrayTypeNode arrayType = ((ArrayTypeNode)targetExpr.getType());
		if (!valueType.isEqual(arrayType.valueType))
		{
			valueExpr = becomeParent(valueExpr.adjustType(arrayType.valueType, getCoords()));
			if(valueExpr == ConstNode.getInvalid()) {
				valueExpr.reportError("Argument (value) to "
						+ "array indexOfOrdered method must be of type " +arrayType.valueType.toString());
				return false;
			}
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.intType;
	}

	@Override
	protected IR constructIR() {
		return new ArrayIndexOfOrderedExpr(targetExpr.checkIR(Expression.class),
				valueExpr.checkIR(Expression.class));
	}
}
