/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * The void type.
 */
public class VoidType extends PrimitiveType {

	public VoidType(Ident ident) {
		super("void type", ident);
	}

	public boolean isVoid() {
		return true;
	}

	public boolean isEqual(Type t) {
		return t.isVoid();
	}
}
