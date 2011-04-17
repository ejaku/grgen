/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
