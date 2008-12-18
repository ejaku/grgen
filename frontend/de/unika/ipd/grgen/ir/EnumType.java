/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

/**
 * An enumeration type.
 */
public class EnumType extends PrimitiveType {

	private final List<EnumItem> items = new LinkedList<EnumItem>();

	/** Make a new enum type.
	 *  @param ident The identifier of this enumeration. */
	public EnumType(Ident ident) {
		super("enum type", ident);
	}

	/** Add teh given item to a this enum type and autoenumerate it. */
	public void addItem(EnumItem item) {
		items.add(item);
	}

	/** @return A list with the identifiers in the enum type. */
	public List<EnumItem> getItems() {
		return Collections.unmodifiableList(items);
	}

	/** @see de.unika.ipd.grgen.ir.Type#classify() */
	public int classify() {
		return IS_INTEGER;
	}

	protected void canonicalizeLocal() {
		Collections.sort(items, Identifiable.COMPARATOR);
	}

	void addToDigest(StringBuffer sb) {
		sb.append(this);
		sb.append('[');

		int i = 0;
		for(Iterator<EnumItem> it = items.iterator(); it.hasNext(); i++) {
			EnumItem ent = (EnumItem) it.next();
			if(i > 0)
				sb.append(',');
			sb.append(ent);
		}

		sb.append(']');
	}
}
