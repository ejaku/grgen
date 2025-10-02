/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.procenv;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class SynchronizationExitProc extends BuiltinProcedureInvocationBase
{
	private Expression criticalSectionObjectExpr;

	public SynchronizationExitProc(Expression criticalSectionObjectExpr)
	{
		super("synchronization exit procedure");
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
}
