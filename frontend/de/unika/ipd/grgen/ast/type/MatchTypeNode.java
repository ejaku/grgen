/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.type;

import java.util.HashSet;
import java.util.Set;

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

	public abstract Set<DeclNode> getEntities();

	// get set of names of contained entities excluding anonymous entities
	public Set<String> getNamesOfEntities()
	{
		Set<String> set = new HashSet<String>();
		for(DeclNode entity : getEntities())
		{
			String name = entity.ident.toString();
			if(!name.startsWith("$"))
				set.add(name);
		}
		return set;
	}
}
