/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * An operator in an expression.
 */
public class Operator extends Expression {

  /**
   * @param type The type of the operator.
   */
  public Operator(PrimitiveType type) {
    super("operator", type);
  }

}
