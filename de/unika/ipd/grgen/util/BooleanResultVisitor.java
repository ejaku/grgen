/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * A visitor that returns a boolean value.
 * They are occurring more often, so they're an own class. 
 */
public abstract class BooleanResultVisitor implements ResultVisitor {

	private boolean result;

  /**
   * Make a new one.
   * @param def The value, the result is initialized.
   */
  public BooleanResultVisitor(boolean init) {
  	result = init;
  }

	protected void setResult(boolean value) {
		result = value;
	}

  /**
   * @see de.unika.ipd.grgen.util.ResultVisitor#getResult()
   */
  public Object getResult() {
  	return new Boolean(result);
  }
  
  public boolean booleanResult() {
  	return result;
  }

}
