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
using ReachableNodeExpr = de.unika.ipd.grgen.ir.expr.graph.ReachableNodeExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;
using Direction = de.unika.ipd.grgen.util.Direction;

/// <summary>
/// A node yielding the reachable nodes of a node, via incident edges, via incoming edges, via outgoing edges.
/// </summary>
public class ReachableNodeExprNode : NeighborhoodQueryExprNode
{
	static ReachableNodeExprNode()
	{
		SetClassName(typeof(ReachableNodeExprNode), "reachable node expr");
	}

	private SetTypeNode setTypeNode;


	public ReachableNodeExprNode(Coords coords,
			ExprNode startNodeExpr,
			ExprNode incidentTypeExpr, Direction direction,
			ExprNode adjacentTypeExpr)
		: base(coords, startNodeExpr, incidentTypeExpr, direction, adjacentTypeExpr)
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
		return "reachableNodes(.,.,.)";
	}

	protected internal override IR ConstructIR()
	{
		startNodeExpr = startNodeExpr.Evaluate();
		incidentTypeExpr = incidentTypeExpr.Evaluate();
		adjacentTypeExpr = adjacentTypeExpr.Evaluate();
		// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
		return new ReachableNodeExpr(startNodeExpr.CheckIR(typeof(Expression)),
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
