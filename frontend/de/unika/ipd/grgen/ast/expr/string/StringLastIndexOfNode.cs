/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.@string
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using StringLastIndexOf = de.unika.ipd.grgen.ir.expr.@string.StringLastIndexOf;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class StringLastIndexOfNode : BuiltinFunctionInvocationBaseNode
{
	static StringLastIndexOfNode()
	{
		SetClassName(typeof(StringLastIndexOfNode), "string lastIndexOf");
	}

	private ExprNode stringExpr;
	private ExprNode stringToSearchForExpr;
	private ExprNode startIndexExpr;

	public StringLastIndexOfNode(Coords coords, ExprNode stringExpr, ExprNode stringToSearchForExpr)
		: base(coords)
	{

		this.stringExpr = BecomeParent(stringExpr);
		this.stringToSearchForExpr = BecomeParent(stringToSearchForExpr);
	}

	public StringLastIndexOfNode(Coords coords, ExprNode stringExpr, ExprNode stringToSearchForExpr,
			ExprNode startIndexExpr)
		: base(coords)
	{

		this.stringExpr = BecomeParent(stringExpr);
		this.stringToSearchForExpr = BecomeParent(stringToSearchForExpr);
		this.startIndexExpr = BecomeParent(startIndexExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(stringExpr);
		children.Add(stringToSearchForExpr);
		if(startIndexExpr != null)
			children.Add(startIndexExpr);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("string");
		childrenNames.Add("stringToSearchFor");
		if(startIndexExpr != null)
			childrenNames.Add("startIndex");
		return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		if(!stringExpr.Type.IsEqual(BasicTypeNode.stringType))
		{
			stringExpr.ReportError("The string function method lastIndexOf can only be employed on an object of type string"
					+ " (but is employed on an object of type " + stringExpr.Type.TypeName + ").");
			return false;
		}
		if(!stringToSearchForExpr.Type.IsEqual(BasicTypeNode.stringType))
		{
			stringToSearchForExpr.ReportError("The string function method lastIndexOf expects as 1. argument (stringToSearchFor) a value of type string"
					+ " (but is given a value of type " + stringToSearchForExpr.Type.TypeName + ").");
			return false;
		}
		if(startIndexExpr != null
				&& !startIndexExpr.Type.IsEqual(BasicTypeNode.intType))
		{
			startIndexExpr.ReportError("The string function method lastIndexOf expects as 2. argument (startIndex) a value of type int"
					+ " (but is given a value of type " + startIndexExpr.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		stringExpr = stringExpr.Evaluate();
		stringToSearchForExpr = stringToSearchForExpr.Evaluate();
		if(startIndexExpr != null)
		{
			startIndexExpr = startIndexExpr.Evaluate();
			return new StringLastIndexOf(stringExpr.CheckIR(typeof(Expression)),
					stringToSearchForExpr.CheckIR(typeof(Expression)),
					startIndexExpr.CheckIR(typeof(Expression)));
		}
		else
		{
			return new StringLastIndexOf(stringExpr.CheckIR(typeof(Expression)),
					stringToSearchForExpr.CheckIR(typeof(Expression)));
		}
	}

	public override TypeNode Type
	{
		get
		{
		return BasicTypeNode.intType;
		}
	}
}

}
