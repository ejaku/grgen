/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;

import de.unika.ipd.grgen.parser.Coords;

/**
 * Base class for all expression nodes.
 */
public abstract class ExprNode extends BaseNode {

	static {
		setName(ExprNode.class, "expression");
	}

  /**
   * Make a new expression
   */
  public ExprNode(Coords coords) {
		super(coords);
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
   */
  public Color getNodeColor() {
		return Color.PINK;
  }
  
  /**
   * Get the type of the expression.
   * @return The type of this expression node.
   */
  public abstract TypeNode getType();
	
	/**
	 * Adjust the type of the expression.
	 * The type can be adjusted by inserting an implicit cast.
	 * @param type The type the expression should be adjusted to. It must be
	 * compatible with the type of the expression.
 	 * @return A new expression, that is of a valid type and represents
 	 * this expression, if <code>type</code> was compatible with the type of
 	 * this expression, an invalid expression otherwise (one of an error type).
	 */
	protected ExprNode adjustType(TypeNode type) {
		ExprNode res = ConstNode.getInvalid();
		
		if(getType().isEqual(type))
			res = this;
		else if(getType().isCompatibleTo(type)) 
			res = new CastNode(getCoords(), type, this);
			
		return res; 
	}
	
	/**
	 * Check, if the expression is constant.
	 * @return True, if the expression can be evaluated to a constant.
	 */
	public boolean isConstant() {
		return false;
	}
	
	/**
	 * Evaluate the expression, if it's constant.
	 * @return Return a valid constant, if the expression is constant, else
	 * an invalid constant (can be checked with {@link ConstNode#isValid()}.
	 */
	public final ConstNode evaluate() {
		if(isConstant()) 
			return eval();
		else
			return ConstNode.getInvalid();
	}
	
	/**
	 * This method is only called, if the expression is constant, so you don't
	 * have to check for it.
	 * @return The value of the expression.
	 */
	protected abstract ConstNode eval();

}
