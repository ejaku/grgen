/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;

/**
 * A unit with all declared entities 
 */
public class Unit extends Identifiable {

	/** All entities in this unit */
	private Collection members = new LinkedList();
	
	/** The group entities in the unit. */
	private Collection groups = new LinkedList();
	
	/** The declared types in the unit. */
	private Collection types = new LinkedList();
	
	private Collection actions = new LinkedList(); 
	
	/** The source filename of this unit. */
	private String filename;

	public Unit(Ident ident, String filename) {
		super("unit", ident);
	}
	
	/**
	 * Add entity to the unit.
	 * An entity as a declrared object which has a type, 
	 * such as a group, a test, etc.
	 * @param ent The entitiy to add 
	 */
	public void addMember(IR member) {
		members.add(member);
		if(member instanceof Action)
			groups.add(member);
		if(member instanceof Group)
			groups.add(member);
		if(member instanceof Type)
			types.add(member);
	}
	
	/** 
	 * Get all entities declared in this unit
	 * @return An iterator iterating over all entities.
	 */
	public Iterator getMembers() {
		return members.iterator();
	}
	
	/**
	 * Get all groups in the unit.
	 * @return An iterator iterating over all groups in the unit.
	 */
	public Iterator getGroups() {
		return groups.iterator();
	}
	
	public Iterator getActions() {
		return actions.iterator();
	}
	
	/** Get all declared types in the unit.
	 * @return An iterator iterating over all declared types in the unit.
	 */
	public Iterator getTypes() {
		return types.iterator();
	}
	
  /**
   * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
   */
  public Iterator getWalkableChildren() {
    return members.iterator();
  }

  /**
   * Get the source filename corresponding to this unit.
   * @return The source filename.
   */
  public String getFilename() {
    return filename;
  }

}
