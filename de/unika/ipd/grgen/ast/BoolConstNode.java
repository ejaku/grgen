/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * A boolean constant.
 */
public class BoolConstNode extends ConstNode {

  /**
   * @param coords The source code coordinates
   * @param value The value.
   */
  public BoolConstNode(Coords coords, boolean value) {
    super(coords, "boolean", new Boolean(value));
  }
  
  public TypeNode getType() {
  	return BasicTypeNode.booleanType;
  }

}
