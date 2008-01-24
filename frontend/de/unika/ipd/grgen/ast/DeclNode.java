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
 * Created: Sat Jul  5 17:52:43 2003
 *
 * @author Sebastian Hack
 * @version $Id$
 */

package de.unika.ipd.grgen.ast;

import java.awt.Color;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;

/**
 * Base class for all AST nodes representing declarations.
 * children: IDENT:IdentNode TYPE:
 */
public abstract class DeclNode extends BaseNode implements DeclaredCharacter
{
	static {
		setName(DeclNode.class, "declaration");
	}

	IdentNode ident;
	BaseNode typeUnresolved;
	
	
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
	public abstract BaseNode getDeclType();

	/** @see de.unika.ipd.grgen.ast.DeclaredCharacter#getDecl() */
	public DeclNode getDecl() {
		return this;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeColor() */
	public Color getNodeColor() {
		return Color.BLUE;
	}

	public Entity getEntity() {
		return (Entity) checkIR(Entity.class);
	}

	protected IR constructIR() {
		Type type = (Type) getDeclType().checkIR(Type.class);
		return new Entity("entity", getIdentNode().getIdent(), type);
	}

	public static String getKindStr() {
		return "declaration";
	}

	public String toString() {
		return ident.toString();
	}
}

