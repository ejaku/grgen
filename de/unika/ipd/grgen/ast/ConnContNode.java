/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

/**
 * AST node for ConnCont rule
 * These nodes should never appear in the final ast.
 */
public class ConnContNode extends BaseNode {

	/**
	 * New conn cont node
	 * @param edge The edge
	 * @param node The node
	 */
	public ConnContNode(BaseNode edge, BaseNode node) {
		super();
		addChild(edge);
		addChild(node);
	}
	
	protected boolean check() {
		reportError("should never appear in the ast");
		checkChild(0, EdgeDeclNode.class);
		checkChild(1, NodeDeclNode.class);
		return false;
	}
	
	public BaseNode getNode() {
		return getChild(1);
	}
	
	public BaseNode getEdge() {
		return getChild(0);
	}

}
