/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding the graph elements (nodes or edges) from an index by accessing using a comparison for equality (base class for the specific node or edge classes).
/// </summary>
public abstract class FromIndexAccessSameExprNode : FromIndexAccessExprNode
{
	static FromIndexAccessSameExprNode()
	{
		SetClassName(typeof(FromIndexAccessSameExprNode), "from index access same expr");
	}

	protected internal ExprNode expr;

	public FromIndexAccessSameExprNode(Coords coords, BaseNode index, ExprNode expr)
		: base(coords, index)
	{
		this.expr = expr;
		BecomeParent(this.expr);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(GetValidVersion(indexUnresolved, index));
		children.Add(expr);
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
		childrenNames.Add("index");
		childrenNames.Add("expr");
		return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = base.ResolveLocal();
		successfullyResolved &= expr.Resolve();
		return successfullyResolved;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		bool res = base.CheckLocal();
		TypeNode expectedIndexAccessType = index.ExpectedAccessType;
		TypeNode indexAccessType = expr.Type;
		if(!indexAccessType.IsCompatibleTo(expectedIndexAccessType))
		{
			string expTypeName = expectedIndexAccessType.TypeName;
			string typeName = indexAccessType.TypeName;
			int argumentNumber = 2 + IndexShift();
			ReportError("The function " + ShortSignature() + " expects as " + argumentNumber + ". argument (expr) a value of type " + expTypeName
					+ " (but is given a value of type " + typeName + ").");
			return false;
		}
		return res;
	}
}

}
