/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * A visitor that computes a result.
 */
public interface ResultVisitor extends Visitor {
	
	/** 
	 * Get the result, the visitor computed.
	 * @return The result
	 */ 
	Object getResult();
}
