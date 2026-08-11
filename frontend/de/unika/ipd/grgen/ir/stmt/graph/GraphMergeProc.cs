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

public class GraphMergeProc : BuiltinProcedureInvocationBase
{
	private Expression target;
	private Expression source;
	private Expression sourceName;

	public GraphMergeProc(Expression target, Expression source, Expression sourceName)
		: base("graph merge procedure")
	{
		this.target = target;
		this.source = source;
		this.sourceName = sourceName;
	}

	public virtual Expression Target
	{
		get
		{
			return target;
		}
	}

	public virtual Expression Source
	{
		get
		{
			return source;
		}
	}

	public virtual Expression SourceName
	{
		get
		{
			return sourceName;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.NeedsGraph();
		target.CollectNeededEntities(needs);
		source.CollectNeededEntities(needs);
	}
}

}
