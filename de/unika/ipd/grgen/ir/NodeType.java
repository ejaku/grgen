/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * IR class that represents node types.
 */
public class NodeType extends InheritanceType {

  /**
   * Make a new node type.
   * @param ident The identifier that declares this type.
	 * @param modifiers The modifiers for this type. (See the super
	 * class for these.)
   */
  public NodeType(Ident ident, int modifiers) {
    super("node type", ident, modifiers);
  }

}
