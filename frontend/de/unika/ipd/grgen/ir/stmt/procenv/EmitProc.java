/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt.procenv;

import java.util.Collection;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class EmitProc extends BuiltinProcedureInvocationBase
{
	private Collection<Expression> exprs;
	private boolean isDebug;

	public EmitProc(Collection<Expression> expressions, boolean isDebug)
	{
		super("emit procedure");
		this.exprs = expressions;
		this.isDebug = isDebug;
	}

	public Collection<Expression> getExpressions()
	{
		return exprs;
	}

	public boolean isDebug()
	{
		return isDebug;
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
