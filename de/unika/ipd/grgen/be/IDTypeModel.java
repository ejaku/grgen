/**
 * Created on Mar 8, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be;

import de.unika.ipd.grgen.be.TypeID;


/**
 * A type model that uses IDs.
 */
public interface IDTypeModel extends TypeID {

	String getTypeName(boolean forNode, int obj);

	int[] getSuperTypes(boolean forNode, int obj);
	
	int[] getSubTypes(boolean forNode, int obj);

	int getRootType(boolean forNode);

	int[] getIDs(boolean forNode);
}
