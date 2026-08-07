/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.pattern
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
using SubpatternUsageDeclNode = de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
using de.unika.ipd.grgen.ast.util;
using de.unika.ipd.grgen.ast.util;

/// <summary>
/// Represents a reused single (pattern) graph entity.
/// 
/// This node is needed to distinguish between reused single nodes and reused
/// subpatterns.
/// After resolving in <seealso cref="PatternGraphRhsNode.resolveLocal()"/> this node should disappear.
/// 
/// @author buchwald
/// 
/// </summary>
public class SingleGraphEntityNode : BaseNode
{
	private IdentNode entityUnresolved;
	private NodeDeclNode entityNode;
	private SubpatternUsageDeclNode entitySubpattern;

	public SingleGraphEntityNode(IdentNode ent)
		: base(ent.Coords)
	{
		entityUnresolved = ent;
		BecomeParent(this.entityUnresolved);
	}

	protected internal override bool CheckLocal()
	{
		// this node should not exist after resolving
		return false;
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(entityUnresolved);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("entity");
		return childrenNames;
		}
	}

	private static readonly DeclarationPairResolver<NodeDeclNode, SubpatternUsageDeclNode> entityResolver =
			new DeclarationPairResolver<NodeDeclNode, SubpatternUsageDeclNode>(typeof(NodeDeclNode), typeof(SubpatternUsageDeclNode));

	protected internal override bool ResolveLocal()
	{
		if(!FixupDefinition(entityUnresolved, entityUnresolved.Scope))
			return false;

		Pair<NodeDeclNode, SubpatternUsageDeclNode> pair = entityResolver.Resolve(entityUnresolved, this);

		if(pair != null)
		{
			entityNode = pair.fst;
			entitySubpattern = pair.snd;
		}

		return entityNode != null || entitySubpattern != null;
	}

	protected internal virtual SubpatternUsageDeclNode EntitySubpattern
	{
		get
		{
		Debug.Assert(IsResolved());

		return entitySubpattern;
		}
	}

	protected internal virtual NodeDeclNode EntityNode
	{
		get
		{
		Debug.Assert(IsResolved());

		return entityNode;
		}
	}

	public static string KindStr
	{
		get
		{
		return "single graph entity";
		}
	}
}

}
