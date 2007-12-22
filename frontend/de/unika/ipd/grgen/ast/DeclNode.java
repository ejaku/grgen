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

	/** Index of the identifier in the children array */
	protected static final int IDENT = 0;

	/** Index of the type in the children array */
	protected static final int TYPE = 1;

	protected static final int LAST = TYPE;

	protected static final String[] declChildrenNames = {
		"name", "type"
	};

	protected static final String[] addChildrenNames(String[] names) {
		String[] n = new String[names.length + declChildrenNames.length];
		int i;

		for(i = 0; i < declChildrenNames.length; i++) {
			n[i] = declChildrenNames[i];
		}

		for(int j = 0; j < names.length; j++, i++) {
			n[i] = names[j];
		}

		return n;
	}

	public static class InvalidDeclNode extends DeclNode {
		private InvalidDeclNode(IdentNode id) {
			super(id, BasicTypeNode.getErrorType(id));
		}

		public String getKindString() {
			return "undeclared identifier";
		}

		public String getUseString() {
			return "undeclared identifier";
		}

		public String toString() {
			return "undeclared identifier";
		}
	}

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
		addChild(n);
		addChild(t);
		setChildrenNames(declChildrenNames);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#doResolve() */
	protected boolean doResolve() {
		if(isResolved()) {
			return getResolve();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		setResolved(successfullyResolved); // local result
		
		successfullyResolved = getChild(IDENT).doResolve() && successfullyResolved;
		successfullyResolved = getChild(TYPE).doResolve() && successfullyResolved;
		return successfullyResolved;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#doCheck() */
	protected boolean doCheck() {
		assert(isResolved());
		if(!resolveResult) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean successfullyChecked = getCheck();
		if(successfullyChecked) {
			successfullyChecked = getTypeCheck();
		}
		successfullyChecked = getChild(IDENT).doCheck() && successfullyChecked;
		successfullyChecked = getChild(TYPE).doCheck() && successfullyChecked;
		return successfullyChecked;
	}
	
	/**
	 * Get the ident node for this declaration. The ident node represents
	 * the declared identifier.
	 * @return An ident node
	 */
	public IdentNode getIdentNode() {
		return (IdentNode) getChild(IDENT);
	}

	/**
	 * Get the type of the declaration
	 * @return The type node for the declaration
	 */
	public BaseNode getDeclType() {
		return getChild(TYPE);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.DeclaredCharacter#getDecl()
	 */
	public DeclNode getDecl() {
		return this;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#verify()
	 */
	protected boolean check() {
		return checkChild(IDENT, IdentNode.class)
			&& checkChild(TYPE, TypeNode.class);
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeColor()
	 */
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
		return getChild(IDENT).toString();
	}
}

