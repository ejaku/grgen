/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.Constant;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Constant expressions. 
 * A constant is 0-ary operator. 
 */
public abstract class ConstNode extends OpNode {

	/** The value of the constant. */
	protected Object value;
	
	/** A name for the constant. */
	protected String name;

  /**
   * @param coords The source code coordinates.
   */
  public ConstNode(Coords coords, String name, Object value) {
    super(coords, OperatorSignature.CONST);
    this.value = value;
    this.name = name;
  }
  
  /**
   * Include the constants value in its string representation.
   * @see java.lang.Object#toString()
   */
  public String toString() {
  	return super.toString() + " " + value.toString();
  }
  
  

  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
   */
  protected IR constructIR() {
  	return new Constant(name, getType().getType(), value);
  }

}
