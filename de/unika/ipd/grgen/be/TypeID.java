/**
 * Created on Mar 10, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be;

import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.Type;


/**
 * Something that can give IDs for types.
 */
public interface TypeID {

	int getId(NodeType nt);
	
	int getId(EdgeType et);
	
	int getId(Type type, boolean forNode);
	
	short[][] getIsAMatrix(boolean forNode);
}
