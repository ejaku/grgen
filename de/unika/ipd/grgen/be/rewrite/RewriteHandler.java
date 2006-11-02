/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * Created on Mar 10, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.rewrite;

import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Rule;
import java.util.Collection;
import java.util.Map;


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
	void insertNodes(Collection<Node> nodes);
	
	/**
	 * Generate code to delete all nodes given in the collection.
	 * @param nodes A collection of nodes of a rule.
	 */
	void deleteNodes(Collection<Node> nodes);
	
	/**
	 * Generate code to change the type of several nodes.
	 * The map's key elements are nodes of a rule. The value objects are node types.
 	 * A mapping from node <code>n</code> to node type <code>nt</code> indicates
 	 * that the type of the node shall be changed to <code>nt</code>.
	 * @param nodeTypeMap The node, node type map.
	 */
	void changeNodeTypes(Map<Node, Object> nodeTypeMap);
	
	/**
	 * Generate code that deletes incident edges of nodes.
	 * @param The set containing nodes.
	 */
	void deleteEdgesOfNodes(Collection<Node> nodes);

	/**
	 * Generate code to delete all edges in the set.
	 * @param edges A collection of edges.
	 */
	void deleteEdges(Collection<Edge> edges);
	
	/**
	 * Generate code to insert all edges in the collection.
	 * @param edges A collection of edges.
	 */
	void insertEdges(Collection<Edge> edges);

	/**
	 * Generate an eval statement for some assignments.
	 * @param assigns A collection of assignments.
	 */
	void generateEvals(Collection<Assignment> assigns);
	
	/**
	 * Get the class of the required rewrite generator.
	 * It may also be a superclass of it.
	 * @return The required rewrite generator.
	 */
	Class<SPORewriteGenerator> getRequiredRewriteGenerator();
	
	/**
	 * This method is called after all others.
	 */
	void finish();
}
