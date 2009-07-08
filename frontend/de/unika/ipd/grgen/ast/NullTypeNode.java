/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.ObjectType;

public class NullTypeNode extends BasicTypeNode
{
	static {
		setName(NullTypeNode.class, "null type");
	}

	@Override
	protected boolean isCompatibleTo(TypeNode t) {
		// null is compatible to all graph element types, object and string
		if(!(t instanceof BasicTypeNode)) return true;
		if(t == BasicTypeNode.objectType || t == BasicTypeNode.stringType) return true;
		return false;
	}

	@Override
	protected boolean isCastableTo(TypeNode t) {
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
