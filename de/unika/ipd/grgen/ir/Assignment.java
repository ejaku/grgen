/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;


/**
 * Abstract base class for all constants.
 */
public class Assignment extends IR {
	
	/** The lhs of the assignment. */
	private Qualification target;
	
	/** The rhs of the assignment. */
	private Expression expr;
	
	public Assignment(Qualification target, Expression expr) {
		super("assignment");
		this.target = target;
		this.expr = expr;
	}
	
	public Qualification getTarget() {
		return target;
	}
	
	public Expression getExpression() {
		return expr;
	}
	
	public String toString() {
		return getTarget() + " = " + getExpression();
	}
	
}
