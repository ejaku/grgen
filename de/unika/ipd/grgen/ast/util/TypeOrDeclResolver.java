/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;

/**
 * A resolver, that accepts a declaration or a type declaration for 
 * an identifier. 
 * In the latter case, a new declaration is created and returned. 
 */
public class TypeOrDeclResolver extends IdentResolver {

  /**
   * @param classes
   */
  public TypeOrDeclResolver(Class[] classes) {
    super(classes);
    // TODO Auto-generated constructor stub
  }

  /**
   * @see de.unika.ipd.grgen.ast.util.IdentResolver#resolveIdent(de.unika.ipd.grgen.ast.IdentNode)
   */
  protected BaseNode resolveIdent(IdentNode n) {
    // TODO Auto-generated method stub
    return null;
  }

}
