/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;

/**
 * 
 */
public class CollectResolver extends Resolver {

  private Resolver resolver;

  public CollectResolver(Resolver resolver) {
    this.resolver = resolver;
  }

	public boolean resolve(BaseNode node, int pos) {
		boolean res = true;
		
		BaseNode c = node.getChild(pos);
		if(c instanceof CollectNode) {
			for(int i = 0; i < c.children(); i++)
				if(!resolver.resolve(c, i))
					res = false;
		} else
			reportError(node, "Expecting \"" + BaseNode.getName(CollectNode.class) 
				+ "\", found \"" + c.getName() + "\" instead.");
		
		return res;
	}

}
   