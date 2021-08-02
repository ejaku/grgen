/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.type;

import java.util.Set;
import java.util.stream.Collectors;

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
		return getEntities().stream()
				.map((DeclNode entity) -> entity.ident.toString())
				.filter((String name) -> !name.startsWith("$"))
				.collect(Collectors.toSet());
	}
}
