/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A Primitive type. 
 */
public class PrimitiveType extends Type {

  /**
   * Make a new primitive type.
   * @param name Name of the primitive type.
   */
  public PrimitiveType(String name, Ident ident) {
    super(name, ident);
  }

}
