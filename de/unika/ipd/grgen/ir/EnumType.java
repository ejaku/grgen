/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

/**
 * An enumeration type.
 */
public class EnumType extends PrimitiveType {
	
	private final List items = new LinkedList();
	
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
	public void addItem(EnumItem item) {
		items.add(item);
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
	 * @see de.unika.ipd.grgen.ir.Type#classify()
	 */
	public int classify() {
		return IS_INTEGER;
	}
	
	protected void canonicalizeLocal() {
		Collections.sort(items, Identifiable.COMPARATOR);
	}
	
	void addToDigest(StringBuffer sb) {
		sb.append(this);
		sb.append('[');
		
		int i = 0;
		for(Iterator it = items.iterator(); it.hasNext(); i++) {
			EnumItem ent = (EnumItem) it.next();
			if(i > 0)
				sb.append(',');
			sb.append(ent);
		}
		
		sb.append(']');
	}
	
}
