/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir;

//import de.unika.ipd.grgen.ast.BasicTypeNode;

public class UntypedExecVarType extends Type {
	/** @param ident The name of the type. */
	public UntypedExecVarType(Ident ident) {
		super("untyped exec var type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_UNTYPED_EXEC_VAR_TYPE;
	}

	public static Type getType() {
		return null;
	}
}

