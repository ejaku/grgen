/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using BoundedReachableNodeExpr = de.unika.ipd.grgen.ir.expr.graph.BoundedReachableNodeExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;
using Direction = de.unika.ipd.grgen.util.Direction;

/// <summary>
/// A node yielding the depth-bounded reachable nodes/reachable nodes via incoming edges/reachable nodes via outgoing edges of a node.
/// </summary>
public class BoundedReachableNodeExprNode : BoundedNeighborhoodQueryExprNode
{
	static BoundedReachableNodeExprNode()
	{
		SetClassName(typeof(BoundedReachableNodeExprNode), "bounded reachable node expr");
	}

	private SetTypeNode setTypeNode;


	public BoundedReachableNodeExprNode(Coords coords,
			ExprNode startNodeExpr, ExprNode depthExpr,
			ExprNode incidentTypeExpr, Direction direction,
			ExprNode adjacentTypeExpr)
		: base(coords, startNodeExpr, depthExpr, incidentTypeExpr, direction, adjacentTypeExpr)
	{
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		setTypeNode = new SetTypeNode(GetNodeRoot(adjacentTypeExpr));
		return setTypeNode.Resolve();
	}

	protected internal override string ShortSignature()
	{
		return "boundedReachable(.,.,.,.)";
	}

	protected internal override IR ConstructIR()
	{
		startNodeExpr = startNodeExpr.Evaluate();
		depthExpr = depthExpr.Evaluate();
		incidentTypeExpr = incidentTypeExpr.Evaluate();
		adjacentTypeExpr = adjacentTypeExpr.Evaluate();
		// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
		return new BoundedReachableNodeExpr(startNodeExpr.CheckIR(typeof(Expression)),
				depthExpr.CheckIR(typeof(Expression)),
				incidentTypeExpr.CheckIR(typeof(Expression)), direction,
				adjacentTypeExpr.CheckIR(typeof(Expression)),
				Type.IRType);
	}

	public override TypeNode Type
	{
		get
		{
			return setTypeNode;
		}
	}
}

}
