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
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class VResetProc extends BuiltinProcedureInvocationBase
{
	private Expression visFlagExpr;

	public VResetProc(Expression stringExpr)
	{
		super("vreset procedure");
		this.visFlagExpr = stringExpr;
	}

	public Expression getVisitedFlagExpr()
	{
		return visFlagExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		visFlagExpr.collectNeededEntities(needs);
	}
}
