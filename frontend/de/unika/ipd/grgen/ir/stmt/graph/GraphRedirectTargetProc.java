/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class GraphRedirectTargetProc extends BuiltinProcedureInvocationBase
{
	private Expression edge;
	private Expression newTarget;
	private Expression oldTargetName; // optional

	public GraphRedirectTargetProc(Expression edge, Expression newTarget, Expression oldTargetName)
	{
		super("graph redirect target procedure");
		this.edge = edge;
		this.newTarget = newTarget;
		this.oldTargetName = oldTargetName;
	}

	public Expression getEdge()
	{
		return edge;
	}

	public Expression getNewTarget()
	{
		return newTarget;
	}

	public Expression getOldTargetName()
	{
		return oldTargetName;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		edge.collectNeededEntities(needs);
		newTarget.collectNeededEntities(needs);
	}
}
