/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * An anonymous edge.
 */
public class AnonymousEdge extends Edge {

  /**
   * @param ident The identifier (here will be generated one, since
   * the edge is anonymous).
   * @param type The edge type.
   */
  public AnonymousEdge(Ident ident, EdgeType type) {
    super(ident, type);
  }

  /**
   * @see de.unika.ipd.grgen.ir.Edge#isAnonymous()
   */
  public boolean isAnonymous() {
    return true;
  }

}
