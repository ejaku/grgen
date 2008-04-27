/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * IR class that represents node types.
 */
public class NodeType extends InheritanceType {
	/**
	 * Make a new node type.
	 * @param ident The identifier that declares this type.
	 * @param modifiers The modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public NodeType(Ident ident, int modifiers, String externalName) {
		super("node type", ident, modifiers, externalName);
	}
}
