/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.parser.Coords;

/**
 *  
 */
public class NodeTypeChangeNode extends BaseNode implements NodeCharacter {

	static {
		setName(NodeTypeChangeNode.class, "node type change");
	}

	private static final int NODE = 0;
	
	private static final int TYPE = 1;
	
	private static final String[] childrenNames = {
		"node", "replace type"
	};

	private static final Resolver nodeResolver =
		new DeclResolver(NodeDeclNode.class);
		
	private static final Resolver typeResolver = 
		new DeclTypeResolver(NodeTypeNode.class);

  public NodeTypeChangeNode(Coords coords, BaseNode nodeDecl, 
  	BaseNode newType) {
  		
  	super(coords);
  	setChildrenNames(childrenNames);
  	addChild(nodeDecl);
  	addChild(newType);
  	addResolver(NODE, nodeResolver);
  	addResolver(TYPE, typeResolver);
  }

  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#check()
   */
  protected boolean check() {
    return checkChild(NODE, NodeDeclNode.class)
    	&& checkChild(TYPE, NodeTypeNode.class);
  }
  
  public Node getNode() {
  	return (Node) checkIR(Node.class);
  }

  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
   */
  protected IR constructIR() {
		// This cast must be ok after checking.
  	NodeTypeNode newType = (NodeTypeNode) getChild(TYPE);
  	NodeDeclNode nodeDecl = (NodeDeclNode) getChild(NODE);
  	
  	Node node = nodeDecl.getNode();
  	node.setReplaceType(newType.getNodeType());
  	
		return node;
  }

}
