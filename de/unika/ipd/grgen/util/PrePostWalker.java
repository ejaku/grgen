/**
 * @file PostWalker.java
 * @author shack
 * @date Jul 20, 2003
 */
package de.unika.ipd.grgen.util;

import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;
import de.unika.ipd.grgen.ast.BaseNode;


/**
 * A walker calling visitors before descding to the children and after
 * returning from the descent.
 */
public class PrePostWalker extends Base implements Walker {

	private Set<Walkable> visited;
	private Visitor pre, post;

  /**
   * @param pre Visitor called before descending to children
   * @param post Visitor called after returning from descend
   */
  public PrePostWalker(Visitor pre, Visitor post) {
  	this.pre = pre;
  	this.post = post;
  	visited = new HashSet<Walkable>();
  }

	public void reset() {
		visited.clear();
	}
	
	public void walk(Walkable node) {
		int i = 0;

		if(!visited.contains(node)) {
			visited.add(node);
			pre.visit(node);

      for(Walkable p : node.getWalkableChildren()) {
        walk(p);
      }
      post.visit(node);
		}
	}
}
