/**
 * Created on Apr 18, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * Something that is declared.
 *
 */
import java.io.PrintStream;

public interface Declared {

	/**
	 * The name with which the object was declared with.
	 * @return The declaration name.
	 */
	String getDeclName();
	
	/**
	 * Dump the declaration.
	 * @param ps The print stream.
	 * @return The print stream.
	 */
	PrintStream dumpDecl(PrintStream ps);
	
	
}
