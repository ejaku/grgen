/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.decl.executable;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.FilterAutoSupplied;
import de.unika.ipd.grgen.ir.executable.Rule;

/**
 * AST node class representing auto supplied filters (automatically declared)
 */
public class FilterAutoSuppliedDeclNode extends FilterAutoDeclNode
{
	static {
		setName(FilterAutoSuppliedDeclNode.class, "auto supplied filter");
	}

	IdentNode ident;

	protected IdentNode actionUnresolved;
	protected TestDeclNode action;
	protected IteratedDeclNode iterated;

	public FilterAutoSuppliedDeclNode(IdentNode ident, IdentNode action)
	{
		super(ident);

		this.ident = ident;
		this.actionUnresolved = action;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(actionUnresolved, action, iterated));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("actionOrIterated");
		return childrenNames;
	}

	private static final DeclarationPairResolver<TestDeclNode, IteratedDeclNode> actionOrIteratedResolver =
			new DeclarationPairResolver<TestDeclNode, IteratedDeclNode>(TestDeclNode.class, IteratedDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		Pair<TestDeclNode, IteratedDeclNode> actionOrIterated = actionOrIteratedResolver.resolve(actionUnresolved, this);
		if(actionOrIterated == null)
			return false;
		action = actionOrIterated.fst;
		iterated = actionOrIterated.snd;
		return action != null || iterated != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		// return if the IR object was already constructed
		// that may happen in recursive calls
		if(isIRAlreadySet()) {
			return getIR();
		}

		FilterAutoSupplied filterAutoSup = new FilterAutoSupplied(ident.toString());

		// mark this node as already visited
		setIR(filterAutoSup);

		Rule actionOrIterated = action != null ? action.getAction() : iterated.getAction();
		filterAutoSup.setAction(actionOrIterated);
		actionOrIterated.addFilter(filterAutoSup);

		return filterAutoSup;
	}

	public static String getKindStr()
	{
		return "auto supplied filter";
	}
}
