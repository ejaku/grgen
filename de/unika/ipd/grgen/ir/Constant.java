/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * Abstract base class for all constants.
 */
public class Constant extends Expression {

	/** The value of the constant. */ 
	protected Object value; 

  /**
   * @param name The name of the constant.
   * @param type The type of the constant.
   */
  public Constant(String name, Type type, Object value) {
    super(name, type);
  	this.value = value;
  }
  
  protected Object getValue() {
  	return value;
  }
}
