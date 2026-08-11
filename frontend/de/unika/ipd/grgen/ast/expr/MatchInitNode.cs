/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{

using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ast;
using DefinedMatchTypeNode = de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using MatchInit = de.unika.ipd.grgen.ir.expr.MatchInit;
using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class MatchInitNode : ExprNode
{
	static MatchInitNode()
	{
		SetClassName(typeof(MatchInitNode), "match init");
	}

	private IdentNode matchTypeUnresolved;
	private DefinedMatchTypeNode matchType;

	public MatchInitNode(Coords coords, IdentNode matchType)
		: base(coords)
	{
		this.matchTypeUnresolved = matchType;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
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
			return childrenNames;
		}
	}

	private static readonly DeclarationTypeResolver<DefinedMatchTypeNode> matchTypeResolver =
			new DeclarationTypeResolver<DefinedMatchTypeNode>(typeof(DefinedMatchTypeNode));

	protected internal override bool ResolveLocal()
	{
		matchType = matchTypeResolver.Resolve(matchTypeUnresolved, this);
		return matchType != null && matchType.Resolve();
	}

	protected internal override bool CheckLocal()
	{
		return true;
	}

	public override TypeNode Type
	{
		get
		{
			return MatchType;
		}
	}

	public virtual DefinedMatchTypeNode MatchType
	{
		get
		{
			Debug.Assert((IsResolved()));
			return matchType;
		}
	}

	protected internal override IR ConstructIR()
	{
		DefinedMatchType type = matchType.CheckIR(typeof(DefinedMatchType));
		return new MatchInit(type);
	}

	public virtual MatchInit IRMatchInit
	{
		get
		{
			return CheckIR(typeof(MatchInit));
		}
	}

	public static string KindStr
	{
		get
		{
			return "match initialization";
		}
	}
}

}
