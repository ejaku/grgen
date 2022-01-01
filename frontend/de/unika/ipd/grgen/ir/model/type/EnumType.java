/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir.model.type;

import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.model.EnumItem;
import de.unika.ipd.grgen.ir.type.basic.PrimitiveType;

/**
 * An enumeration type.
 */
public class EnumType extends PrimitiveType implements ContainedInPackage
{
	private String packageContainedIn;

	private final List<EnumItem> items = new LinkedList<EnumItem>();

	/** Make a new enum type.
	 *  @param ident The identifier of this enumeration. */
	public EnumType(Ident ident)
	{
		super("enum type", ident);
	}

	/** Add teh given item to a this enum type and autoenumerate it. */
	public void addItem(EnumItem item)
	{
		items.add(item);
	}

	/** @return A list with the identifiers in the enum type. */
	public List<EnumItem> getItems()
	{
		return Collections.unmodifiableList(items);
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	@Override
	public TypeClass classify()
	{
		return TypeClass.IS_INTEGER;
	}

	@Override
	public String getPackageContainedIn()
	{
		return packageContainedIn;
	}

	public void setPackageContainedIn(String packageContainedIn)
	{
		this.packageContainedIn = packageContainedIn;
	}

	@Override
	protected void canonicalizeLocal()
	{
		Collections.sort(items, Identifiable.COMPARATOR);
	}

	@Override
	public void addToDigest(StringBuffer sb)
	{
		sb.append(this);
		sb.append('[');

		int i = 0;
		for(Iterator<EnumItem> it = items.iterator(); it.hasNext(); i++) {
			EnumItem ent = it.next();
			if(i > 0)
				sb.append(',');
			sb.append(ent);
		}

		sb.append(']');
	}
}
