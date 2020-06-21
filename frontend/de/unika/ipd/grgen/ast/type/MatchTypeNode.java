/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.type;

import de.unika.ipd.grgen.ast.MemberAccessor;
import de.unika.ipd.grgen.ast.decl.DeclNode;

// base class for the different match types (action, iterated, defined=match class)
public abstract class MatchTypeNode extends DeclaredTypeNode implements MemberAccessor
{
	static {
		setName(MatchTypeNode.class, "match type");
	}

	@Override
	public String getName()
	{
		return getTypeName();
	}

	@Override
	public abstract DeclNode tryGetMember(String name);
}
