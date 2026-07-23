/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.procenv;

import java.util.ArrayList;
import java.util.List;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugHighlightProc;
import de.unika.ipd.grgen.parser.Coords;

public class DebugHighlightProcNode extends DebugProcNode
{
	static {
		setClassName(DebugHighlightProcNode.class, "debug highlight procedure");
	}

	public DebugHighlightProcNode(Coords coords)
	{
		super(coords);
	}

	@Override
	protected boolean checkLocal()
	{
		int paramNum = 0;
		for(ExprNode expr : exprs.getChildrenExact()) {
			TypeNode exprType = expr.getType();
			if(paramNum % 2 == 0 && !(exprType.equals(BasicTypeNode.stringType))) {
				reportError("The " + shortSignature() + " procedure expects as " + paramNum + ". argument"
						+ " a value of type string (a message followed by a sequence of (value, annotation for the value)* must be given)"
						+ " (but is given a value of type " + exprType.toStringWithDeclarationCoords() + ").");
				return false;
			}
			++paramNum;
		}
		return true;
	}

	@Override
	protected String shortSignature()
	{
		return "Debug::highlight()";
	}

	@Override
	protected IR constructIR()
	{
		List<Expression> expressions = new ArrayList<Expression>();
		for(ExprNode expr : exprs.getChildrenExact()) {
			expr = expr.evaluate();
			expressions.add(expr.checkIR(Expression.class));
		}
		return new DebugHighlightProc(expressions);
	}
}
