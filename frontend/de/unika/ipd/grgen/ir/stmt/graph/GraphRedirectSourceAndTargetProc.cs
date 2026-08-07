/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.stmt.graph
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using BuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class GraphRedirectSourceAndTargetProc : BuiltinProcedureInvocationBase
{
	private Expression edge;
	private Expression newSource;
	private Expression newTarget;
	private Expression oldSourceName; // optional
	private Expression oldTargetName; // optional

	public GraphRedirectSourceAndTargetProc(Expression edge, Expression newSource, Expression newTarget,
			Expression oldSourceName, Expression oldTargetName)
		: base("graph redirect source and target procedure")
	{
		this.edge = edge;
		this.newSource = newSource;
		this.newTarget = newTarget;
		this.oldSourceName = oldSourceName;
		this.oldTargetName = oldTargetName;
	}

	public virtual Expression Edge
	{
		get
		{
		return edge;
		}
	}

	public virtual Expression NewSource
	{
		get
		{
		return newSource;
		}
	}

	public virtual Expression NewTarget
	{
		get
		{
		return newTarget;
		}
	}

	public virtual Expression OldSourceName
	{
		get
		{
		return oldSourceName;
		}
	}

	public virtual Expression OldTargetName
	{
		get
		{
		return oldTargetName;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.NeedsGraph();
		edge.CollectNeededEntities(needs);
		newSource.CollectNeededEntities(needs);
		newTarget.CollectNeededEntities(needs);
	}
}

}
