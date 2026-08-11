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
using AlternativeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.AlternativeDeclNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using Alternative = de.unika.ipd.grgen.ir.pattern.Alternative;
using AlternativeReplacement = de.unika.ipd.grgen.ir.pattern.AlternativeReplacement;

public class AlternativeReplNode : OrderedReplacementNode
{
	static AlternativeReplNode()
	{
		SetClassName(typeof(AlternativeReplNode), "alternative repl node");
	}

	private IdentNode alternativeUnresolved;
	private AlternativeDeclNode alternative;

	public AlternativeReplNode(IdentNode n)
	{
		this.alternativeUnresolved = n;
		BecomeParent(this.alternativeUnresolved);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(GetValidVersion(alternativeUnresolved, alternative));
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("alternative");
			return childrenNames;
		}
	}

	private static readonly DeclarationResolver<AlternativeDeclNode> alternativeResolver =
		new DeclarationResolver<AlternativeDeclNode>(typeof(AlternativeDeclNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		alternative = alternativeResolver.Resolve(alternativeUnresolved, this);
		return alternative != null;
	}

	protected internal override bool CheckLocal()
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		return new AlternativeReplacement("alternative replacement", alternativeUnresolved.IRIdent,
				alternative.CheckIR(typeof(Alternative)));
	}
}

}
