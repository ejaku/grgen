/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt.procenv;

import java.util.Collection;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class DebugHighlightProc extends BuiltinProcedureInvocationBase
{
	private Collection<Expression> exprs;

	public DebugHighlightProc(Collection<Expression> expressions)
	{
		super("debug highlight procedure");
		this.exprs = expressions;
	}

	public Expression getFirstExpression()
	{
		for(Expression expr : exprs) {
			return expr;
		}
		return null;
	}

	public Collection<Expression> getExpressions()
	{
		return exprs;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		for(Expression expr : exprs) {
			expr.collectNeededEntities(needs);
		}
	}
}
