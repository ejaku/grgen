/**
 * Created on Mar 10, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.rewrite;

import de.unika.ipd.grgen.ir.Rule;


/**
 * A class that handles rewriting.
 */
public interface RewriteGenerator {

	void rewrite(Rule r, RewriteHandler handler);
	
}
