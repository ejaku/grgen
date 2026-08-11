/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.array
{
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using ArrayOrderDescending = de.unika.ipd.grgen.ir.expr.array.ArrayOrderDescending;
using IR = de.unika.ipd.grgen.ir.IR;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class ArrayOrderDescendingNode : ArrayFunctionMethodInvocationBaseExprNode
{
	static ArrayOrderDescendingNode()
	{
		SetClassName(typeof(ArrayOrderDescendingNode), "array order descending");
	}

	public ArrayOrderDescendingNode(Coords coords, ExprNode targetExpr)
		: base(coords, targetExpr)
	{
	}

	protected internal override bool CheckLocal()
	{
		// target type already checked during resolving into this node
		ArrayTypeNode arrayType = TargetTypeExact;
		if(!(arrayType.valueType.IsOrderableType()))
			targetExpr.ReportError("The array function method orderDescending can only be employed on an object of type array<" + TypeNode.OrderableTypesAsString + ">"
					+ " (but is employed on an object of type " + arrayType.TypeName + ").");
		return true;
	}

	public override TypeNode Type
	{
		get
		{
			return TargetType;
		}
	}

	protected internal override IR ConstructIR()
	{
		targetExpr = targetExpr.Evaluate();
		return new ArrayOrderDescending(targetExpr.CheckIR(typeof(Expression)));
	}
}

}
