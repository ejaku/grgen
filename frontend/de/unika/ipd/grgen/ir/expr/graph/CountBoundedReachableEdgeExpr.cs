/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr.graph
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using IntType = de.unika.ipd.grgen.ir.type.basic.IntType;
using Direction = de.unika.ipd.grgen.util.Direction;

public class CountBoundedReachableEdgeExpr : NeighborhoodQueryExpr
{
	private readonly Expression depthExpr;

	public CountBoundedReachableEdgeExpr(Expression startNodeExpression, Expression depthExpression,
			Expression incidentEdgeTypeExpr, Direction direction,
			Expression adjacentNodeTypeExpr)
		: base("count bounded reachable edge expression", IntType.Type, startNodeExpression, incidentEdgeTypeExpr, direction, adjacentNodeTypeExpr)
	{
		this.depthExpr = depthExpression;
	}

	public virtual Expression DepthExpr
	{
		get
		{
			return depthExpr;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.NeedsGraph();
		startNodeExpr.CollectNeededEntities(needs);
		depthExpr.CollectNeededEntities(needs);
		incidentEdgeTypeExpr.CollectNeededEntities(needs);
		adjacentNodeTypeExpr.CollectNeededEntities(needs);
	}
}

}
