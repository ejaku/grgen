/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

/**
 * An enumeration type.
 */
public class EnumType extends PrimitiveType {

	private List items = new LinkedList();

  /**
   * Make a new enum type.
   * @param ident
   */
  public EnumType(Ident ident) {
    super("enum type", ident);
  }

	/**
	 * Add an item to a this enum type.
	 * @param name The identifier of the enum item.
	 */
	public void addItem(Ident name) {
		items.add(name);
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

}
