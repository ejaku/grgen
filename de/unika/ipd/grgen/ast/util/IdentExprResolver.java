/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentExprNode;

/**
 * Resolves the ident node behind an ident expr node.
 */
public class IdentExprResolver implements Resolver {

	private Resolver resolver;

  /**
   * 
   */
  public IdentExprResolver(Resolver resolver) {
		this.resolver = resolver;
  }

  /**
   * @see de.unika.ipd.grgen.ast.util.Resolver#resolve(de.unika.ipd.grgen.ast.BaseNode, int)
   */
  public boolean resolve(BaseNode node, int child) {
  	boolean res = false;
  	BaseNode c = node.getChild(child);
  	
  	if(c instanceof IdentExprNode) {
  		IdentExprNode e = (IdentExprNode) c;
			node.replaceChild(child, e.getIdent());
			res = resolver.resolve(node, child);
  	}
  	
  	return res;
  }

}
