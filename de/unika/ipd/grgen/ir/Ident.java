/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.util.Attributed;
import de.unika.ipd.grgen.util.Attributes;
import de.unika.ipd.grgen.util.EmptyAttributes;
import java.util.HashMap;

/**
 * A class representing an identifier.
 */
public class Ident extends IR implements Comparable, Attributed {
	
	private static final String defaultScope = "<default>";
	
	/** Symbol table recording all identifiers. */
	private static HashMap identifiers = new HashMap();

	/** Text of the identifier */
	private final String text;
	
	/** The scope/namespace the identifier was defined in. */
	private final String scope;
	
	/** location of the definition of the identifier */
	private final Coords def;
	
	/** The attributes for the identifier. */
	private final Attributes attrs;
	
	/** A precomputed hash code. */
	private final int precomputedHashCode;
	
  /**
   * New Identifier.
   * @param text The text of the identifier.
	 * @param scope The scope/namespace of the identifier.
   * @param def The location of the definition of the identifier.
	 * @param attrs The attributes of this identifier (Each identifier
	 * can carry several attributes which serve as meta information
	 * usable by backend components).
   */
  private Ident(String text, String scope, Coords def, Attributes attrs) {
    super("ident");
    this.text = text;
		this.scope = scope;
    this.def = def;
		this.attrs = attrs;
		this.precomputedHashCode = (scope + ":" + text).hashCode();
  }
	
  /**
   * New Identifier.
   * @param text The text of the identifier.
   * @param def The location of the definition of the identifier.
   */
	private Ident(String text, Coords def, Attributes attrs) {
		this(text, defaultScope, def, attrs);
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
  		res = text.equals(id.text) && scope.equals(id.scope);
  	}
  	return res;
  }
  
  /**
   * Identifier factory.
   * Use this to get a new Identifier using a string and a location
   * @param text The text of the identifier.
	 * @param scope The scope/namespace the identifier was defined in.
   * @param loc The location of the identifier.
	 * @param attrs The attributes of this identifier.
   * @return The IR identifier object for the desired identifier.
   */
  public static Ident get(String text, String scope, Coords loc,
													Attributes attrs) {
  	String key = text + "#" + loc.toString();
  	Ident res;
  	
  	if(identifiers.containsKey(key))
  		res = (Ident) identifiers.get(key);
  	else {
  		res = new Ident(text, scope, loc, attrs);
  		identifiers.put(key, res);
  	}
  	return res;
  }

	/**
	 * Identifier factory.
	 * Use this function to achieve the same as {@link #get(String, Location)}
	 * without a location.
	 * @param text The text of the identifier.
	 * @param scope The scope/namespace.
	 * @param attrs The attributes of this identifier.
	 * @return The IR identifier object for the desired identifier.
	 */
	public static Ident get(String text, String scope, Attributes attrs) {
		return get(text, scope, Coords.getInvalid(), attrs);
	}
	
	public static Ident get(String text) {
		return get(text, defaultScope, Coords.getInvalid(),
							 EmptyAttributes.get());
	}
	

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeInfo()
   */
  public String getNodeInfo() {
    return super.getNodeInfo() + "\nCoords: " + def
			+ "\nScope: " + scope;
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
	
	public int hashCode() {
		return precomputedHashCode;
	}

	/**
	 * Get the attributes.
	 * @return The atttributes.
	 */
	public Attributes getAttributes() {
		return attrs;
	}

}
