/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.procenv;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;
import de.unika.ipd.grgen.ir.type.Type;

public class SynchronizationTryEnterProc extends BuiltinProcedureInvocationBase
{
	Type returnType;
	
	private Expression criticalSectionObjectExpr;

	public SynchronizationTryEnterProc(Type returnType, Expression criticalSectionObjectExpr)
	{
		super("synchronization try enter procedure");
		this.returnType = returnType;
		this.criticalSectionObjectExpr = criticalSectionObjectExpr;
	}

	public Expression getCriticalSectionObject()
	{
		return criticalSectionObjectExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		criticalSectionObjectExpr.collectNeededEntities(needs);
	}
	
	@Override
	public int returnArity()
	{
		return 1;
	}
	
	@Override
	public Type getReturnType(int index)
	{
		assert(index == 0);
		return returnType;
	}
}