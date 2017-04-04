/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;
import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.parser.Symbol;
import de.unika.ipd.grgen.util.Annotated;
import de.unika.ipd.grgen.util.Annotations;
import de.unika.ipd.grgen.util.EmptyAnnotations;

/**
 * AST node that represents an Identifier (name that appears within the specification)
 * children: none
 */
public class IdentNode extends BaseNode implements DeclaredCharacter, Annotated {
	static {
		setName(IdentNode.class, "identifier");
	}

	/** The annotations. */
	protected Annotations annotations = EmptyAnnotations.get();

	/** Occurrence of the identifier. */
	protected Symbol.Occurrence occ;

	/** The declaration associated with this identifier. */
	protected DeclNode decl = DeclNode.getInvalid();

	protected static final IdentNode INVALID =
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

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		// there must be exactly one definition
		return getSymDef().isValid();
	}

	/**
	 * Get the symbol definition of this identifier
	 * @see Symbol#Definition
	 * @return The symbol definition.
	 */
	public Symbol.Definition getSymDef() {
		if(occ.getDefinition()==null) {
			// I don't now why this is needed, it feels like a hack, but it works
			Symbol.Definition def = occ.getScope().getCurrDef(getSymbol());
			if(def.isValid())
				setSymDef(def);
		}
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
			if(def.getNode() == this) {
				return decl;
			} else {
				return def.getNode().getDecl();
			}
		} else {
			return DeclNode.getInvalid(this);
		}
	}

	/**
	 * Get the symbol of the identifier.
	 * @return The symbol.
	 */
	public Symbol getSymbol() {
		return occ.getSymbol();
	}

	@Override
	public String getNodeLabel() {
		return toString();
	}

	/**
	 * The string representation for this node.
	 * For an identifier, this is the string of the symbol, the identifier represents.
	 */
	@Override
	public String toString() {
		return occ.getSymbol().toString();
	}

	public static String getUseStr() {
		return "identifier";
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	@Override
	public Color getNodeColor() {
		return Color.ORANGE;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeInfo()
	 */
	@Override
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
		return checkIR(Ident.class);
	}

	/**
	 * Construct the ir object.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected Ident constructIR() {
		Symbol.Definition def = getSymDef();
		return Ident.get(toString(), def, getAnnotations());
	}

	/**
	 * Get the annotations of this identifier.
	 * @return The annotations of this identifier.
	 */
	public Annotations getAnnotations() {
		return annotations;
	}

	/**
	 * Set annotations for this ident node.
	 * @param annots The annotations.
	 */
	public void setAnnotations(Annotations annots) {
		annotations = annots;
	}
}
