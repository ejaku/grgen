/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.map
{
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using MapRangeExpr = de.unika.ipd.grgen.ir.expr.map.MapRangeExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class MapRangeNode : MapFunctionMethodInvocationBaseExprNode
{
	static MapRangeNode()
	{
		SetClassName(typeof(MapSizeNode), "map range expression");
	}

	private SetTypeNode setTypeNode;

	public MapRangeNode(Coords coords, ExprNode targetExpr)
		: base(coords, targetExpr)
	{
	}

	protected internal override bool ResolveLocal()
	{
		// target type already checked during resolving into this node
		setTypeNode = new SetTypeNode(TargetTypeExact.valueTypeUnresolved);
		return setTypeNode.Resolve();
	}

	public override TypeNode Type
	{
		get
		{
		return setTypeNode;
		}
	}

	protected internal override IR ConstructIR()
	{
		targetExpr = targetExpr.Evaluate();
		return new MapRangeExpr(targetExpr.CheckIR(typeof(Expression)), Type.IRType);
	}
}

}
