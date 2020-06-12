/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.procenv;

import java.util.Vector;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugHighlightProc;
import de.unika.ipd.grgen.parser.Coords;

public class DebugHighlightProcNode extends DebugProcNode
{
	static {
		setName(DebugHighlightProcNode.class, "debug highlight procedure");
	}

	public DebugHighlightProcNode(Coords coords)
	{
		super(coords);
	}

	@Override
	protected boolean checkLocal()
	{
		int paramNum = 0;
		for(ExprNode expr : exprs.getChildren()) {
			if(paramNum % 2 == 0 && !(expr.getType().equals(BasicTypeNode.stringType))) {
				reportError("argument " + paramNum
						+ " of " + shortSignature() + " must be of string type (a message followed by a sequence of (value, annotation for the value)* must be given)");
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
		Vector<Expression> expressions = new Vector<Expression>();
		for(ExprNode expr : exprs.getChildren()) {
			expr = expr.evaluate();
			expressions.add(expr.checkIR(Expression.class));
		}
		return new DebugHighlightProc(expressions);
	}
}
