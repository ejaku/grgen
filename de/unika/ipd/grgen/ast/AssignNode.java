/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * An expression node, denoting an assignment.
 */
public class AssignNode extends ExprNode {

  /**
   * @param coords The source code coordinates of = operator.
   * @param qual The left hand side.
   * @param expr The expression, that is assigned. 
   */
  public AssignNode(Coords coords, BaseNode qual, BaseNode expr) {
    super(coords);
    addChild(qual);
    addChild(expr);
  }

  /**
   * Assignments have always the void type.  
   * @see de.unika.ipd.grgen.ast.ExprNode#getType()
   */
  public TypeNode getType() {
  	return BasicTypeNode.voidType;
  }

}
