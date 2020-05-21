/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ir.FilterInvocation;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.IteratedFiltering;
import de.unika.ipd.grgen.ir.Rule;

public class IteratedFilteringNode extends EvalStatementNode
{
	static {
		setName(IteratedFilteringNode.class, "iterated filtering node");
	}

	private IdentNode actionUnresolved;
	private TestDeclNode action;
	private SubpatternDeclNode subpattern;

	private IdentNode iteratedUnresolved;
	private IteratedNode iterated;

	private CollectNode<FilterInvocationNode> filters;

	public IteratedFilteringNode(IdentNode actionUnresolved, IdentNode iteratedUnresolved,
			CollectNode<FilterInvocationNode> filtersUnresolved)
	{
		super(iteratedUnresolved.getCoords());
		this.actionUnresolved = becomeParent(actionUnresolved);
		this.iteratedUnresolved = becomeParent(iteratedUnresolved);
		this.filters = becomeParent(filtersUnresolved);
	}

	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		//children.add(getValidVersion(iteratedUnresolved, iterated));
		children.add(filters);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		//childrenNames.add("iterated");
		childrenNames.add("filters");
		return childrenNames;
	}

	private static final DeclarationPairResolver<TestDeclNode, SubpatternDeclNode> actionOrSubpatternResolver =
			new DeclarationPairResolver<TestDeclNode, SubpatternDeclNode>(TestDeclNode.class, SubpatternDeclNode.class);
	private static final DeclarationResolver<IteratedNode> iteratedResolver =
			new DeclarationResolver<IteratedNode>(IteratedNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		if(!(actionUnresolved instanceof PackageIdentNode))
			fixupDefinition((IdentNode)actionUnresolved, actionUnresolved.getScope());

		Pair<TestDeclNode, SubpatternDeclNode> actionOrSubpattern = actionOrSubpatternResolver.resolve(actionUnresolved, this);
		if(actionOrSubpattern == null || actionOrSubpattern.fst == null && actionOrSubpattern.snd == null)
			return false;
		if(actionOrSubpattern.fst != null)
			action = actionOrSubpattern.fst;
		if(actionOrSubpattern.snd != null)
			subpattern = actionOrSubpattern.snd;
		iterated = iteratedResolver.resolve(iteratedUnresolved, this);
		return iterated != null;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		IteratedFiltering iteratedFiltering = new IteratedFiltering("iterated filtering",
				action != null ? action.checkIR(Rule.class) : subpattern.checkIR(Rule.class),
				iterated.checkIR(Rule.class));
		for(FilterInvocationNode filter : filters.getChildren()) {
			iteratedFiltering.addFilterInvocation(filter.checkIR(FilterInvocation.class));
		}
		return iteratedFiltering;
	}
}
