/**
 * Created on Mar 10, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.rewrite;

import java.util.Collection;
import java.util.Map;

import de.unika.ipd.grgen.ir.Rule;


/**
 * Something that can rewrite. The methods are called from 
 * {@link RewriteGenerator#rewrite(Rule, RewriteHandler)}. 
 * 
 * The arguments of (nearly) all methods are collections that contain either nodes
 * or edges of a specific rule. A call to method indicates, that code for the action
 * denoted by the method's name shall be emitted.
 *
 * Generally, you cannot be sure which method is called when (besides {@link #start(Rule)} 
 * and {@link #finish()} which are called first and last respectively). Some
 * {@link RewriteGenerator}s can assure a calling sequence. 
 */
public interface RewriteHandler {

	/**
	 * This method is called before any of the others.
	 * @param rule The rule to generate the rewrite sequence for.
	 */
	void start(Rule rule);
	
	/**
	 * Generate code to insert all nodes in the given collection.
	 * @param nodes A collection of nodes of a rule.
	 */
	void insertNodes(Collection nodes);
	
	/**
	 * Generate code to delete all nodes given in the collection.
	 * @param nodes A collection of nodes of a rule.
	 */
	void deleteNodes(Collection nodes);
	
	/**
	 * Generate code to change the type of several nodes.
	 * The map's key elements are nodes of a rule. The value objects are node types.
 	 * A mapping from node <code>n</code> to node type <code>nt</code> indicates
 	 * that the type of the node shall be changed to <code>nt</code>. 
	 * @param nodeTypeMap The node, node type map.
	 */
	void changeNodeTypes(Map nodeTypeMap);
	
	void deleteEdgesOfNodes(Collection nodes);
	void deleteEdges(Collection edges);
	void insertEdges(Collection edges);

	/**
	 * Get the class of the required rewrite generator. 
	 * It may also be a superclass of it.
	 * @return The required rewrite generator.
	 */
	Class getRequiredRewriteGenerator();
	
	/**
	 * This method is called after all others.
	 */
	void finish();
}
