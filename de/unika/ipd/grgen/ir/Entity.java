/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.Attributed;
import de.unika.ipd.grgen.util.Attributes;
import de.unika.ipd.grgen.util.EmptyAttributes;
import de.unika.ipd.grgen.util.SingleIterator;
import java.util.Iterator;
import java.util.Map;

/**
 * An instantiation of a type.
 */
public class Entity extends Identifiable implements Attributed {
	
	private static final String[] childrenNames = { "type" };
	
	/** Type of the entity. */
	private final Type type;

	/** The entity's owner. */
	private Type owner = Type.getGlobal();
	
	/** The attributes of this entity. */
	private final Attributes attributes;

  /**
   * Make a new entity of a given type
   * @param name The name of the entity.
   * @param ident The declaring identifier.
   * @param type The type used in the declaration.
   */
  protected Entity(String name, Ident ident, Type type, Attributes attr) {
  	super(name, ident);
		setChildrenNames(childrenNames);
  	this.type = type;
		this.attributes = attr;
  }
  
  public Entity(Ident ident, Type type, Attributes attr) {
  	this("entity", ident, type, attr);
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
	
	/**
	 * Get the attributes.
	 * @return The atttributes.
	 */
	public Attributes getAttributes() {
		return attributes;
	}
	
	public void addFields(Map fields) {
		super.addFields(fields);
		fields.put("type", new SingleIterator(type));
		fields.put("owner", new SingleIterator(owner));
	}
	
}
