/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A graph action.
 */
public abstract class Action extends Identifiable {

  /**
   * @param name
   */
  public Action(String name, Ident ident) {
    super(name, ident);
  }
  
}
