/**
 * Created on Mar 8, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.graph.id;


/**
 * A graph model that represents nodes, edges, node types and edge types as IDs.
 */
public interface IDGraphModel extends IDTypeModel {

	int add(int nodeType);
	int add(int edgeType, int srcNode, int tgtNode);
	
	boolean removeNode(int node);
	boolean removeEdge(int edge);
	
	int[] getIncoming(int node);
	int[] getOutgoing(int node);
	
	int getSource(int edge);
	int getTarget(int edge);
	
	int getTypeOfNode(int node);
	int getTypeOfEdge(int edge);

}
