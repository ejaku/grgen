/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.OptionalResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.Graph;

/**
 * A single node occurrence in the graph.
 * This AST node is used only for nodes that occur without an edge connection
 * to the rest of the graph.
 */
public class SingleNodeConnNode extends BaseNode {

	/** Index of the node in the children array. */
	private static final int NODE = 0;

	private static final String[] childrenNames = {
		"node"
	};
	
	private static final Resolver nodeResolver = 
		new OptionalResolver(new DeclResolver(NodeDeclNode.class));

	static {
		setName(SingleNodeConnNode.class, "single node");
	}

  /**
   * @param n The node 
   */
  public SingleNodeConnNode(BaseNode n) {
  	super(n.getCoords());
  	addChild(n);
  	setChildrenNames(childrenNames);
  	addResolver(NODE, nodeResolver);
  }

	/**
	 * Get the node child of this node.
	 * @return The node child.
	 */
	public BaseNode getNode() {
		return getChild(NODE);
	}

  /**
   * @see de.unika.ipd.grgen.ast.GraphObjectNode#addToGraph(de.unika.ipd.grgen.ir.Graph)
   */
  protected void addToGraph(Graph gr) {
  	NodeProducer n = (NodeProducer) getChild(NODE);
  	gr.addSingleNode(n.getNode());
  }

  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#check()
   */
  protected boolean check() {
  	return checkChild(NODE, NodeDeclNode.class);
  }

}
