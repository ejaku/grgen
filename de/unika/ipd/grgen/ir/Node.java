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
public class Node extends Entity {
  
  /** The type, the node will have after a rule has been applied. */
  private NodeType replaceType;
  
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
    this.replaceType = type;
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
   * Get the type of the node after a rule has finished.
   * @return The post rule type of the node.
   */
  public NodeType getReplaceType() {
		return replaceType;
  }
  
  /**
   * Set the type that will become the new type of the node
   * after a rule has been applied.
   * @param nt The new type of the node.
   */
  public void setReplaceType(NodeType nt) {
		replaceType = nt;
  }
  
  /**
   * Check, if the type of this node changes in a rule.
   * @return true, if the type changes, false, if not.
   */
  public boolean typeChanges() {
  	return !replaceType.isEqual(getNodeType());
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
		/* removed because symmetry is added in grgen.g in rule patternNodeDecl
		 n.homomorphicNodes.add(this);
		*/
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
