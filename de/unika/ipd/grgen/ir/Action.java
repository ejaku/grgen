/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A graph action.
 */
public abstract class Action extends Identifiable {

	/** The group this action is belonging to. */
	private Group group;

  /**
   * @param name
   */
  public Action(String name, Ident ident, Group group) {
    super(name, ident);
  }
  
  /**
   * Get the group this action is belonging to.
   * @return The group, that owns this action.
   */
  public Group getGroup() {
  	return group;
  }

}
