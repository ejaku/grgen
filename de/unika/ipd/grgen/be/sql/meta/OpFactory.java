/**
 * Created on Apr 13, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * An operator factory.
 */
public interface OpFactory {

	/**
	 * Get the operator for a specific opcode.
	 * @param opcode The opcode.
	 * @return The operator.
	 */
	Op getOp(int opcode);
	
}
