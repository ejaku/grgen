/**
 * Created on Mar 10, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be;

import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.NodeType;


/**
 * Something that can give IDs for types.
 */
public interface TypeID {

	int getId(NodeType nt);
	
	int getId(EdgeType et);
	
	short[][] getIsAMatrix(boolean forNode);
}
