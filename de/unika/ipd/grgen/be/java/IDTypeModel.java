/**
 * Created on Mar 8, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import de.unika.ipd.grgen.be.sql.TypeID;


/**
 * A type model that uses IDs.
 */
public interface IDTypeModel extends TypeID {

	String getNodeTypeName(int node);
	String getEdgeTypeName(int edge);

	int[] getNodeTypeSuperTypes(int node);
	int[] getEdgeTypeSuperTypes(int edge);
	
	int[] getNodeTypeSubTypes(int node);
	int[] getEdgeTypeSubTypes(int edge);

	int getNodeRootType();
	int getEdgeRootType();
	
	int[] getNodeTypeIsA(int n);
	int[] getEdgeTypeIsA(int e);
	
	boolean nodeTypeIsA(int n1, int n2);
	boolean edgeTypeIsA(int e1, int e2);

	int[] getNodeTypes();
	int[] getEdgeTypes();
	
}
