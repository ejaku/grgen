/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Vector;

/**
 * An expression node.
 */
public abstract class Expression extends IR {

	/** The operands of the expression. */
	protected Vector operands = new Vector();
	
	/** The type of the expression. */
	protected Type type;

  public Expression(String name, Type type) {
  	super(name);
  	this.type = type;
  }

	/** 
	 * Add an operand to the expression.
	 * @param e An operand.
	 */
	public void addOperand(Expression e) {
		operands.add(e);
	}

}
