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
		
	/**
	 * Make a new node.
	 * @param ident The identifier for the node.
	 * @param type The type of the node.
	 */
	public Node(Ident ident, NodeType type, Annotations annots) {
		super("node", ident, type, annots);
		this.type = type;
	}

	/**
	 * Make a new node.
	 * @param ident The identifier for the node.
	 * @param type The type of the node.
	 */
	public Node(Ident ident, NodeType type) {
		this(ident, type, EmptyAnnotations.get());
	}
	
	/**
	 * Get the type of the node.
	 * @return The type of the node.
	 */
	public NodeType getNodeType() {
		return type;
	}

	/**
	 * Get the node from which this node inherits its dynamic type
	 */
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
}
