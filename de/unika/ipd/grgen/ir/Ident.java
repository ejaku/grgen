/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.HashMap;

import de.unika.ipd.grgen.parser.Coords;

/**
 * A class representing an identifier.
 */
public class Ident extends IR implements Comparable {

	private static final String defaultNamespace = "<default>";
	
	/** Symbol table recording all identifiers. */
	private static HashMap identifiers = new HashMap();

	/** Text of the identifier */
	private final String text;
	
	/** The namespace the identifier was defined in. */
	private final String nameSpace;
	
	/** location of the definition of the identifier */
	private final Coords def;
	
  /**
   * New Identifier.
   * @param text The text of the identifier.
	 * @param nameSpace The namespace of the identifier.
   * @param def The location of the definition of the identifier.
   */
  private Ident(String text, String nameSpace, Coords def) {
    super("ident");
    this.text = text;
		this.nameSpace = nameSpace;
    this.def = def;
  }
	
  /**
   * New Identifier.
   * @param text The text of the identifier.
   * @param def The location of the definition of the identifier.
   */
	private Ident(String text, Coords def) {
		this(text, defaultNamespace, def);
	}
  
  /**
   * The string of an identifier is its text.
   * @see java.lang.Object#toString()
   */
  public String toString() {
  	return text;
  }

	/**
	 * Get the location, where the identifier was defined.
	 * @return The location of the identifier's definition.
	 */
	public Coords getCoords() {
		return def;
	}
  
	/**
	 * @see java.lang.Object#equals(java.lang.Object)
	 * Two identifiers are equal, if they have the same names and
	 * the same location of definition.
	 */
  public boolean equals(Object obj) {
  	boolean res = false;
  	if(obj instanceof Ident) {
  		Ident id = (Ident) obj;
  		res = text.equals(id.text) && nameSpace.equals(id.nameSpace);
  	}
  	return res;
  }
  
  /**
   * Identifier factory.
   * Use this to get a new Identifier using a string and a location
   * @param text The text of the identifier.
	 * @param nameSpace The name space the identifier was defined in.
   * @param loc The location of the identifier.
   * @return The IR identifier object for the desired identifier.
   */
  public static Ident get(String text, String nameSpace, Coords loc) {
  	String key = text + "#" + loc.toString();
  	Ident res;
  	
  	if(identifiers.containsKey(key))
  		res = (Ident) identifiers.get(key);
  	else {
  		res = new Ident(text, nameSpace, loc);
  		identifiers.put(key, res);
  	}
  	return res;
  }

	/**
	 * Identifier factory.
	 * Use this function to achieve the same as {@link #get(String, Location)}
	 * without a location.
	 * @param text The text of the identifier.
	 * @return The IR identifier object for the desired identifier.
	 */
	public static Ident get(String text, String nameSpace) {
		return get(text, nameSpace, Coords.getInvalid());
	}
	
	public static Ident get(String text) {
		return get(text, defaultNamespace, Coords.getInvalid());
	}
	

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeInfo()
   */
  public String getNodeInfo() {
    return super.getNodeInfo() + "\nCoords: " + def;
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel()
   */
  public String getNodeLabel() {
    return getName() + " " + text;
  }
	
	/**
	 * Compare an identifier to another.
	 * @param obj The other identifier.
	 * @return -1, 0, 1, respectively.
	 */
	public int compareTo(Object obj) {
		Ident id = (Ident) obj;
		return toString().compareTo(id.toString());
	}

}
