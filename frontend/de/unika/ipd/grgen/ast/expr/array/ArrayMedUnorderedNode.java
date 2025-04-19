/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.expr.array.ArrayMedUnorderedExpr;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayMedUnorderedNode extends ArrayAccumulationMethodNode
{
	static {
		setName(ArrayMedUnorderedNode.class, "array med unordered");
	}

	public ArrayMedUnorderedNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		ArrayTypeNode arrayType = getTargetType();
		if(!arrayType.valueType.isAccumulatableType()) {
			targetExpr.reportError("The array function method medUnordered can only be employed on an object of type array<" + TypeNode.getAccumulatableTypesAsString() + ">"
					+ " (but is employed on an object of type " + arrayType.getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.doubleType;
	}

	@Override
	public boolean isValidTargetTypeOfAccumulation(TypeNode type)
	{
		return type.isEqual(BasicTypeNode.doubleType);
	}

	@Override
	public String getValidTargetTypesOfAccumulation()
	{
		return "double";
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		return new ArrayMedUnorderedExpr(targetExpr.checkIR(Expression.class));
	}
}
