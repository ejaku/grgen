/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * Constant expressions. 
 * A constant is 0-ary operator. 
 */
public abstract class ConstNode extends OpNode {

	/** The value of the constant. */
	protected Object value;

  /**
   * @param coords The source code coordinates.
   */
  public ConstNode(Coords coords, Object value) {
    super(coords, Operator.CONST);
    this.value = value;
  }
  
  public String toString() {
  	return super.toString() + " " + value.toString();
  }

}
