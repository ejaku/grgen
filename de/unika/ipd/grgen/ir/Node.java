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
  
  /** A set of nodes, that are homomorphic to this one. */
  private Set homomorphicNodes = new HashSet();
  
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
  public boolean isRetypedNode()
  {
	  return (oldNode!=null);
  }

	/**
	 * Add a node that is homomorphic to this one.
	 * It does not care, if you add <code>this</code>. This method adds
	 * this node to the homomorphic set of <code>n</code>.
	 *
	 * @param n Another node.
	 */
	public void addHomomorphic(Node n) {
		homomorphicNodes.add(n);
		n.homomorphicNodes.add(this);
	}
	
	/**
	 * Put all nodes, that are homomoprohic to this one in a given set.
	 * @param addTo The set to put them all into.
	 */
	public void getHomomorphic(Collection addTo) {
		addTo.addAll(homomorphicNodes);
	}
	
	/**
	 * Check, if a node may be just homomorphic and not isomorphic to this one
	 * @param n The other node.
	 * @return true, if <code>n</code> and this node can be identified by
	 * the matching morphism, false if not.
	 */
	public boolean isHomomorphic(Node n) {
		return homomorphicNodes.contains(n);
	}
}
