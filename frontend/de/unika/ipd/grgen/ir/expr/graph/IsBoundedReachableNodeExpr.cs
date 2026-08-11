/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr.graph
{
using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Type = de.unika.ipd.grgen.ir.type.Type;
using Direction = de.unika.ipd.grgen.util.Direction;

public class IsBoundedReachableNodeExpr : NeighborhoodQueryExpr
{
	private readonly Expression endNodeExpr;
	private readonly Expression depthExpr;

	public IsBoundedReachableNodeExpr(Expression startNodeExpression,
			Expression endNodeExpression, Expression depthExpression,
			Expression incidentEdgeTypeExpr, Direction direction,
			Expression adjacentNodeTypeExpr, Type type)
		: base("is bouneded reachable node expression", type, startNodeExpression, incidentEdgeTypeExpr, direction, adjacentNodeTypeExpr)
	{
		this.endNodeExpr = endNodeExpression;
		this.depthExpr = depthExpression;
	}

	public virtual Expression EndNodeExpr
	{
		get
		{
		return endNodeExpr;
		}
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
		endNodeExpr.CollectNeededEntities(needs);
		depthExpr.CollectNeededEntities(needs);
		incidentEdgeTypeExpr.CollectNeededEntities(needs);
		adjacentNodeTypeExpr.CollectNeededEntities(needs);
	}
}

}
