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

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using MapPeekExpr = de.unika.ipd.grgen.ir.expr.map.MapPeekExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class MapPeekNode : MapFunctionMethodInvocationBaseExprNode
{
	static MapPeekNode()
	{
		SetClassName(typeof(MapPeekNode), "map peek");
	}

	private ExprNode numberExpr;

	public MapPeekNode(Coords coords, ExprNode targetExpr, ExprNode numberExpr)
		: base(coords, targetExpr)
	{
		this.numberExpr = BecomeParent(numberExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(targetExpr);
			children.Add(numberExpr);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("targetExpr");
			childrenNames.Add("numberExpr");
			return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		// target type already checked during resolving into this node
		if(!numberExpr.Type.IsEqual(BasicTypeNode.intType))
		{
			numberExpr.ReportError("The map function method peek expects as argument (number) a value of type int"
					+ " (but is given a value of type " + numberExpr.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	public override TypeNode Type
	{
		get
		{
			return TargetTypeExact.keyType;
		}
	}

	protected internal override IR ConstructIR()
	{
		targetExpr = targetExpr.Evaluate();
		numberExpr = numberExpr.Evaluate();
		return new MapPeekExpr(targetExpr.CheckIR(typeof(Expression)),
				numberExpr.CheckIR(typeof(Expression)));
	}
}

}
