/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * An integer constant.
 */
public class IntConstNode extends ConstNode {

  /**
   * @param coords The coordinates.
   * @param value The integer value
   */
  public IntConstNode(Coords coords, int v) {
    super(coords, new Integer(v));
  }
  
  public TypeNode getType() {
  	return BasicTypeNode.intType;
  }

}
