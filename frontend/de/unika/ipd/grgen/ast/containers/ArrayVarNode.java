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

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.ArrayVarExpr;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayVarNode extends ArrayAccumulationMethodNode
{
	static {
		setName(ArrayVarNode.class, "array var");
	}

	public ArrayVarNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof ArrayTypeNode)) {
			targetExpr.reportError("This argument to array var method must be of type array<T>");
			return false;
		}
		ArrayTypeNode arrayType = (ArrayTypeNode)targetExpr.getType();
		if(!arrayType.valueType.isAccumulatableType()) {
			targetExpr.reportError("The array value type of the array var method must be one of: " + TypeNode.getAccumulatableTypesAsString());
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.doubleType;
	}

	@Override
	public boolean isValidTargetTypeOfAccumulation(TypeNode type) {
		return type.isEqual(BasicTypeNode.doubleType);
	}

	@Override
	protected IR constructIR() {
		return new ArrayVarExpr(targetExpr.checkIR(Expression.class));
	}
}
