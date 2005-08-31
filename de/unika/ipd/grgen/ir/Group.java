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
