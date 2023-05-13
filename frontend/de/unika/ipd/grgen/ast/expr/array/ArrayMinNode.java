/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayMinExpr;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayMinNode extends ArrayAccumulationMethodNode
{
	static {
		setName(ArrayMinNode.class, "array min");
	}

	public ArrayMinNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		ArrayTypeNode arrayType = getTargetType();
		if(!arrayType.valueType.isAccumulatableType()) {
			targetExpr.reportError("The array function method min can only be employed on an object of type array<" + TypeNode.getAccumulatableTypesAsString() + ">"
					+ " (but is employed on an object of type " + arrayType.getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType()
	{
		ArrayTypeNode arrayType = getTargetType();
		return BasicTypeNode.getArrayAccumulationResultType(arrayType.valueType);
	}

	@Override
	public boolean isValidTargetTypeOfAccumulation(TypeNode type)
	{
		return type.isAccumulationTargetType();
	}

	@Override
	public String getValidTargetTypesOfAccumulation()
	{
		return TypeNode.getAccumulationTargetTypesAsString();
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		return new ArrayMinExpr(targetExpr.checkIR(Expression.class));
	}
}
