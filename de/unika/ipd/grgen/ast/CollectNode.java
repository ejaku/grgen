/**
 * @file CollectNode.java
 * @author shack
 * @date Jul 21, 2003
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;

/**
 * A node that represents a collection of other nodes
 */
public class CollectNode extends BaseNode {

	static {
		setName(CollectNode.class, "collect");
	}

  public CollectNode() {
    super();
  }

	/**
	 * The collect node is always in a correct state.
	 * Use #checkAllChildren(Class) to check for the state
	 * of the children
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkAllChildren(Class)
	 */  
  protected boolean check() {
  	return true;
  }
  
  public Color getNodeColor() {
  	return Color.GRAY;
  }
}
