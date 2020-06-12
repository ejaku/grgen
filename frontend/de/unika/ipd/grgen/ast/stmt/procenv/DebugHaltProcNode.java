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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugHaltProc;
import de.unika.ipd.grgen.parser.Coords;

public class DebugHaltProcNode extends DebugProcNode
{
	static {
		setName(DebugHaltProcNode.class, "debug halt procedure");
	}

	public DebugHaltProcNode(Coords coords)
	{
		super(coords);
	}

	@Override
	protected String shortSignature()
	{
		return "Debug::halt()";
	}

	@Override
	protected IR constructIR()
	{
		Vector<Expression> expressions = new Vector<Expression>();
		for(ExprNode expr : exprs.getChildren()) {
			expr = expr.evaluate();
			expressions.add(expr.checkIR(Expression.class));
		}
		return new DebugHaltProc(expressions);
	}
}
