/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.executable.ProcedureBase;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.invocation.ProcedureOrBuiltinProcedureInvocationBase;

public class VFreeProc extends ProcedureOrBuiltinProcedureInvocationBase
{
	private Expression visFlagExpr;

	public VFreeProc(Expression stringExpr)
	{
		super("vfree procedure");
		this.visFlagExpr = stringExpr;
	}

	public Expression getVisitedFlagExpr()
	{
		return visFlagExpr;
	}

	public ProcedureBase getProcedureBase()
	{
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		visFlagExpr.collectNeededEntities(needs);
	}
}
