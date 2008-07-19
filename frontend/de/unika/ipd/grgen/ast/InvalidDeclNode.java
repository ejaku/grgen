/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
/**
 * AST node class representing invalid declarations.
 */
public class InvalidDeclNode extends DeclNode {

	static {
		setName(InvalidDeclNode.class, "invalid declaration");
	}

	ErrorTypeNode type;

	/**
	 * Create a resolved and checked invalid DeclNode.
	 */
	public InvalidDeclNode(IdentNode id) {
		super(id, BasicTypeNode.getErrorType(id));
		resolve();
		check();
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		return childrenNames;
	}

	DeclarationResolver<ErrorTypeNode> typeResolver = new DeclarationResolver<ErrorTypeNode>(ErrorTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);

		return type != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return true;
	}

	public static String getKindStr() {
		return "undeclared identifier";
	}

	public static String getUseStr() {
		return "undeclared identifier";
	}

	public String toString() {
		return "undeclared identifier";
	}

	@Override
	public TypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}
}
