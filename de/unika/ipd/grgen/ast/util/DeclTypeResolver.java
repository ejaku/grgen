/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.ast.IdentNode;

/**
 * Resolve the type of a type declaration.
 */
public class DeclTypeResolver extends IdentResolver {

	/**
	 * Make a new type decl resolver.
	 * @param classes An array of classes, the resolved node must be 
	 * instance of. E.g., If youe have a declaration declaring an 
	 * <code>BasicTypeNode</code> instance with "int", you can give
	 * <code>new TypeNode[] { BasicTypeNode }</code> as an argument.  
	 */
  public DeclTypeResolver(Class[] classes) {
    super(classes);
  }
  
  /**
   * A convenience function for {@link #DeclTypeResolver(Class[])}.
   * @param cls The class
   * @see #DeclTypeResolver(Class[]).
   */
  public DeclTypeResolver(Class cls) {
  	this(new Class[] { cls });
  }

  /**
   * @see de.unika.ipd.grgen.ast.check.Resolver#doResolve(de.unika.ipd.grgen.ast.IdentNode)
   */
  protected BaseNode resolveIdent(IdentNode n) {
  	BaseNode decl = n.getDecl();
  	BaseNode res = BaseNode.getErrorNode();

		if(decl instanceof DeclNode)
			res = ((DeclNode) decl).getDeclType();
		
		return res;
  }
}
