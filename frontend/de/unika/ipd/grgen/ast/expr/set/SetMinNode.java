/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.set;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.set.SetMinExpr;
import de.unika.ipd.grgen.parser.Coords;

public class SetMinNode extends SetFunctionMethodInvocationBaseExprNode
{
	static {
		setName(SetMinNode.class, "set min");
	}

	public SetMinNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		SetTypeNode setType = getTargetType();
		if(!setType.valueType.isAccumulatableType()) {
			targetExpr.reportError("The set function method min can only be employed on an object of type set<" + TypeNode.getAccumulatableTypesAsString() + ">"
					+ " (but is employed on an object of type " + setType.getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType()
	{
		SetTypeNode setType = getTargetType();
		return BasicTypeNode.getArrayAccumulationResultType(setType.valueType);
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		return new SetMinExpr(targetExpr.checkIR(Expression.class));
	}
}
