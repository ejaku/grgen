
package de.unika.ipd.grgen.ast;

import java.awt.Color;

import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;

public class NodeDeclNode extends DeclNode {

	static {
		setName(NodeDeclNode.class, "node");
	}
	
	private static final Resolver typeResolver = 
		new DeclTypeResolver(NodeTypeNode.class);

  public NodeDeclNode(IdentNode i, BaseNode t) {
  	super(i, t);
  	addResolver(TYPE, typeResolver);
  }
  
  /**
   * The node node is ok if the decl check succeeds and
   * the second child is a node type node.
   * @see de.unika.ipd.grgen.ast.BaseNode#check()
   */
  protected boolean check() {
  	return super.check() 
  		&& checkChild(1, NodeTypeNode.class);
  }
  
  
  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
   */
  public Color getNodeColor() {
    return Color.GREEN;
  }

	public Node getNode() {
		return (Node) checkIR(Node.class);
	}
	
	protected IR constructIR() {
		// This cast must be ok after checking.
		NodeTypeNode tn = (NodeTypeNode) getDeclType();
		NodeType nt = tn.getNodeType();
		
		Node n = new Node(getIdentNode().getIdent(), nt);
		
		return n;
	}
	 

}
