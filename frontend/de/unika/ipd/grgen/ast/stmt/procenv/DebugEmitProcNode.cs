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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugEmitProc;
import de.unika.ipd.grgen.parser.Coords;

public class DebugEmitProcNode extends DebugProcNode
{
	static {
		setClassName(DebugEmitProcNode.class, "debug emit procedure");
	}

	public DebugEmitProcNode(Coords coords)
	{
		super(coords);
	}

	@Override
	protected String shortSignature()
	{
		return "Debug::emit()";
	}

	@Override
	protected IR constructIR()
	{
		List<Expression> expressions = new ArrayList<Expression>();
		for(ExprNode expr : exprs.getChildrenExact()) {
			ExprNode exprEvaluated = expr.evaluate();
			expressions.add(exprEvaluated.checkIR(Expression.class));
		}
		return new DebugEmitProc(expressions);
	}
}
