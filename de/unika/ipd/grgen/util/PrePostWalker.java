/**
 * @file PostWalker.java
 * @author shack
 * @date Jul 20, 2003
 */
package de.unika.ipd.grgen.util;

import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;


/**
 * A walker calling visitors before descding to the children and after
 * returning from the descent.
 */
public class PrePostWalker extends Base implements Walker {

	private Set visited;
	private Visitor pre, post;

  /**
   * @param pre Visitor called before descending to children
   * @param post Visitor called after returning from descend
   */
  public PrePostWalker(Visitor pre, Visitor post) {
  	this.pre = pre;
  	this.post = post;
  	visited = new HashSet();
  }

	public void reset() {
		visited.clear(); 
	}
	
	public void walk(Walkable node) {
		int i = 0;

		debug.entering();

		if(!visited.contains(node)) {
			visited.add(node);


			debug.report(8, "walking " + node);
			
			pre.visit(node);

      Iterator it = node.getWalkableChildren();
      while(it.hasNext()) {
        Walkable p = (Walkable) it.next();
        walk(p);
      }
      post.visit(node);
		}
		
		debug.leaving();
	}
}
