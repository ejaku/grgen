/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Iterator;

import de.unika.ipd.grgen.util.SingleIterator;

/**
 * Abstract base class for all constants.
 */
public class Constant extends Expression {

	/** The value of the constant. */
	protected Object value;

  /**
   * @param type The type of the constant.
	 * @param value The value of the constant.
   */
  public Constant(Type type, Object value) {
    super("constant", type);
  	this.value = value;
  }
  
  /**
   * Get the value of the constant.
   * @return The value.
   */
  public Object getValue() {
  	return value;
  }
  
  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel()
   */
  public String getNodeLabel() {
		return getName() + " " + value;
  }

}
