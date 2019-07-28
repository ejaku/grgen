/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import java.awt.Color;

import de.unika.ipd.grgen.ir.Entity;

// TODO: things would be simpler if node/edge/var would be distinguished only by its type, not its declaration node?

/**
 * Base class for all AST nodes representing declarations.
 * children: IDENT:IdentNode TYPE:
 */
public abstract class DeclNode extends BaseNode implements DeclaredCharacter
{
	static {
		setName(DeclNode.class, "declaration");
	}

	public IdentNode ident;

	// TODO this should not be public
	public BaseNode typeUnresolved;


	/** An invalid declaration. */
	private static final DeclNode invalidDecl = new InvalidDeclNode(IdentNode.getInvalid());

	/** Get an invalid declaration. */
	public static final DeclNode getInvalid() {
		return invalidDecl;
	}

	/** Get an invalid declaration for an IdentNode. */
	public static final DeclNode getInvalid(IdentNode id) {
		return new InvalidDeclNode(id);
	}

	/**
	 * Create a new declaration node
	 * @param n The identifier that is declared
	 * @param t The type with which it is declared
	 */
	protected DeclNode(IdentNode n, BaseNode t) {
		super(n.getCoords());
		n.setDecl(this);
		this.ident = n;
		becomeParent(this.ident);
		this.typeUnresolved = t;
		becomeParent(this.typeUnresolved);
	}

	/** @return The ident node of the declaration */
	public IdentNode getIdentNode() {
		return ident;
	}

	/** @return The type node of the declaration */
	public abstract TypeNode getDeclType();

	/** @see de.unika.ipd.grgen.ast.DeclaredCharacter#getDecl() */
	public DeclNode getDecl() {
		return this;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeColor() */
	@Override
	public Color getNodeColor() {
		return Color.BLUE;
	}

	public Entity getEntity() {
		return checkIR(Entity.class);
	}

	//@Override
	//protected IR constructIR() {
	//	Type type = getDeclType().checkIR(Type.class);
	//	return new Entity("entity", getIdentNode().getIdent(), type, false);
	//}

	public static String getKindStr() {
		return "declaration";
	}

	@Override
	public String toString() {
		return ident.toString();
	}
}


