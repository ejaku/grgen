/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Set;

import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.Rule;

/**
 * AST class representing a redirection statement.
 */
public class RedirectionNode extends BaseNode {

	/** Index of the source node. */
	private static final int FROM = 0;

	/** Index of the edge. */
	private static final int EDGE_TYPE = 1;

	/** Index of the target node. */
	private static final int NODE_TYPE = 2;

	/** Index of the "redirect to" node. */
	private static final int TO = 3;

	static {
		setName(RedirectionNode.class, "redirection");
	}

	/** edge names for the children. */
	private static final String[] childrenNames = {
		"src", "edge", "tgt", "to"
	};
	
	private static final Resolver fromResolver = 
		new DeclResolver(NodeDeclNode.class);
		
	private static final Resolver nodeTypeResolver = 
		new DeclTypeResolver(NodeTypeNode.class);

	private static final Resolver edgeTypeResolver = 
		new DeclTypeResolver(EdgeTypeNode.class);
		
	private static final Resolver toResolver = 
		new DeclResolver(NodeDeclNode.class);
		
	/** 
	 * Denotes, if the edges specified by the "edge type" child are
	 * are incoming or outgoing edges to the node represented by the
	 * "from" child. 
	 */
	private boolean incoming;

  /**
   * Create a new redirection AST node.
   * @param src A declared node in the left side of the rule (The rule must
   * check that).
   * @param edge The edge type to consider.
   * @param tgt A node type.
   * @param to The node to which the edges shall be redirected to (This node
   * must occur on the right side of the rule). 
   */
  public RedirectionNode(BaseNode src, BaseNode edge, BaseNode tgt, 
  	BaseNode to, boolean incoming) {
    super(src.getCoords());
    setChildrenNames(childrenNames);
    
    addChild(src);
		addChild(edge);
		addChild(tgt);
		addChild(to);
		this.incoming = incoming;
		
		addResolver(FROM, fromResolver);
		addResolver(EDGE_TYPE, edgeTypeResolver);
		addResolver(NODE_TYPE, nodeTypeResolver);
		addResolver(TO, toResolver);
  }
  
	/**
	 * Check, if the redirection source node appears in a set.
	 * @param nodes The set to check.
	 * @return true, iff the source node was found in the set.
	 */
  protected boolean checkFrom(Set nodes) {
		return nodes.contains(getChild(FROM));
  }
  
  /**
   * Check, if the redirection target node appears in a set.
   * @param nodes The set to check.
   * @return true, iff the target node was found in the set.
   */
  protected boolean checkTo(Set nodes) {
  	return nodes.contains(getChild(TO));
  }
  
  /**
   * Add this redirection to an IR rule object.
   * @param r The IR rule object.
   */
  protected void addToRule(Rule r) {
  	NodeDeclNode from = (NodeDeclNode) getChild(FROM);
		NodeDeclNode to = (NodeDeclNode) getChild(TO);
  	EdgeTypeNode et = (EdgeTypeNode) getChild(EDGE_TYPE);
		NodeTypeNode nt = (NodeTypeNode) getChild(NODE_TYPE);
  	
  	r.addRedirection(from.getNode(), to.getNode(), 
  		et.getEdgeType(), nt.getNodeType(), incoming);
  }

  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#check()
   */
  protected boolean check() {
  	boolean res = false;
  	
  	res = checkChild(FROM, NodeDeclNode.class)
  		&& checkChild(EDGE_TYPE, EdgeTypeNode.class)
  		&& checkChild(NODE_TYPE, NodeTypeNode.class)
  		&& checkChild(TO, NodeDeclNode.class);
  	
  	return res;
  }

}
