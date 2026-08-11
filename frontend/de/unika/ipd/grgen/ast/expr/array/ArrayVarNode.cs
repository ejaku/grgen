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
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using ArrayVarExpr = de.unika.ipd.grgen.ir.expr.array.ArrayVarExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class ArrayVarNode : ArrayAccumulationMethodNode
{
	static ArrayVarNode()
	{
		SetClassName(typeof(ArrayVarNode), "array var");
	}

	public ArrayVarNode(Coords coords, ExprNode targetExpr)
		: base(coords, targetExpr)
	{
	}

	protected internal override bool CheckLocal()
	{
		// target type already checked during resolving into this node
		ArrayTypeNode arrayType = TargetTypeExact;
		if(!arrayType.valueType.IsAccumulatableType())
		{
			targetExpr.ReportError("The array function method var can only be employed on an object of type array<" + TypeNode.AccumulatableTypesAsString + ">"
					+ " (but is employed on an object of type " + arrayType.TypeName + ").");
			return false;
		}
		return true;
	}

	public override TypeNode Type
	{
		get
		{
		return BasicTypeNode.doubleType;
		}
	}

	public override bool IsValidTargetTypeOfAccumulation(TypeNode type)
	{
		return type.IsEqual(BasicTypeNode.doubleType);
	}

	public override string ValidTargetTypesOfAccumulation
	{
		get
		{
		return "double";
		}
	}

	protected internal override IR ConstructIR()
	{
		targetExpr = targetExpr.Evaluate();
		return new ArrayVarExpr(targetExpr.CheckIR(typeof(Expression)));
	}
}

}
