/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir;

/**
 * IR class that represents node types.
 */
public class NodeType extends InheritanceType implements ContainedInPackage {
	private String packageContainedIn;
	
	/**
	 * Make a new node type.
	 * @param ident The identifier that declares this type.
	 * @param modifiers The modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public NodeType(Ident ident, int modifiers, String externalName) {
		super("node type", ident, modifiers, externalName);
	}
	
	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_NODE;
	}
	
	public String getPackageContainedIn() {
		return packageContainedIn;
	}
	
	public void setPackageContainedIn(String packageContainedIn) {
		this.packageContainedIn = packageContainedIn;
	}
}
