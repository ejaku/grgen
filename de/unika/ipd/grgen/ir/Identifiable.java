/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * Identifier with an identifier.
 * This is a super clas for all classes which are associated with 
 * an identifier.
 */
public class Identifiable extends IR {

	/** The identifier */
	private Ident ident;
	
  /**
   * @param name The name of the IR class
   * @param ident The identifier associated with this IR object. 
   */
  public Identifiable(String name, Ident ident) {
    super(name);
    this.ident = ident;
  }

  /**
   * @return The identifier that identifies this IR structure.
   */
  public Ident getIdent() {
    return ident;
  }

  /**
   * Set the identifier for this object.
   * @param ident The identifier.
   */
  public void setIdent(Ident ident) {
    this.ident = ident;
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel()
   */
  public String getNodeLabel() {
    return toString();
  }

	public String toString() {
		return getName() + " " + ident;
	}
}
