/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.exprevals.PrimitiveType;

/**
 * Base class for all AST nodes representing declared types.
 * Declared types have identifiers (and declaration nodes).
 * The location of this type is set by the declaration node's
 * constructor
 * @see DeclNode#DeclNode(IdentNode, BaseNode)
 */
public abstract class DeclaredTypeNode extends TypeNode
{
	private DeclNode decl = null;

	/**
	 * Get the identifier of the type declaration.
	 * @return The identifier of the type declaration or an invalid
	 * identifier, if the type declaration was not set.
	 */
	public IdentNode getIdentNode() {
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
