/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr.graph
{
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using BuiltinFunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
using Type = de.unika.ipd.grgen.ir.type.Type;
using Direction = de.unika.ipd.grgen.util.Direction;

public abstract class NeighborhoodQueryExpr : BuiltinFunctionInvocationExpr
{
	protected internal readonly Expression startNodeExpr;
	protected internal readonly Expression incidentEdgeTypeExpr;
	protected internal readonly Direction direction;
	protected internal readonly Expression adjacentNodeTypeExpr;

	public NeighborhoodQueryExpr(string name, Type type,
			Expression startNodeExpression,
			Expression incidentEdgeTypeExpr, Direction direction,
			Expression adjacentNodeTypeExpr)
		: base(name, type)
	{
		this.startNodeExpr = startNodeExpression;
		this.incidentEdgeTypeExpr = incidentEdgeTypeExpr;
		this.direction = direction;
		this.adjacentNodeTypeExpr = adjacentNodeTypeExpr;
	}

	public virtual Expression StartNodeExpr
	{
		get
		{
			return startNodeExpr;
		}
	}

	public virtual Expression IncidentEdgeTypeExpr
	{
		get
		{
			return incidentEdgeTypeExpr;
		}
	}

	public virtual Direction Direction()
	{
		return direction;
	}

	public virtual Expression AdjacentNodeTypeExpr
	{
		get
		{
			return adjacentNodeTypeExpr;
		}
	}
}

}
