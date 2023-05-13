/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.model;

import java.util.Collection;
import java.util.HashSet;
import java.util.Set;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.expr.EnumExpression;

/**
 * An enumeration value
 */
public class EnumItem extends Identifiable
{
	private final Ident id;

	private final EnumExpression value;

	/**
	 * Make a new enumeration value.
	 * @param id The enumeration item identifier.
	 * @param value The associated value.
	 */
	public EnumItem(Ident id, EnumExpression value)
	{
		super("enum item", id);
		this.id = id;
		this.value = value;
	}

	/** @return The identifier of the enum item. */
	@Override
	public Ident getIdent()
	{
		return id;
	}

	/** The string of an enum item is its identifier's text.
	 * @see java.lang.Object#toString() */
	@Override
	public String toString()
	{
		return id.toString();
	}

	/** @return The value of the enum item. */
	public EnumExpression getValue()
	{
		return value;
	}

	/** @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren() */
	public Collection<IR> getWalkableChildren()
	{
		Set<IR> res = new HashSet<IR>();
		res.add(id);
		res.add(value);
		return res;
	}
}
