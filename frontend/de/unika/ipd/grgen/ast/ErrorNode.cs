/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ast
{

using System.Collections.Generic;

/// <summary>
/// Dummy AST node, that is used in the case of an error.
/// children: none
/// </summary>
public class ErrorNode : BaseNode
{
	static ErrorNode()
	{
		SetClassName(typeof(ErrorNode), "error node");
	}

	protected internal ErrorNode()
		: base()
	{
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

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		return true;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		return true;
	}

	public override Color NodeColor
	{
		get
		{
		return Color.RED;
		}
	}

	public override string NodeLabel
	{
		get
		{
		return "Error";
		}
	}

	public override sealed bool IsError()
	{
		return true;
	}
}

}
