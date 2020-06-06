/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

public class ExportProc extends BuiltinProcedureInvocationBase
{
	private Expression pathExpr;
	private Expression graphExpr;

	public ExportProc(Expression pathExpr, Expression graphExpr)
	{
		super("export procedure");
		this.pathExpr = pathExpr;
		this.graphExpr = graphExpr;
	}

	public Expression getPathExpr()
	{
		return pathExpr;
	}

	public Expression getGraphExpr()
	{
		return graphExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		pathExpr.collectNeededEntities(needs);
		if(graphExpr != null)
			graphExpr.collectNeededEntities(needs);
	}
}
