/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * An expression appearing in conditions.
 */
public interface Term extends MetaBase {

	/**
	 * Get the number of operands.
	 * @return The number of operands.
	 */
	int operandCount();
	
	/**
	 * Get the operand at position <code>i</code>.
	 * @param i The position.
	 * @return The operand at position <code>i</code> or <code>null</code> if
	 * <code>i</code> is not in a valid range.
	 */
	Term getOperand(int i);
	
	/**
	 * Get the opcode of the term.
	 * @return The opcode.
	 */
	Op getOp();
	
}
