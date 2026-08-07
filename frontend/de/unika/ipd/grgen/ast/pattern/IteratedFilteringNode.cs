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
using de.unika.ipd.grgen.ast;
using FilterInvocationBaseNode = de.unika.ipd.grgen.ast.FilterInvocationBaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using PackageIdentNode = de.unika.ipd.grgen.ast.PackageIdentNode;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ActionDeclNode = de.unika.ipd.grgen.ast.decl.executable.ActionDeclNode;
using SubpatternDeclNode = de.unika.ipd.grgen.ast.decl.executable.SubpatternDeclNode;
using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using de.unika.ipd.grgen.ast.util;
using de.unika.ipd.grgen.ast.util;
using de.unika.ipd.grgen.ast.util;
using FilterInvocationBase = de.unika.ipd.grgen.ir.FilterInvocationBase;
using IR = de.unika.ipd.grgen.ir.IR;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using IteratedFiltering = de.unika.ipd.grgen.ir.pattern.IteratedFiltering;

public class IteratedFilteringNode : EvalStatementNode
{
	static IteratedFilteringNode()
	{
		SetClassName(typeof(IteratedFilteringNode), "iterated filtering node");
	}

	private IdentNode actionUnresolved;
	private ActionDeclNode action;
	private SubpatternDeclNode subpattern;

	private IdentNode iteratedUnresolved;
	private IteratedDeclNode iterated;

	private CollectNode<FilterInvocationBaseNode> filters;

	public IteratedFilteringNode(IdentNode actionUnresolved, IdentNode iteratedUnresolved,
			CollectNode<FilterInvocationBaseNode> filtersUnresolved)
		: base(iteratedUnresolved.Coords)
	{
		this.actionUnresolved = BecomeParent(actionUnresolved);
		this.iteratedUnresolved = BecomeParent(iteratedUnresolved);
		this.filters = BecomeParent(filtersUnresolved);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		//children.add(getValidVersion(iteratedUnresolved, iterated));
		children.Add(filters);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		//childrenNames.add("iterated");
		childrenNames.Add("filters");
		return childrenNames;
		}
	}

	private static readonly DeclarationPairResolver<ActionDeclNode, SubpatternDeclNode> actionOrSubpatternResolver =
			new DeclarationPairResolver<ActionDeclNode, SubpatternDeclNode>(typeof(ActionDeclNode), typeof(SubpatternDeclNode));
	private static readonly DeclarationResolver<IteratedDeclNode> iteratedResolver =
			new DeclarationResolver<IteratedDeclNode>(typeof(IteratedDeclNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		if(!(actionUnresolved is PackageIdentNode))
			FixupDefinition(actionUnresolved, actionUnresolved.Scope);

		Pair<ActionDeclNode, SubpatternDeclNode> actionOrSubpattern = actionOrSubpatternResolver.Resolve(actionUnresolved, this);
		if(actionOrSubpattern == null || actionOrSubpattern.fst == null && actionOrSubpattern.snd == null)
			return false;
		if(actionOrSubpattern.fst != null)
			action = actionOrSubpattern.fst;
		if(actionOrSubpattern.snd != null)
			subpattern = actionOrSubpattern.snd;
		iterated = iteratedResolver.Resolve(iteratedUnresolved, this);
		return iterated != null;
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		IteratedFiltering iteratedFiltering = new IteratedFiltering("iterated filtering",
				action != null ? action.CheckIR(typeof(Rule)) : subpattern.CheckIR(typeof(Rule)),
				iterated.CheckIR(typeof(Rule)));
		foreach(FilterInvocationBaseNode filter in filters.ChildrenExact)
			iteratedFiltering.AddFilterInvocation(filter.CheckIR(typeof(FilterInvocationBase)));
		return iteratedFiltering;
	}
}

}
