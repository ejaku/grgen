/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.pattern
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using IteratedReplacement = de.unika.ipd.grgen.ir.pattern.IteratedReplacement;

public class IteratedReplNode : OrderedReplacementNode
{
	static IteratedReplNode()
	{
		SetClassName(typeof(IteratedReplNode), "iterated repl node");
	}

	private IdentNode iteratedUnresolved;
	private IteratedDeclNode iterated;

	public IteratedReplNode(IdentNode n)
	{
		this.iteratedUnresolved = n;
		BecomeParent(this.iteratedUnresolved);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(GetValidVersion(iteratedUnresolved, iterated));
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("iterated");
			return childrenNames;
		}
	}

	private static readonly DeclarationResolver<IteratedDeclNode> iteratedResolver =
			new DeclarationResolver<IteratedDeclNode>(typeof(IteratedDeclNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		iterated = iteratedResolver.Resolve(iteratedUnresolved, this);
		return iterated != null;
	}

	protected internal override bool CheckLocal()
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		return new IteratedReplacement("iterated replacement", iteratedUnresolved.IRIdent,
				iterated.CheckIR(typeof(Rule)));
	}
}

}
