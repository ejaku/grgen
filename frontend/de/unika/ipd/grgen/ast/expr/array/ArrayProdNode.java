/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.expr.array.ArrayProdExpr;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayProdNode extends ArrayAccumulationMethodNode
{
	static {
		setName(ArrayProdNode.class, "array prod");
	}

	public ArrayProdNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		ArrayTypeNode arrayType = getTargetType();
		if(!arrayType.valueType.isAccumulatableType()) {
			targetExpr.reportError("The array value type of the array prod method must be one of: "
					+ TypeNode.getAccumulatableTypesAsString());
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
		return new ArrayProdExpr(targetExpr.checkIR(Expression.class));
	}
}
