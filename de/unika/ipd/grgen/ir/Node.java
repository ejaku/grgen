/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A node in a graph 
 */
public class Node extends Entity {
  
  private NodeType replaceType;
  
  /**
   * Make a new node.
   * @param ident The identifier that declared the node.
   * @param type The node type of the node.
   */
  public Node(Ident ident, NodeType type) {
    super("node", ident, type);
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
}
