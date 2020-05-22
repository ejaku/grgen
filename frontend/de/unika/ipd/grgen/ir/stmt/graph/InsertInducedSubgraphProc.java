/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.ProcedureBase;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.ProcedureInvocationBase;

public class InsertInducedSubgraphProc extends ProcedureInvocationBase
{
	private final Expression nodeSetExpr;
	private final Expression nodeExpr;

	public InsertInducedSubgraphProc(Expression nodeSetExpr, Expression nodeExpr)
	{
		super("insert induced subgraph procedure");
		this.nodeSetExpr = nodeSetExpr;
		this.nodeExpr = nodeExpr;
	}

	public Expression getSetExpr()
	{
		return nodeSetExpr;
	}

	public Expression getNodeExpr()
	{
		return nodeExpr;
	}

	public ProcedureBase getProcedureBase()
	{
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		nodeSetExpr.collectNeededEntities(needs);
		nodeExpr.collectNeededEntities(needs);
	}
}
