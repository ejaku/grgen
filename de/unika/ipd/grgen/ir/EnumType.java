/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import de.unika.ipd.grgen.ir.EnumValue;

/**
 * An enumeration type.
 */
public class EnumType extends PrimitiveType {

	private List items = new LinkedList();
	private int next_id = 0;

  /**
   * Make a new enum type.
   * @param ident The identifier of this enumeration.
   */
  public EnumType(Ident ident) {
    super("enum type", ident);
  }

	/**
	 * Add an item to a this enum type and autoenumerate it.
	 * @param name The identifier of the enum item.
	 */
	public void addItem(Ident name) {
		items.add(new EnumValue(name, getNextEnumId()));
	}
	
	/**
	 * Add an item to a this enum type.
	 * @param name  The identifier of the enum item.
	 * @param value The value of the enum item.
	 */
	public void addItem(Ident name, int value) {
		items.add(new EnumValue(name, value));
		setNextEnumId(value+1);
	}
		
	/**
	 * Return iterator of all identifiers in the enum type.
	 * @return An iterator with idents.
	 */
	public Iterator getItems() {
		return items.iterator();
	}

  /**
   * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
   */
  public Iterator getWalkableChildren() {
    return items.iterator();
  }

	/**
	 * Return the next id for an enum value and autoincrement
	 * this value.
	 * @return The next ID value.
	 */
	public int getNextEnumId() {
		return next_id++;
	}
	
	/**
	 * Sets the next ID for an enum value.
	 * @param next_id
	 */
	public void setNextEnumId(int next_id)
	{
		this.next_id = next_id; 
	}
}
