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
 * Created: Wed Jul  2 15:29:14 2003
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.parser.Symbol;
import de.unika.ipd.grgen.util.Attributed;
import de.unika.ipd.grgen.util.Attributes;
import de.unika.ipd.grgen.util.EmptyAttributes;
import java.awt.Color;

/**
 * AST node that represents an Identifier (name that appears within the specification)
 * children: none
 */
public class IdentNode extends BaseNode implements DeclaredCharacter, Attributed {
	static {
		setName(IdentNode.class, "identifier");
	}

	/** The attributes. */
	private Attributes attributes = EmptyAttributes.get();

	/** Occurrence of the identifier. */
	private Symbol.Occurrence occ;

	/** The declaration associated with this identifier. */
	private DeclNode decl = DeclNode.getInvalid();

	private static final IdentNode INVALID =
		new IdentNode(Symbol.Definition.getInvalid());

	/**
	 * Get an invalid ident node.
	 * @return An invalid ident node.
	 */
	public static IdentNode getInvalid() {
		return INVALID;
	}

	/**
	 * Make a new identifier node at a symbols's definition.
	 * @param def The definition of the symbol.
	 */
	public IdentNode(Symbol.Definition def) {
		this((Symbol.Occurrence) def);
		def.setNode(this);
	}

	/**
	 * Make a new identifier node at a symbol's occurrence.
	 * @param occ The occurrence of the symbol.
	 */
	public IdentNode(Symbol.Occurrence occ) {
		super(occ.getCoords());
		this.occ = occ;
	}

	/**
	 * Get the symbol definition of this identifier
	 * @see Symbol#Definition
	 * @return The symbol definition.
	 */
	public Symbol.Definition getSymDef() {
		return occ.getDefinition();
	}

	/**
	 * Set the definition of the symbol of this identifier.
	 * @param def The definition.
	 */
	public void setSymDef(Symbol.Definition def) {
		occ.setDefinition(def);
	}

	/**
	 * set the declaration node for this ident node. Each ident node
	 * declares an entity. To resolve this declared entity from the name,
	 * an ident node (which gets the name from the symbol defined
	 * by the symbol definition) has a declaration as its only child.
	 * @param n The declaration this ident represents.
	 * @return For convenience, this method returns <code>this</code>.
	 */
	public IdentNode setDecl(DeclNode n) {
		decl = n;
		return this;
	}

	/**
	 * Get the declaration corresponding to this node.
	 * @see #setDecl() for a detailed description.
	 * @return The declaration this node represents
	 */
	public DeclNode getDecl() {
		Symbol.Definition def = getSymDef();

		if(def.isValid()) {
			if(def.getNode() == this) return decl;
			else return def.getNode().getDecl();
		}
		else return DeclNode.getInvalid(this);
	}

	/**
	 * Get the symbol of the identifier.
	 * @return The symbol.
	 */
	public Symbol getSymbol() {
		return occ.getSymbol();
	}

	public String getNodeLabel() {
		return toString();
	}

	/**
	 * The string representation for this node. for an identidfier, this
	 * is the string of the symbol, the identifier represents.
	 */
	public String toString() {
		return occ.getSymbol().toString();
	}

	public static String getUseStr() {
		return "identifier";
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	public Color getNodeColor() {
		return Color.ORANGE;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeInfo()
	 */
	protected String extraNodeInfo() {
		return "occurrence: " + occ + "\ndefinition: " + getSymDef();
	}

	/**
	 * Get the current occurrence of this identifier.
	 * Each time this ident node is reused in the parser (rule identUse)
	 * the current occurrence changes.
	 * @return The current occurrence.
	 */
	public Symbol.Occurrence getCurrOcc() {
		return occ;
	}

	/**
	 * Get the IR object.
	 * This is an ident here.
	 * @return The IR object.
	 */
	public Ident getIdent() {
		return (Ident) checkIR(Ident.class);
	}

	/**
	 * Construct the ir object.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		Symbol sym = getSymbol();
		Symbol.Definition def = getSymDef();
		return Ident.get(toString(), def, getAttributes());
	}

	/**
	 * Get the attributes of this identifier.
	 * @return The attributes of this identifier.
	 */
	public Attributes getAttributes() {
		return attributes;
	}

	/**
	 * Set attributes for this ident node.
	 * @param attr The attributes.
	 */
	public void setAttributes(Attributes attr) {
		attributes = attr;
	}
}
