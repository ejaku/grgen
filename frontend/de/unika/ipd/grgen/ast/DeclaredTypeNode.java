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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

/**
 * Base class for all AST nodes representing declared types.
 * Declared types have identifiers (and declaration nodes).
 * The location of this type is set by the declaration node's
 * constructor
 * @see DeclNode#DeclNode(IdentNode, BaseNode)
 */
import de.unika.ipd.grgen.ir.PrimitiveType;

public abstract class DeclaredTypeNode extends TypeNode
{
	private DeclNode decl = null;

	/**
	 * Get the identifier of the type declaration.
	 * @return The identifier of the type declaration or an invalid
	 * identifier, if the type declaration was not set.
	 */
	protected IdentNode getIdentNode() {
		return decl != null ? decl.getIdentNode() : IdentNode.getInvalid();
	}

	/** Set the declaration of this type.
	 *  @param decl The declaration of this type. */
	protected void setDecl(DeclNode decl) {
		this.decl = decl;
	}

	/** Get the declaration of this type
	 * @return The declaration of this type. */
	public DeclNode getDecl() {
		return decl;
	}

	protected PrimitiveType getPrimitiveType() {
		return checkIR(PrimitiveType.class);
	}
}
