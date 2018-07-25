/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ir.exprevals.*;
import de.unika.ipd.grgen.ast.BasicTypeNode;

/**
 * A graph type.
 */
public class GraphType extends PrimitiveType {
	public GraphType(Ident ident) {
		super("graph type", ident);
	}

	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_GRAPH;
	}

	public static Type getType() {
		return BasicTypeNode.graphType.checkIR(Type.class);
	}
}
