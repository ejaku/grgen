/*
 * Created on Dec 4, 2003
 *
 * To change the template for this generated file go to
 * Window&gt;Preferences&gt;Java&gt;Code Generation&gt;Code and Comments
 */
package de.unika.ipd.grgen.ir;

/**
 * An enumeration value
 */
public class EnumValue extends IR	{
	private final Ident id;	
	private final int value;	
	
	/**
	 * Make a new enumeration value.
	 * 
	 * @param id    The enumeration item identifier.
	 * @param value The associated value.
	 */
	public EnumValue(Ident id, int value) {
		super("enum value");
		this.id    = id;
		this.value = value;		
	}
	
	/**
	 * Returns the associated value.
	 * @return The enum items value.
	 */
	public int getEnumValue() {
		return value;
	}
	
	/**
	 * Returns the enum items identifier.
	 * @return The identifier of the enum item.
	 */
	public Ident getEnumIdent() {
		return id;
	}
	
	/**
	  * The string of an enum item is its identifier's text.
	  * @see java.lang.Object#toString()
	  */
	public String toString() {
		return id.toString();	
	}
}
