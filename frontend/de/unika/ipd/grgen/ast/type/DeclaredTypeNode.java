/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ast.type;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ir.type.basic.PrimitiveType;

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
	public IdentNode getIdentNode()
	{
		return decl != null ? decl.getIdentNode() : IdentNode.getInvalid();
	}

	/** Set the declaration of this type.
	 *  @param decl The declaration of this type. */
	public void setDecl(DeclNode decl)
	{
		this.decl = decl;
	}

	/** Get the declaration of this type
	 * @return The declaration of this type. */
	public DeclNode getDecl()
	{
		return decl;
	}

	public PrimitiveType getPrimitiveType()
	{
		return checkIR(PrimitiveType.class);
	}
	
	@Override
	public String getTypeName() {
		return getIdentNode().toString();
	}
}
