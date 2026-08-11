/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.@string
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using StringTypeNode = de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using StringAsArray = de.unika.ipd.grgen.ir.expr.@string.StringAsArray;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class StringAsArrayNode : BuiltinFunctionInvocationBaseNode
{
	static StringAsArrayNode()
	{
		SetClassName(typeof(StringAsArrayNode), "string asArray");
	}

	private ExprNode stringExpr;
	private ExprNode stringToSplitAtExpr;
	private ArrayTypeNode arrayTypeNode;

	public StringAsArrayNode(Coords coords, ExprNode stringExpr, ExprNode stringToSplitAtExpr)
		: base(coords)
	{

		this.stringExpr = BecomeParent(stringExpr);
		this.stringToSplitAtExpr = BecomeParent(stringToSplitAtExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(stringExpr);
			children.Add(stringToSplitAtExpr);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("string");
			childrenNames.Add("stringToSplitAt");
			return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		arrayTypeNode = new ArrayTypeNode(((StringTypeNode)stringExpr.Type).Ident);
		return arrayTypeNode.Resolve();
	}

	protected internal override bool CheckLocal()
	{
		if(!stringExpr.Type.IsEqual(BasicTypeNode.stringType))
		{
			stringExpr.ReportError("The string function method explode can only be employed on an object of type string"
					+ " (but is employed on an object of type " + stringExpr.Type.TypeName + ").");
			return false;
		}
		if(!stringToSplitAtExpr.Type.IsEqual(BasicTypeNode.stringType))
		{
			stringToSplitAtExpr.ReportError("The string function method explode expects as argument (stringToSplitAt) a value of type string"
					+ " (but is given a value of type " + stringToSplitAtExpr.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		stringExpr = stringExpr.Evaluate();
		stringToSplitAtExpr = stringToSplitAtExpr.Evaluate();
		return new StringAsArray(stringExpr.CheckIR(typeof(Expression)),
				stringToSplitAtExpr.CheckIR(typeof(Expression)),
				Type.IRType);
	}

	public override TypeNode Type
	{
		get
		{
			return arrayTypeNode;
		}
	}
}

}
