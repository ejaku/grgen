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
using IntTypeNode = de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using MapAsArrayExpr = de.unika.ipd.grgen.ir.expr.map.MapAsArrayExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class MapAsArrayNode : MapFunctionMethodInvocationBaseExprNode
{
	static MapAsArrayNode()
	{
		SetClassName(typeof(MapAsArrayNode), "map as array expression");
	}

	private ArrayTypeNode arrayTypeNode;

	public MapAsArrayNode(Coords coords, ExprNode targetExpr)
		: base(coords, targetExpr)
	{
	}

	protected internal override bool ResolveLocal()
	{
		// target type already checked during resolving into this node
		arrayTypeNode = new ArrayTypeNode(TargetTypeExact.valueTypeUnresolved);
		return arrayTypeNode.Resolve();
	}

	protected internal override bool CheckLocal()
	{
		MapTypeNode targetMapType = TargetTypeExact;
		if(!(targetMapType.keyType is IntTypeNode))
		{
			targetExpr.ReportError("The map function method asArray can only be employed on an object of type map<int,T>"
					+ " (but is employed on an object of type " + targetMapType.TypeName + ").");
			return false;
		}
		return true;
	}

	public override TypeNode Type
	{
		get
		{
			return arrayTypeNode;
		}
	}

	protected internal override IR ConstructIR()
	{
		targetExpr = targetExpr.Evaluate();
		return new MapAsArrayExpr(targetExpr.CheckIR(typeof(Expression)), Type.IRType);
	}
}

}
