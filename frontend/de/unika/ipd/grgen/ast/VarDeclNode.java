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
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import java.util.Collection;

/**
 * Decleration of a variable.
 */
public class VarDeclNode extends DeclNode {

	public VarDeclNode(IdentNode id, TypeNode type) {
		super(id, type);
    }

	/** returns children of this node */
	public Collection<? extends BaseNode> getChildren() {
		// TODO
		return null;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		// TODO
		return null;
	}

	/**
	 * local resolving of the current node to be implemented by the subclasses, called from the resolve AST walk
	 * @return true, if resolution of the AST locally finished successfully;
	 * false, if there was some error.
	 */
	protected boolean resolveLocal() {
		// TODO
		return false;
	}

	/**
	 * local checking of the current node to be implemented by the subclasses, called from the check AST walk
	 * @return true, if checking of the AST locally finished successfully;
	 * false, if there was some error.
	 */
	protected boolean checkLocal() {
		// TODO
		return false;
	}

	/** @return The type node of the declaration */
	public TypeNode getDeclType() {
		// TODO
		return null;
	}
}
