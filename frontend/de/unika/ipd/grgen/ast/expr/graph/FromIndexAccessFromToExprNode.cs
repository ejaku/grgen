/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{

using System.Collections.Generic;
using System.Text;

using de.unika.ipd.grgen.ast;
using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding the graph elements (nodes or edges) from an index by accessing a range from a certain value to a certain value (one or both may be optional) (base class for the specific node or edge versions).
/// </summary>
public abstract class FromIndexAccessFromToExprNode : FromIndexAccessExprNode
{
	static FromIndexAccessFromToExprNode()
	{
		SetClassName(typeof(FromIndexAccessFromToExprNode), "from index access from to expr");
	}

	protected internal ExprNode fromExpr;
	protected internal bool fromExclusive;
	protected internal ExprNode toExpr;
	protected internal bool toExclusive;

	public FromIndexAccessFromToExprNode(Coords coords, BaseNode index, ExprNode fromExpr, bool fromExclusive, ExprNode toExpr, bool toExclusive)
		: base(coords, index)
	{
		this.fromExpr = fromExpr;
		BecomeParent(this.fromExpr);
		this.fromExclusive = fromExclusive;
		this.toExpr = toExpr;
		BecomeParent(this.toExpr);
		this.toExclusive = toExclusive;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(GetValidVersion(indexUnresolved, index));
			if(fromExpr != null)
				children.Add(fromExpr);
			if(toExpr != null)
				children.Add(toExpr);
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
			if(fromExpr != null)
				childrenNames.Add("fromExpr");
			if(toExpr != null)
				childrenNames.Add("toExpr");
			return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = base.ResolveLocal();
		if(fromExpr != null)
			successfullyResolved &= fromExpr.Resolve();
		if(toExpr != null)
			successfullyResolved &= toExpr.Resolve();
		return successfullyResolved;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		bool res = base.CheckLocal();
		TypeNode expectedIndexAccessType = index.ExpectedAccessType;
		if(fromExpr != null)
		{
			TypeNode fromIndexAccessType = fromExpr.Type;
			if(!fromIndexAccessType.IsCompatibleTo(expectedIndexAccessType))
			{
				string expTypeName = expectedIndexAccessType.TypeName;
				string typeName = fromIndexAccessType.TypeName;
				int fromArgumentNumber = 2 + IndexShift();
				ReportError("The function " + ShortSignature() + " expects as " + fromArgumentNumber + ". argument (fromExpr) a value of type " + expTypeName
						+ " (but is given a value of type " + typeName + ").");
				return false;
			}
		}
		if(toExpr != null)
		{
			TypeNode toIndexAccessType = toExpr.Type;
			if(!toIndexAccessType.IsCompatibleTo(expectedIndexAccessType))
			{
				string expTypeName = expectedIndexAccessType.TypeName;
				string typeName = toIndexAccessType.TypeName;
				int toArgumentNumber = (fromExpr != null ? 3 : 2) + IndexShift();
				ReportError("The function " + ShortSignature() + " expects as " + toArgumentNumber + ". argument (toExpr) a value of type " + expTypeName
						+ " (but is given a value of type " + typeName + ").");
				return false;
			}
		}
		return res;
	}

	protected internal virtual string FromPart()
	{
		if(fromExpr == null)
			return "";
		return fromExclusive ? "FromExclusive" : "From";
	}

	protected internal virtual string ToPart()
	{
		if(toExpr == null)
			return "";
		return toExclusive ? "ToExclusive" : "To";
	}

	protected internal virtual string ArgumentsPart()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append(".");
		if(fromExpr != null)
			sb.Append(",.");
		if(toExpr != null)
			sb.Append(",.");
		return sb.ToString();
	}

	protected internal virtual Operator FromOperator()
	{
		return fromExclusive ? Operator.GT : Operator.GE;
	}

	protected internal virtual Operator ToOperator()
	{
		return toExclusive ? Operator.LT : Operator.LE;
	}
}

}
