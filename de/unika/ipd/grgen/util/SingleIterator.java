/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * Iterator for iterating over just ine single element. 
 */
public class SingleIterator extends ArrayIterator {

  /**
   * Make a new single iterator 
   */
  public SingleIterator(Object obj) {
    super(new Object[] { obj });
  }
}
