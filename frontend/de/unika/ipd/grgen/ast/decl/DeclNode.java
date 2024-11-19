/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.decl;

import java.awt.Color;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.DeclaredCharacter;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.model.type.ArbitraryEdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.DirectedEdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.UndirectedEdgeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.Entity;

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

	public BaseNode typeUnresolved;

	/** An invalid declaration. */
	private static final DeclNode invalidDecl = new InvalidDeclNode(IdentNode.getInvalid());

	/** Get an invalid declaration. */
	public static final DeclNode getInvalid()
	{
		return invalidDecl;
	}

	/** Get an invalid declaration for an IdentNode. */
	public static final DeclNode getInvalid(IdentNode id)
	{
		return new InvalidDeclNode(id);
	}

	/**
	 * Create a new declaration node
	 * @param n The identifier that is declared
	 * @param t The type with which it is declared
	 */
	protected DeclNode(IdentNode n, BaseNode t)
	{
		super(n.getCoords());
		n.setDecl(this);
		this.ident = n;
		becomeParent(this.ident);
		this.typeUnresolved = t;
		becomeParent(this.typeUnresolved);
	}

	/** @return The ident node of the declaration */
	public IdentNode getIdentNode()
	{
		return ident;
	}

	/** @return The type node of the declaration */
	public abstract TypeNode getDeclType();

	/** @see de.unika.ipd.grgen.ast.DeclaredCharacter#getDecl() */
	@Override
	public DeclNode getDecl()
	{
		return this;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeColor() */
	@Override
	public Color getNodeColor()
	{
		return Color.BLUE;
	}

	public final String emptyWhenAnonymous(String str)
	{
		return getIdentNode().getCurrOcc().isAnonymous() ? "" : str;
	}

	public final String emptyWhenAnonymousPostfix(String prefix)
	{
		return getIdentNode().getCurrOcc().isAnonymous() ? "" : prefix + getIdentNode();
	}

	public final String emptyWhenAnonymousInParenthesis(String prefix)
	{
		return getIdentNode().getCurrOcc().isAnonymous() ? "" : prefix + "(" + getIdentNode() + ")";
	}

	public final String dotOrArrowWhenAnonymous()
	{
		if(getIdentNode().getCurrOcc().isAnonymous() && this instanceof EdgeDeclNode)
		{
			EdgeDeclNode edge = (EdgeDeclNode)this;
			if(edge.getDeclType() instanceof ArbitraryEdgeTypeNode)
				return "?--?";
			else if(edge.getDeclType() instanceof DirectedEdgeTypeNode)
				return "-->";
			else if(edge.getDeclType() instanceof UndirectedEdgeTypeNode)
				return "--";
		}
		return getIdentNode().getCurrOcc().isAnonymous() ? "." : getIdentNode().toString();
	}

	public Entity getEntity()
	{
		return checkIR(Entity.class);
	}

	//@Override
	//protected IR constructIR() {
	//	Type type = getDeclType().checkIR(Type.class);
	//	return new Entity("entity", getIdentNode().getIdent(), type, false);
	//}

	public static String getKindStr()
	{
		return "declaration";
	}

	@Override
	public String toString()
	{
		return ident.toString();
	}
}
