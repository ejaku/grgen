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
using StringReplace = de.unika.ipd.grgen.ir.expr.@string.StringReplace;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class StringReplaceNode : BuiltinFunctionInvocationBaseNode
{
	static StringReplaceNode()
	{
		SetClassName(typeof(StringReplaceNode), "string replace");
	}

	private ExprNode stringExpr;
	private ExprNode startExpr;
	private ExprNode lengthExpr;
	private ExprNode replaceStrExpr;

	public StringReplaceNode(Coords coords, ExprNode stringExpr,
			ExprNode startExpr, ExprNode lengthExpr, ExprNode replaceStrExpr)
		: base(coords)
	{

		this.stringExpr = BecomeParent(stringExpr);
		this.startExpr = BecomeParent(startExpr);
		this.lengthExpr = BecomeParent(lengthExpr);
		this.replaceStrExpr = BecomeParent(replaceStrExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(stringExpr);
			children.Add(startExpr);
			children.Add(lengthExpr);
			children.Add(replaceStrExpr);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("string");
			childrenNames.Add("start");
			childrenNames.Add("length");
			childrenNames.Add("replaceStrExpr");
			return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		if(!stringExpr.Type.IsEqual(BasicTypeNode.stringType))
		{
			stringExpr.ReportError("The string function method replace can only be employed on an object of type string"
					+ " (but is employed on an object of type " + stringExpr.Type.TypeName + ").");
			return false;
		}
		if(!startExpr.Type.IsEqual(BasicTypeNode.intType))
		{
			startExpr.ReportError("The string function method replace expects as 1. argument (startPosition) a value of type int"
					+ " (but is given a value of type " + startExpr.Type.TypeName + ").");
			return false;
		}
		if(!lengthExpr.Type.IsEqual(BasicTypeNode.intType))
		{
			lengthExpr.ReportError("The string function method replace expects as 2. argument (length) a value of type int"
					+ " (but is given a value of type " + lengthExpr.Type.TypeName + ").");
			return false;
		}
		if(!replaceStrExpr.Type.IsEqual(BasicTypeNode.stringType))
		{
			replaceStrExpr.ReportError("The string function method replace expects as 3. argument (replacementString) a value of type string"
					+ " (but is given a value of type " + replaceStrExpr.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		stringExpr = stringExpr.Evaluate();
		startExpr = startExpr.Evaluate();
		lengthExpr = lengthExpr.Evaluate();
		replaceStrExpr = replaceStrExpr.Evaluate();
		return new StringReplace(stringExpr.CheckIR(typeof(Expression)),
				startExpr.CheckIR(typeof(Expression)),
				lengthExpr.CheckIR(typeof(Expression)),
				replaceStrExpr.CheckIR(typeof(Expression)));
	}

	public override TypeNode Type
	{
		get
		{
			return BasicTypeNode.stringType;
		}
	}
}

}
