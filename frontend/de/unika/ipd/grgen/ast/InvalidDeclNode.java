/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
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
