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

import java.util.Collection;
import java.util.HashSet;
import java.util.Set;

import de.unika.ipd.grgen.util.Attributes;

/**
 * A node in a graph.
 */
public class Node extends ConstraintEntity {
	
	/** The retyped node with the type the node will have after a rule has been applied. */
	private Node retypedNode;
	
	/**  The original node if this is a retyped Node */
	private Node oldNode;
	
	/**
	 * Make a new node.
	 * @param ident The identifier that declared the node.
	 * @param type The node type of the node.
	 */
	//  public Node(Ident ident, NodeType type) {
//		this(ident, type, EmptyAttributes.get());
	//  }
	
	/**
	 * Make a new node.
	 * @param ident The identifier that declared the node.
	 * @param type The node type of the node.
	 * @param attr Some attributes.
	 */
	public Node(Ident ident, NodeType type, Attributes attr) {
		super("node", ident, type, attr);
		this.retypedNode = null;
		this.oldNode = null;
	}
	
	
	/**
	 * Get the type of the node.
	 * @return The type of the node.
	 */
	public NodeType getNodeType() {
		assert getType() instanceof NodeType : "type of node must be NodeType";
		return (NodeType) getType();
	}
	
	/**
	 * If the node changes its type then this will
	 * return the virtual retyped node.
	 *
	 * @return The retyped node
	 */
	public Node getRetypedNode() {
		return retypedNode;
	}
	
	/**
	 * Get the type of the node after a rule has finished.
	 * @return The post rule type of the node.
	 */
	public NodeType getReplaceType() {
		if(typeChanges()) {
			return retypedNode.getNodeType();
		} else {
			return getNodeType();
		}
	}
	
	/**
	 * Set the type that will become the new type of the node
	 * after a rule has been applied.
	 * @param retyped The retyped node with new type of the node.
	 */
	public void setRetypedNode(Node retyped) {
		retypedNode = retyped;
	}
	
	/**
	 * Check, if the type of this node changes in a rule.
	 * @return true, if the type changes, false, if not.
	 */
	public boolean typeChanges() {
		return (retypedNode!=null);
	}
	
	/**
	 * If this is a retyped node then this will return
	 * the original node in the graph.
	 *
	 * @return The retyped node
	 */
	public Node getOldNode() {
		return oldNode;
	}
	
	/**
	 * Set the original node in the graph if this one
	 * is a retyped one.
	 * @param old The new type of the node.
	 */
	public void setOldNode(Node old) {
		oldNode = old;
	}
	
	/**
	 * Check, whether this is a retyped ode.
	 * @return true, if this is a retyped node
	 */
	public boolean isRetypedNode() {
		return (oldNode!=null);
	}
}
