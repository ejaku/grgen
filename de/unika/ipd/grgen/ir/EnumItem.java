
package de.unika.ipd.grgen.ir;

import java.util.Iterator;

import de.unika.ipd.grgen.util.ArrayIterator;

/**
 * An enumeration value
 */
public class EnumItem extends IR	{
	private final Ident id;	
	private final Constant value;
	
	/**
	 * Make a new enumeration value.
	 * 
	 * @param id The enumeration item identifier.
	 * @param value The associated value.
	 */
	public EnumItem(Ident id, Constant value) {
		super("enum item");
		this.id = id;
		this.value = value;		
	}

	/**
	 * Returns the enum items identifier.
	 * @return The identifier of the enum item.
	 */
	public Ident getIdent() {
		return id;
	}
	
	/**
	  * The string of an enum item is its identifier's text.
	  * @see java.lang.Object#toString()
	  */
	public String toString() {
		return id.toString();	
	}
	
	/**
	 * Get the value of the enum item.
	 * @return The value.
	 */
	public Constant getValue() {
		return value;
	}
	
	
  /**
   * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
   */
  public Iterator getWalkableChildren() {
  	return new ArrayIterator(new Object[] { id, value });
  }

}
