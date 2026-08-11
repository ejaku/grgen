/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using IntTypeNode = de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
using Coords = de.unika.ipd.grgen.parser.Coords;
using Direction = de.unika.ipd.grgen.util.Direction;

/// <summary>
/// Base class for bounded neighborhood graph queries (with members shared by all these queries).
/// </summary>
public abstract class BoundedNeighborhoodQueryExprNode : NeighborhoodQueryExprNode
{
	static BoundedNeighborhoodQueryExprNode()
	{
		SetClassName(typeof(BoundedNeighborhoodQueryExprNode), "bounded neighborhood query node expr");
	}

	protected internal ExprNode depthExpr;


	protected internal BoundedNeighborhoodQueryExprNode(Coords coords,
			ExprNode startNodeExpr, ExprNode depthExpr,
			ExprNode incidentTypeExpr, Direction direction,
			ExprNode adjacentTypeExpr)
		: base(coords, startNodeExpr, incidentTypeExpr, direction, adjacentTypeExpr)
	{
		this.depthExpr = depthExpr;
		BecomeParent(this.depthExpr);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(startNodeExpr);
			children.Add(depthExpr);
			children.Add(incidentTypeExpr);
			children.Add(adjacentTypeExpr);
			return children;
		}
	}

	/// <summary>
	/// returns names of the children, same order as in getChildren </summary>
	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("start node expr");
			childrenNames.Add("depth expr");
			childrenNames.Add("incident type expr");
			childrenNames.Add("adjacent type expr");
			return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		if(!(startNodeExpr.Type is NodeTypeNode))
		{
			ReportError("The function " + ShortSignature() + " expects as 1. argument a value of type node"
					+ " (but is given a value of type " + startNodeExpr.Type.TypeName + ").");
			return false;
		}
		if(!(depthExpr.Type is IntTypeNode))
		{
			ReportError("The function " + ShortSignature() + " expects as 2. argument a value of type int"
					+ " (but is given a value of type " + depthExpr.Type.TypeName + ").");
			return false;
		}
		if(!(incidentTypeExpr.Type is EdgeTypeNode))
		{
			ReportError("The function " + ShortSignature() + " expects as 3. argument a value of type edge type"
					+ " (but is given a value of type " + incidentTypeExpr.Type.TypeName + ").");
			return false;
		}
		if(!(adjacentTypeExpr.Type is NodeTypeNode))
		{
			ReportError("The function " + ShortSignature() + " expects as 4. argument a value of type node type"
					+ " (but is given a value of type " + adjacentTypeExpr.Type.TypeName + ").");
			return false;
		}
		return true;
	}
}

}
