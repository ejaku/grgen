
package de.unika.ipd.grgen.ir;



/**
 * An enumeration value
 */
import java.util.*;

public class EnumItem extends Identifiable {
	private final Ident id;
	
	private final Constant value;
	
	/**
	 * Make a new enumeration value.
	 *
	 * @param id The enumeration item identifier.
	 * @param value The associated value.
	 */
	public EnumItem(Ident id, Constant value) {
		super("enum item", id);
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
  public Collection<IR> getWalkableChildren() {
		Set<IR> res = new HashSet<IR>();
		res.add(id);
		res.add(value);
		return res;
  }
	
}
