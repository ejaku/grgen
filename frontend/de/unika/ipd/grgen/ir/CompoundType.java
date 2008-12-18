/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

/**
 * Abstract base class for compound types containing members.
 */
public abstract class CompoundType extends Type {

	/** Collection containing all members defined in that type. */
	private List<Entity> members = new LinkedList<Entity>();

	/**
	 * Make a new compound type.
	 * @param name The name of the type.
	 * @param ident The identifier used to declare this type.
	 */
	public CompoundType(String name, Ident ident) {
		super(name, ident);
	}

	/** Get all members of this compound type. */
	public Collection<Entity> getMembers() {
		return Collections.unmodifiableCollection(members);
	}

	/** Add a member entity to the compound type. */
	public void addMember(Entity member) {
		members.add(member);
		member.setOwner(this);
	}

	protected void canonicalizeLocal() {
		Collections.sort(members, Identifiable.COMPARATOR);
	}

	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("members", members.iterator());
	}

	void addToDigest(StringBuffer sb) {
		sb.append(this);
		sb.append('[');

		int i = 0;
		for(Iterator<Entity> it = members.iterator(); it.hasNext(); i++) {
			Entity ent = it.next();
			if(i > 0)
				sb.append(',');
			sb.append(ent);
		}

		sb.append(']');
	}
}
