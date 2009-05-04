/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.HashSet;
import java.util.Set;

/**
 * An enumeration value
 */
public class EnumItem extends Identifiable {
	private final Ident id;

	private final EnumExpression value;

	/**
	 * Make a new enumeration value.
	 * @param id The enumeration item identifier.
	 * @param value The associated value.
	 */
	public EnumItem(Ident id, EnumExpression value) {
		super("enum item", id);
		this.id = id;
		this.value = value;
	}

	/** @return The identifier of the enum item. */
	public Ident getIdent() {
		return id;
	}

	/** The string of an enum item is its identifier's text.
	 * @see java.lang.Object#toString() */
	public String toString() {
		return id.toString();
	}

	/** @return The value of the enum item. */
	public EnumExpression getValue() {
		return value;
	}

	/** @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren() */
	public Collection<IR> getWalkableChildren() {
		Set<IR> res = new HashSet<IR>();
		res.add(id);
		res.add(value);
		return res;
	}
}
