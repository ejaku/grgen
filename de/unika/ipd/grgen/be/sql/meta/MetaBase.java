/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;

import de.unika.ipd.grgen.util.StreamDumpable;
import java.io.PrintStream;



/**
 * Base for each SQL meta construct.
 */
public interface MetaBase extends StreamDumpable {

	/**
	 * Print the meta construct.
	 * @param ps The print stream.
	 */
	void dump(PrintStream ps);
	
	/**
	 * Get some special debug info for this object.
	 * This is mostly verbose stuff for dumping.
	 * @return Debug info.
	 */
	String debugInfo();
	
}
