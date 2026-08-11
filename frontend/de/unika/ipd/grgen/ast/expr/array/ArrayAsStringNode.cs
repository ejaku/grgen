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

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using StringTypeNode = de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using ArrayAsString = de.unika.ipd.grgen.ir.expr.array.ArrayAsString;
using IR = de.unika.ipd.grgen.ir.IR;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class ArrayAsStringNode : ArrayFunctionMethodInvocationBaseExprNode
{
	static ArrayAsStringNode()
	{
		SetClassName(typeof(ArrayAsStringNode), "array asString");
	}

	private ExprNode valueExpr;

	public ArrayAsStringNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr)
		: base(coords, targetExpr)
	{
		this.valueExpr = BecomeParent(valueExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(targetExpr);
		children.Add(valueExpr);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("targetExpr");
		childrenNames.Add("valueExpr");
		return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		// target type already checked during resolving into this node
		targetExpr.GetType().Resolve(); // call to ensure the array type exists
		return true;
	}

	protected internal override bool CheckLocal()
	{
		ArrayTypeNode arrayMemberType = TargetTypeExact;
		if(!(arrayMemberType.valueType is StringTypeNode))
		{
			targetExpr.ReportError("The array function method asString can only be employed on an object of type array<string>"
					+ " (but is employed on an object of type " + arrayMemberType.TypeName + ").");
			return false;
		}
		TypeNode valueType = valueExpr.Type;
		if(!valueType.IsEqual(BasicTypeNode.stringType))
		{
			valueExpr.ReportError("The array function method asString expects as argument a value of type string"
					+ " (but is given a value of type " + valueType.TypeName + ").");
			return false;
		}
		return true;
	}

	public override TypeNode Type
	{
		get
		{
		return BasicTypeNode.stringType;
		}
	}

	protected internal override IR ConstructIR()
	{
		targetExpr = targetExpr.Evaluate();
		return new ArrayAsString(targetExpr.CheckIR(typeof(Expression)), valueExpr.CheckIR(typeof(Expression)));
	}
}

}
