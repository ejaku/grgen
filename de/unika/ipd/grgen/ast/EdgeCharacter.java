/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.Edge;

/**
 * Something, that looks like an edge.
 */
public interface EdgeCharacter {

	/** 
	 * Get the IR edge for this AST edge.
	 * @return The IR edge.
	 */
	Edge getEdge();
	
}
