/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.parser.Symbol;

/**
 * AST node that represents an Identifier in a package (name that appears within the specification),
 */
 public class PackageIdentNode extends IdentNode {
	static {
		setName(PackageIdentNode.class, "package identifier");
	}

	/** Occurrence of the package identifier owning the base identifier. */
	public Symbol.Occurrence owningPackage;

	/** The declaration of the package owning the base identifier. */
	protected DeclNode ownerDecl = DeclNode.getInvalid();

	/**
	 * Make a new identifier node at a symbol's occurrence.
	 * @param owningPackage The occurrence of the symbol of the package owning the identifier.
	 * @param occ The occurrence of the symbol of the identifier.
	 */
	public PackageIdentNode(Symbol.Occurrence owningPackage, Symbol.Occurrence occ) {
		super(occ);
		this.owningPackage = owningPackage; 
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

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		// there must be exactly one definition
		return super.checkLocal() && getOwnerSymDef().isValid();
	}

	public Symbol.Definition getOwnerSymDef() {
		if(owningPackage.getDefinition()==null || !owningPackage.getDefinition().isValid()) {
			Symbol.Definition def = owningPackage.getScope().getCurrDef(getOwnerSymbol());
			if(def.isValid())
				setOwnerSymDef(def);
		}
		return owningPackage.getDefinition();
	}

	public void setOwnerSymDef(Symbol.Definition def) {
		owningPackage.setDefinition(def);
	}

	public IdentNode setOwnerDecl(DeclNode n) {
		ownerDecl = n;
		return this;
	}

	public DeclNode getOwnerDecl() {
		Symbol.Definition def = getOwnerSymDef();

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

	public Symbol getOwnerSymbol() {
		return owningPackage.getSymbol();
	}

	public DeclNode getDecl() {
		Resolver.resolveOwner(this);
		return super.getDecl();
	}

	@Override
	public String toString() {
		return owningPackage.getSymbol().toString() + "::" + occ.getSymbol().toString();
	}

	public static String getUseStr() {
		return "identifier";
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeInfo()
	 */
	@Override
	protected String extraNodeInfo() {
		return "package: " + owningPackage + "occurrence: " + occ + "\ndefinition: " + getSymDef();
	}

	/**
	 * Get the IR object.
	 * This is an ident here.
	 * @return The IR object.
	 */
	//public Ident getIdent() {
	//	return checkIR(Ident.class);
	//}
	// TODO: remove

	/**
	 * Construct the ir object.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected Ident constructIR() {
		//Symbol.Definition def = getSymDef();
		//return Ident.get(toString(), def, getAnnotations());
		// TODO: remove
		return null;
	}
}
