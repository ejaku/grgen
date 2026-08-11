/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.type.basic
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using VoidType = de.unika.ipd.grgen.ir.type.basic.VoidType;

/// <summary>
/// The error basic type. It is compatible to no other type.
/// TODO: Why compatible to no other type? The error node within an compiler
/// should be compatible to every other node, to protect against error avalanches
/// </summary>
public class ErrorTypeNode : TypeNode
{
	static ErrorTypeNode()
	{
		SetClassName(typeof(ErrorTypeNode), "error type");
	}

	private IdentNode id;

	public ErrorTypeNode(IdentNode id)
	{
		this.id = id;
		Coords = id.Coords;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			// no children
			return children;
		}
	}

	/// <summary>
	/// returns names of the children, same order as in getChildren </summary>
	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			// no children
			return childrenNames;
		}
	}

	protected internal override IR ConstructIR()
	{
		return new VoidType(id.IRIdent);
	}

	public static string KindStr
	{
		get
		{
			return "error type";
		}
	}

	public override string ToString()
	{
		return "error type";
	}
}

}
