/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A node in a graph 
 */
public class Node extends Entity {
  
  /**
   * Make a new node.
   * @param ident The identifier that declared the node.
   * @param type The node type of the node.
   */
  public Node(Ident ident, NodeType type) {
    super("node", ident, type);
  }
  
  public NodeType getNodeType() {
  	assert getType() instanceof NodeType : "type of node must be NodeType";
  	return (NodeType) getType();
  }
}
