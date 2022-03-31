/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt.procenv;

import java.util.Collection;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class AssertProc extends BuiltinProcedureInvocationBase
{
	private Collection<Expression> exprs;
	private boolean isAlways;

	public AssertProc(Collection<Expression> expressions, boolean isAlways)
	{
		super("assert procedure");
		this.exprs = expressions;
		this.isAlways = isAlways;
	}

	public Collection<Expression> getExpressions()
	{
		return exprs;
	}

	public boolean isAlways()
	{
		return isAlways;
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
