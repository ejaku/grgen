/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ast.BasicTypeNode;

/**
 * A boolean type
 */
public class BooleanType extends PrimitiveType {

	/** @param ident The name of the boolean type. */
	public BooleanType(Ident ident) {
		super("boolean type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_BOOLEAN;
	}

	public static Type getType() {
		return BasicTypeNode.booleanType.checkIR(Type.class);
	}
}
