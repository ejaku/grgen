/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * A string constant. 
 */
public class StringConstNode extends ConstNode {

  /**
   * @param coords The source code coordinates
   * @param value The string
   */
  public StringConstNode(Coords coords, String value) {
    super(coords, "string", value);
  }

	public TypeNode getType() {
		return BasicTypeNode.stringType;
	}
	
  /**
   * @see de.unika.ipd.grgen.ast.ConstNode#doCastTo(de.unika.ipd.grgen.ast.TypeNode)
   */
  protected ConstNode doCastTo(TypeNode type) {
    return ConstNode.getInvalid();
  }

}
