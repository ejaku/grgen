/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * An integer type. 
 */
public class IntType extends PrimitiveType {

  public IntType(Ident ident) {
    super("integer type", ident);
  }

	/**
	 * @see de.unika.ipd.grgen.ir.Type#classify()
	 */
	public int classify() {
		return IS_INTEGER;
	}

}
