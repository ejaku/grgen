/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A string type.
 */
public class StringType extends PrimitiveType {

  /**
   * @param ident The name of the string type.
   */
  public StringType(Ident ident) {
    super("string type", ident);
  }

	/**
	 * @see de.unika.ipd.grgen.ir.Type#classify()
	 */
	public int classify() {
		return IS_STRING;
	}

}
