/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.graph
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using EmptyExpr = de.unika.ipd.grgen.ir.expr.graph.EmptyExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class EmptyExprNode : BuiltinFunctionInvocationBaseNode
{
	static EmptyExprNode()
	{
		SetClassName(typeof(EmptyExprNode), "empty expr");
	}

	public EmptyExprNode(Coords coords)
		: base(coords)
	{
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		return new EmptyExpr();
	}

	public override TypeNode Type
	{
		get
		{
			return BasicTypeNode.booleanType;
		}
	}
}

}
