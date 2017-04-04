/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ast.exprevals;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.ObjectType;

public class NullTypeNode extends BasicTypeNode
{
	static {
		setName(NullTypeNode.class, "null type");
	}

	@Override
	public boolean isCompatibleTo(TypeNode t) {
		// null is compatible to all graph element types, object, string, and graph
		if(!(t instanceof BasicTypeNode)) return true;
		if(t == BasicTypeNode.objectType || t == BasicTypeNode.stringType || t == BasicTypeNode.graphType) return true;
		return false;
	}

	@Override
	public boolean isCastableTo(TypeNode t) {
		return isCompatibleTo(t);
	}

	@Override
	protected IR constructIR() {
		// TODO: Check whether this is OK
		return new ObjectType(getIdentNode().getIdent());
	}

	@Override
	public String toString() {
		return "null";
	}
}
