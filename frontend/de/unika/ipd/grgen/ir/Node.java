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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.Annotations;
import de.unika.ipd.grgen.util.EmptyAnnotations;

/**
 * A node in a graph.
 */
public class Node extends GraphEntity {

	/** Type of the node. */
	protected final NodeType type;

	/** Point of definition, that is the pattern graph the node was defined in*/
	protected PatternGraph pointOfDefinition;

	/**
	 * Make a new node.
	 * @param ident The identifier for the node.
	 * @param type The type of the node.
	 * @param annots The annotations of this node.
	 * @param maybeDeleted Indicates whether this element might be deleted due to homomorphy.
	 * @param maybeRetyped Indicates whether this element might be retyped due to homomorphy.
	 */
	public Node(Ident ident, NodeType type, Annotations annots, boolean maybeDeleted, boolean maybeRetyped) {
		super("node", ident, type, annots, maybeDeleted, maybeRetyped);
		this.type = type;
	}

	/**
	 * Make a new node.
	 * @param ident The identifier for the node.
	 * @param type The type of the node.
	 * @param maybeDeleted Indicates whether this element might be deleted due to homomorphy.
	 * @param maybeRetyped Indicates whether this element might be retyped due to homomorphy.
	 */
	public Node(Ident ident, NodeType type, boolean maybeDeleted, boolean maybeRetyped) {
		this(ident, type, EmptyAnnotations.get(), maybeDeleted, maybeRetyped);
	}

	/** @return The type of the node. */
	public NodeType getNodeType() {
		return type;
	}

	/** Get the node from which this node inherits its dynamic type */
	public Node getTypeof() {
		return (Node)typeof;
	}

	/**
	 * Sets the corresponding retyped version of this node
	 * @param retyped The retyped node
	 */
	public void setRetypedNode(Node retyped) {
		this.retyped = retyped;
	}

	/**
	 * Returns the corresponding retyped version of this node
	 * @return The retyped version or <code>null</code>
	 */
	public RetypedNode getRetypedNode() {
		return (RetypedNode)this.retyped;
	}

	public void setPointOfDefinition(PatternGraph pointOfDefinition) {
		assert this.pointOfDefinition==null && pointOfDefinition!=null;
		this.pointOfDefinition = pointOfDefinition;
	}

	public PatternGraph getPointOfDefinition() {
		return pointOfDefinition;
	}
}
