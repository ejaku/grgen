/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


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
	
	private Collection<ErrorMessage> errorMsgs = new LinkedList<ErrorMessage>();

	public OneOfResolver(Collection<? extends Resolver> resolvers) {
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
