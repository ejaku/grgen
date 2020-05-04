/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.containers.ArrayKeepOneForEach;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayKeepOneForEachNode extends ExprNode
{
	static {
		setName(ArrayKeepOneForEachNode.class, "array keep one for each");
	}

	private ExprNode targetExpr;

	public ArrayKeepOneForEachNode(Coords coords, ExprNode targetExpr)
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
		if(!(targetType instanceof ArrayTypeNode)) {
			targetExpr.reportError("This argument to array keepOneForEach expression must be of type array<T>");
			return false;
		}
		ArrayTypeNode arrayType = (ArrayTypeNode)targetType;
		if(!(arrayType.valueType.isFilterableType())) {
			targetExpr.reportError("array keepOneForEach only available for arrays of type " + TypeNode.getFilterableTypesAsString());
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return ((ArrayTypeNode)targetExpr.getType());
	}

	@Override
	protected IR constructIR() {
		return new ArrayKeepOneForEach(targetExpr.checkIR(Expression.class));
	}
}
