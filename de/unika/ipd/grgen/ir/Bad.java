/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A bad IR element.
 * This used in case of an error.
 */
public class Bad extends IR {

  public Bad() {
    super("bad");
  }
  
  public boolean isBad() {
  	return true;
  }

}
