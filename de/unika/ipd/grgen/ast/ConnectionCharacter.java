/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Set;

import de.unika.ipd.grgen.ir.Graph;

/**
 * Something that looks like a connection.
 * @see de.unika.ipd.grgen.ast.ConnectionNode
 */
public interface ConnectionCharacter {

	/**
	 * Add all nodes of this connection to a set.
	 * @param set The set.
	 */
	public void addNodes(Set set);
	
	/**
	 * Add all edges of this connection to a set.
	 * @param set The set.
	 */
	public void addEdge(Set set);

	public EdgeCharacter getEdge();

	public NodeDeclNode getSrc();
	
	public NodeDeclNode getTgt();
	
	/**
	 * Add this connection character to an IR graph.
	 * @param gr The IR graph.
	 */
	public void addToGraph(Graph gr);
	
}
