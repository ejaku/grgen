/**
 * ObjectType.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.ir;

public class ObjectType extends PrimitiveType
{

  /**
   * @param ident The name of the boolean type.
   */
  public ObjectType(Ident ident) {
    super("object type", ident);
  }

	/**
	 * @see de.unika.ipd.grgen.ir.Type#classify()
	 */
	public int classify() {
		return IS_OBJECT;
	}
	
}

