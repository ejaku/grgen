/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;

/**
 * An class representing a node or an edge. 
 */
public abstract class CompoundType extends Type {

	/** 
	 * Collection containing all members. 
	 * The members must be of type Entity.
	 */
	protected Collection members;

  /**
   * Make a new compound type.
   * @param name The name of the type.
   * @param ident The identifier used to declare this type.
   */
  public CompoundType(String name, Ident ident) {
    super(name, ident);
    members = new LinkedList();
  }
	
  /**
   * Get all members of this compund type.
   * @return An iterator with all members.
   */
  public Iterator getMembers() {
  	return members.iterator();
  }

	/**
	 * Add a member to the compound type.
	 * @param member The entity to add.
	 */
	public void addMember(Entity member) {
		members.add(member);
		member.setOwner(this);
	}
  
  /**
   * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
   */
  public Iterator getWalkableChildren() {
  	return members.iterator();
  }
}
