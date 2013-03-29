/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.Bad;
import de.unika.ipd.grgen.ir.IR;
import java.util.Collection;
import java.util.Vector;


/**
 * AST node class representing match filters
 */
public class FilterDeclNode extends ActionDeclNode {
	static {
		setName(FilterDeclNode.class, "filter declaration");
	}

	private FilterTypeNode type;
	private static final TypeNode filterType = new FilterTypeNode();
	private IdentNode actionUnresolved;
	public TestDeclNode action;

	public FilterDeclNode(IdentNode id, IdentNode actionUnresolved) {
		super(id, filterType);
		this.actionUnresolved = actionUnresolved;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		return childrenNames;
	}

	private static final DeclarationTypeResolver<FilterTypeNode> typeResolver = 
		new DeclarationTypeResolver<FilterTypeNode>(FilterTypeNode.class);

	private static final DeclarationResolver<TestDeclNode> actionResolver =
		new DeclarationResolver<TestDeclNode>(TestDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);
		action = actionResolver.resolve(actionUnresolved);
		return type != null && action != null;
	}


	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	public TypeNode getDeclType() {
		assert isResolved();
		return type;
	}

	public static String getKindStr() {
		return "filter declaration";
	}

	public static String getUseStr() {
		return "filter";
	}

	@Override
	protected IR constructIR() {
		assert false;
		return Bad.getBad();
	}
}


