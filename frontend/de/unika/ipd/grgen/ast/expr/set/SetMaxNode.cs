/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.set
{
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using SetMaxExpr = de.unika.ipd.grgen.ir.expr.set.SetMaxExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class SetMaxNode : SetFunctionMethodInvocationBaseExprNode
{
	static SetMaxNode()
	{
		SetClassName(typeof(SetMaxNode), "set max");
	}

	public SetMaxNode(Coords coords, ExprNode targetExpr)
		: base(coords, targetExpr)
	{
	}

	protected internal override bool CheckLocal()
	{
		// target type already checked during resolving into this node
		SetTypeNode setType = TargetTypeExact;
		if(!setType.valueType.IsAccumulatableType())
		{
			targetExpr.ReportError("The set function method max can only be employed on an object of type set<" + TypeNode.AccumulatableTypesAsString + ">"
					+ " (but is employed on an object of type " + setType.TypeName + ").");
			return false;
		}
		return true;
	}

	public override TypeNode Type
	{
		get
		{
		SetTypeNode setType = TargetTypeExact;
		return BasicTypeNode.GetArrayAccumulationResultType(setType.valueType);
		}
	}

	protected internal override IR ConstructIR()
	{
		targetExpr = targetExpr.Evaluate();
		return new SetMaxExpr(targetExpr.CheckIR(typeof(Expression)));
	}
}

}
