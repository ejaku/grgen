/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast
{

using System.Collections.Generic;

using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// AST node representing a range specification (used by ConnAssertNode).
/// children: none
/// </summary>
public class RangeSpecNode : BaseNode
{
	static RangeSpecNode()
	{
		SetClassName(typeof(RangeSpecNode), "range spec");
	}

	/// <summary>
	/// Constant, signaling if upper bound is bounded. </summary>
	public static readonly long UNBOUND = int.MaxValue;

	/// <summary>
	/// The upper and lower bound. </summary>
	private long lower, upper;

	/// <param name="coords"> </param>
	public RangeSpecNode(Coords coords, long lower, long upper)
		: base(coords)
	{
		this.lower = lower;
		this.upper = upper;
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
		bool good = true;
		if(lower < 0)
		{
			ReportError("The lower bound of the range must be a positive number.");
			good = false;
		}
		if(upper < 0)
		{
			ReportError("The upper bound of the range must be a positive number.");
			good = false;
		}
		if(lower > upper)
		{
			ReportError("The lower bound must be less (or equal) than the upper bound of the range.");
			good = false;
		}
		return good;
	}

	public override string Name
	{
		get
		{
			return base.Name + " [" + lower + ":" + upper + "]";
		}
	}

	/// <returns> the lower bound of the range. </returns>
	public virtual long Lower
	{
		get
		{
			return lower;
		}
	}

	/// <returns> the upper bound of the range. </returns>
	public virtual long Upper
	{
		get
		{
			return upper;
		}
	}
}

}
