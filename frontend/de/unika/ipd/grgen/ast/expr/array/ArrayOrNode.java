/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.expr.array.ArrayOrExpr;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayOrNode extends ArrayAccumulationMethodNode
{
	static {
		setName(ArrayOrNode.class, "array or");
	}

	public ArrayOrNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		ArrayTypeNode arrayType = getTargetType();
		if(!arrayType.valueType.isEqual(BasicTypeNode.booleanType)) {
			targetExpr.reportError("The array value type of the array or method must be boolean.");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.booleanType;
	}

	@Override
	public boolean isValidTargetTypeOfAccumulation(TypeNode type)
	{
		return type.isEqual(BasicTypeNode.booleanType);
	}

	@Override
	public String getValidTargetTypesOfAccumulation()
	{
		return "boolean";
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		return new ArrayOrExpr(targetExpr.checkIR(Expression.class));
	}
}
