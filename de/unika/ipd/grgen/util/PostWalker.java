/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * Just a post walker 
 */
public class PostWalker extends PrePostWalker {

  /**
   * @param post The visitor to call after the descent
   */
  public PostWalker(Visitor post) {
    super(new Visitor() {
    	public void visit(Walkable w) {
    	}
    }, post);
  }
}
