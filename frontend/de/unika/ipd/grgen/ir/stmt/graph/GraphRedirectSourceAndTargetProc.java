/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class GraphRedirectSourceAndTargetProc extends BuiltinProcedureInvocationBase
{
	private Expression edge;
	private Expression newSource;
	private Expression newTarget;
	private Expression oldSourceName; // optional
	private Expression oldTargetName; // optional

	public GraphRedirectSourceAndTargetProc(Expression edge, Expression newSource, Expression newTarget,
			Expression oldSourceName, Expression oldTargetName)
	{
		super("graph redirect source and target procedure");
		this.edge = edge;
		this.newSource = newSource;
		this.newTarget = newTarget;
		this.oldSourceName = oldSourceName;
		this.oldTargetName = oldTargetName;
	}

	public Expression getEdge()
	{
		return edge;
	}

	public Expression getNewSource()
	{
		return newSource;
	}

	public Expression getNewTarget()
	{
		return newTarget;
	}

	public Expression getOldSourceName()
	{
		return oldSourceName;
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
		newSource.collectNeededEntities(needs);
		newTarget.collectNeededEntities(needs);
	}
}
