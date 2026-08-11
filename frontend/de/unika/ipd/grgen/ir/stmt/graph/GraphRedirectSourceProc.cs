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

public class GraphRedirectSourceProc : BuiltinProcedureInvocationBase
{
	private Expression edge;
	private Expression newSource;
	private Expression oldSourceName; // optional

	public GraphRedirectSourceProc(Expression edge, Expression newSource, Expression oldSourceName)
		: base("graph redirect source procedure")
	{
		this.edge = edge;
		this.newSource = newSource;
		this.oldSourceName = oldSourceName;
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

	public virtual Expression OldSourceName
	{
		get
		{
			return oldSourceName;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.NeedsGraph();
		edge.CollectNeededEntities(needs);
		newSource.CollectNeededEntities(needs);
	}
}

}
