/**
 * Created on Apr 8, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * An opcode.
 */
import java.io.PrintStream;

public interface Op {

	int INFINITE_ARITY = -1;
	
	/**
	 * Get the arity of an operator.
	 * @return The arity.
	 */
	int arity();
	
	/**
	 * Get the priority of an operator.
	 * @return The priority.
	 */
	int priority();
	
	/**
	 * Get a string representation of the operator.
	 * @return A string representation.
	 */
	String text();
	
	/**
	 * Dump the operator to a string buffer.
	 * @param sb The string buffer.
	 * @param operands Its operands.
	 * @return The string buffer.
	 */
	PrintStream dump(PrintStream ps, Term[] operands);
	
}
