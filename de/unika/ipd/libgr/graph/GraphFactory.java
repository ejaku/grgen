/**
 * Created on Mar 11, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.graph;


/**
 * Someone that can create graphs.
 */
public interface GraphFactory {

	Graph getGraph(String name);
	
}
