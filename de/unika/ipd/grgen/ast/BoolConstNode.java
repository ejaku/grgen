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

  /**
   * @see de.unika.ipd.grgen.ast.ConstNode#doCastTo(de.unika.ipd.grgen.ast.TypeNode)
   */
  protected ConstNode doCastTo(TypeNode type) {
  	boolean value = ((Boolean) getValue()).booleanValue();
  	ConstNode res = ConstNode.getInvalid();
  	
  	if(type.isEqual(BasicTypeNode.intType))
  		res = new IntConstNode(getCoords(), value ? 1 : 0);
  	else if(type.isEqual(BasicTypeNode.stringType)) 
  		res = new StringConstNode(getCoords(), "" + value);
  		
  	return res;
  }

}
