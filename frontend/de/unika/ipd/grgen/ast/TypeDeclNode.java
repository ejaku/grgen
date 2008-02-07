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


/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;

/**
 * Declaration of a type.
 */
public class TypeDeclNode extends DeclNode {
	static {
		setName(TypeDeclNode.class, "type declaration");
	}

	DeclaredTypeNode type;
	
	public TypeDeclNode(IdentNode i, BaseNode t) {
		super(i, t);

		// Set the declaration of the declared type node to this node.
		if(t instanceof DeclaredTypeNode) {
			((DeclaredTypeNode) t).setDecl(this);
		}
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

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		DeclarationTypeResolver<DeclaredTypeNode> typeResolver =
			new DeclarationTypeResolver<DeclaredTypeNode>(DeclaredTypeNode.class);
		type = typeResolver.resolve(typeUnresolved, this); 
		return type != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return true;
	}

	/**
	 * A type declaration returns the declared type
	 * as result.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		return getDeclType().getIR();
	}

	public static String getKindStr() {
		return "type declaration";
	}

	public static String getUseStr() {
		return "type";
	}

	@Override
	public DeclaredTypeNode getDeclType()
	{
		assert isResolved();
		
		return type;
	}
}
