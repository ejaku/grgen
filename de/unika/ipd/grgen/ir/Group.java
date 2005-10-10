/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;

/**
 * A type representing a group.
 */
public class Group extends Identifiable {
	
	/** Set of the group's members (tests, rules, ...) */
	private Set<IR> members = new HashSet<IR>();
	
	public Group(Ident ident) {
		super("group", ident);
	}
	
	/**
	 * Add a member to a group.
	 * The member is an action, such as a test or a rule.
	 * @param act The action to add to the group.
	 */
	public void addMember(Action act) {
		members.add(act);
	}
	
	/**
	 * Get all actions declared in this group.
	 * @return All actions in the group.
	 */
	public Iterator<IR> getMembers() {
		return members.iterator();
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
	 */
	public Collection<IR> getWalkableChildren() {
		return members;
	}
	
}
