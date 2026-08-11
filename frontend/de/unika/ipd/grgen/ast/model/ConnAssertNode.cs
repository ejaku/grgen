/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.model
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using RangeSpecNode = de.unika.ipd.grgen.ast.RangeSpecNode;
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using de.unika.ipd.grgen.ast.util;
using Coords = de.unika.ipd.grgen.parser.Coords;
using IR = de.unika.ipd.grgen.ir.IR;
using ConnAssert = de.unika.ipd.grgen.ir.model.ConnAssert;
using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;

/// <summary>
/// AST node that represents a Connection Assertion
/// children: SRC:IdentNode, SRCRANGE:RangeSpecNode, TGT:IdentNode, TGTRANGE:RangeSpecNode
/// or
/// AST node that represents a "meta" Connection Assertion which tells to
/// inherit the connection assertions from the parent edges;
/// after resolving it gets replaced by the connection assertions of the parent nodes.
/// </summary>
public class ConnAssertNode : BaseNode
{
	static ConnAssertNode()
	{
		SetClassName(typeof(ConnAssertNode), "conn assert");
	}

	private NodeTypeNode src;
	private BaseNode srcUnresolved;
	private RangeSpecNode srcRange;
	private NodeTypeNode tgt;
	private BaseNode tgtUnresolved;
	private RangeSpecNode tgtRange;
	private bool bothDirections;

	public bool copyExtends;

	/// <summary>
	/// Construct a new connection assertion node.
	/// </summary>
	public ConnAssertNode(IdentNode src, RangeSpecNode srcRange,
			IdentNode tgt, RangeSpecNode tgtRange,
			bool bothDirections)
		: base(src.Coords)
	{
		this.srcUnresolved = src;
		BecomeParent(this.srcUnresolved);
		this.srcRange = srcRange;
		BecomeParent(this.srcRange);
		this.tgtUnresolved = tgt;
		BecomeParent(this.tgtUnresolved);
		this.tgtRange = tgtRange;
		BecomeParent(this.tgtRange);
		this.bothDirections = bothDirections;
		this.copyExtends = false;
	}

	/// <summary>
	/// Construct a new copy extends = inherit connection assertions from the parent connection assertion node.
	/// </summary>
	public ConnAssertNode(Coords coords)
		: base(coords)
	{
		this.copyExtends = true;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		if(!copyExtends)
		{
			children.Add(GetValidVersion(srcUnresolved, src));
			children.Add(srcRange);
			children.Add(GetValidVersion(tgtUnresolved, tgt));
			children.Add(tgtRange);
		}
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
		if(!copyExtends)
		{
			childrenNames.Add("src");
			childrenNames.Add("src range");
			childrenNames.Add("tgt");
			childrenNames.Add("tgt range");
		}
		return childrenNames;
		}
	}

	private static DeclarationTypeResolver<NodeTypeNode> nodeResolver =
			new DeclarationTypeResolver<NodeTypeNode>(typeof(NodeTypeNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		if(copyExtends)
			return true;

		src = nodeResolver.Resolve(srcUnresolved, this);
		tgt = nodeResolver.Resolve(tgtUnresolved, this);

		return src != null && tgt != null;
	}

	/// <summary>
	/// Check, if the AST node is correctly built. </summary>
	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"/>
	protected internal override bool CheckLocal()
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		Debug.Assert(!copyExtends); // must have been replaced by copies of the connection assertions of the parents befor entering this phase

		long srcLower = srcRange.Lower;
		long srcUpper = srcRange.Upper;
		NodeType srcType = src.CheckIR(typeof(NodeType));

		long tgtLower = tgtRange.Lower;
		long tgtUpper = tgtRange.Upper;
		NodeType tgtType = tgt.CheckIR(typeof(NodeType));

		return new ConnAssert(srcType, srcLower, srcUpper, tgtType, tgtLower, tgtUpper, bothDirections);
	}
}

}
