/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import java.util.Collection;
import java.util.LinkedList;

import de.unika.ipd.grgen.ast.BaseNode;

/**
 * A resolver, that tries out some other resolvers on a node.
 * It succeeds, if one of the resolvers is successful.
 * All Resolvers are checked out in the order, the appear in the given 
 * array or collection.
 */
public class OneOfResolver extends Resolver {

	private Resolver[] resolvers;
	
	private Collection errorMsgs = new LinkedList();

	public OneOfResolver(Collection resolvers) {
		this((Resolver[]) resolvers.toArray());
	}
	
	public OneOfResolver(Resolver[] resolvers) {
		this.resolvers = resolvers;
		
		// Let the subresolvers emit their error messages to this resolver's
		// message queue.
		for(int i = 0; i < resolvers.length; i++) 
			resolvers[i].setErrorQueue(errorMsgs);
		
		setErrorQueue(errorMsgs);
	}

  /**
   * @see de.unika.ipd.grgen.ast.util.Resolver#resolve(de.unika.ipd.grgen.ast.BaseNode, int)
   */
  public boolean resolve(BaseNode node, int child) {
  	
  	boolean res = false;
  	
  	for(int i = 0; i < resolvers.length; i++) {
  		if(resolvers[i].resolve(node, child)) {
  			errorMsgs.clear();
  			res = true;
  			break;
  		}
  	}
  	
  	return res;
  }

}
