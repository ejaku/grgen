/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Iterator;

import de.unika.ipd.grgen.util.SingleIterator;

/**
 * An instantiation of a type.
 */
public class Entity extends Identifiable {
	
	/** Type of the entity. */
	private Type type;

	/** The entity's owner. */
	private Type owner = Type.getGlobal();
	

  /**
   * Make a new entity of a given type
   * @param name The name of the entity.
   * @param ident The declaring identifier.
   * @param type The type used in the declaration.
   */
  protected Entity(String name, Ident ident, Type type) {
  	super(name, ident);
  	this.type = type;
  }
  
  public Entity(Ident ident, Type type) {
  	this("entity", ident, type);
  }
  
  /**
   * Get the type of the entity.
   * @return The entity's type.
   */
  public Type getType() {
  	return type;
  }
  
  /**
   * Only walkable child here is the type
   * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
   */
  public Iterator getWalkableChildren() {
  	return new SingleIterator(type);
  }
  
  /**
   * Get the owner of the entity.
   * @return The entity's owner.
   */
  public Type getOwner() {
    return owner;
  }

  /**
   * Set the owner of the entity.
   * This function is just called from other IR classes.
   * @param type The owner of the entity.
   */
  protected void setOwner(Type type) {
    owner = type;
  }

}
