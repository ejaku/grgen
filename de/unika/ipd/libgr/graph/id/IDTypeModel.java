/**
 * Created on Mar 8, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.graph.id;


/**
 * A type model that uses IDs. 
 */
public interface IDTypeModel {

	int[] getNodeTypeSuperTypes(int node);
	int[] getEdgeTypeSuperTypes(int edge);
	
	int getNodeRootType();
	int getEdgeRootType();
	
	boolean nodeTypeIsA(int n1, int n2);
	boolean edgeTypeIsA(int e1, int e2);	

}
