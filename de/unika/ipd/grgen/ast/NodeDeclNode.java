
package de.unika.ipd.grgen.ast;

import java.awt.Color;
import java.util.Iterator;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.OptionalResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;

/**
 * Declaration of a node.
 */
public class NodeDeclNode extends DeclNode implements NodeCharacter {

	static {
		setName(NodeDeclNode.class, "node");
	}
	
	private static final Resolver typeResolver = 
		new DeclTypeResolver(NodeTypeNode.class);
		
	/** Index of the collect node containing probable homomorphic nodes. */
	private static final int HOMOMORPHIC = 2;

	/** 
	 * Resolve probable identifiers in the hom collect node to 
	 * node declarations.
	 */	
	private static final Resolver homResolver = 
	  new CollectResolver(new OptionalResolver(new DeclResolver(NodeDeclNode.class)));
	  	
	/**
	 * Check the homomorphic nodes child. It must be a collect node
	 * with node declarations. 
	 */
	private static final Checker homChecker =
		new CollectChecker(new SimpleChecker(NodeDeclNode.class)); 	

	/**
	 * Make a new node declaration with no homomorphic nodes.
	 * @param id The identifier of the node.
	 * @param type The type of the node.
	 */
  public NodeDeclNode(IdentNode id, BaseNode type) {
		this(id, type, new CollectNode());
  }

	/**
	 * Make a new node declaration with other homomorphic nodes.
	 * @param id The identifier of the node.
	 * @param type The type of the node.
	 * @param homomorphic A collect node with homomorphic nodes.
	 */
	public NodeDeclNode(IdentNode id, BaseNode type, BaseNode homomorphic) {
		super(id, type);
		addChild(homomorphic);
		addResolver(TYPE, typeResolver);
		addResolver(HOMOMORPHIC, homResolver);
	}
  
  /**
   * The node node is ok if the decl check succeeds and
   * the second child is a node type node.
   * @see de.unika.ipd.grgen.ast.BaseNode#check()
   */
  protected boolean check() {
  	return super.check() 
  		&& checkChild(TYPE, NodeTypeNode.class)
  		&& checkChild(HOMOMORPHIC, homChecker);
  }
  
  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
   */
  public Color getNodeColor() {
    return Color.GREEN;
  }

	/**
	 * @see de.unika.ipd.grgen.ast.NodeCharacter#getNode()
	 */
	public Node getNode() {
		return (Node) checkIR(Node.class);
		
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */	
	protected IR constructIR() {
		// This cast must be ok after checking.
		NodeTypeNode tn = (NodeTypeNode) getDeclType();
		NodeType nt = tn.getNodeType();
		
		Node res = new Node(getIdentNode().getIdent(), nt);
		
		// Add all homomorphic nodes in the collect node to 
		// the constructed IR node of this node. 
		for(Iterator it = getChild(HOMOMORPHIC).getChildren(); it.hasNext();) {
			NodeCharacter nc = (NodeCharacter) it.next();
			res.addHomomorphic(nc.getNode());
		}
		
		return res; 
	}
	 

}
