/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;

/**
 * A resolver, that resolves a declaration node from an identifier.
 */
public class DeclResolver extends IdentResolver {

	/**
	 * Make a new declaration resolver.
	 * @param classes A list of classes, the resolved node must be
	 * instance of. 
	 */
	public DeclResolver(Class[] classes) {
		super(classes);
	}
	
	/**
	 * Just a convenience constructor for {@link #DeclResolver(Class[])}
	 */
	public DeclResolver(Class cls) {
		super(new Class[] { cls });
	}

  /**
   * @see de.unika.ipd.grgen.ast.check.Resolver#resolve()
   */
  protected BaseNode resolveIdent(IdentNode n) {
		return n.getDecl();
  }

}
