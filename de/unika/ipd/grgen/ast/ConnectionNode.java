/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.EdgeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.Graph;

/**
 * Node that represents a Connection
 * Children are: Node, Edge, Node
 */
public class ConnectionNode extends BaseNode {

	static {
		setName(ConnectionNode.class, "connection");
	}

  /** edge names for the children. */
	private static final String[] childrenNames = {
		"src", "edge", "tgt"
	};
	
	/** Index of the source node. */
	private static final int LEFT = 0;
	
	/** Index of the edge node. */
	private static final int EDGE = 1;
	
	/** Index of the target node. */	
	private static final int RIGHT= 2;
	
	/** Resolver for the nodes. */
	private static final Resolver nodeResolver = 
		new DeclResolver(NodeDeclNode.class);

	/**
	 * Construct a new connection node. 
	 * A connection node has two node nodes and one edge node
	 * @param loc Location in the source code
	 * @param n1 First node
	 * @param edge Edge that connects n1 with n2
	 * @param n2 Second node.
	 * @param negated true, if the edge was negated, false if not.
	 */
	public ConnectionNode(BaseNode n1, BaseNode edge, BaseNode n2, boolean negated) {
		super(edge.getCoords());
		addChild(n1);
		addChild(edge);
		addChild(n2);
		setChildrenNames(childrenNames);
		addResolver(LEFT, nodeResolver);
		addResolver(RIGHT, nodeResolver);		
		addResolver(EDGE, new EdgeResolver(getScope(), edge.getCoords(), negated));
	}

	/**
	 * Check, if the AST node is correctly built.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(LEFT, NodeDeclNode.class)
			&& checkChild(EDGE, EdgeDeclNode.class)
			&& checkChild(RIGHT, NodeDeclNode.class);
	}
	
	/**
	 * Get the left (source) node of this connection. 
	 * @return The source node of the connection.
	 */
	public BaseNode getLeft() {
		return getChild(0);
	}
	
	/**
	 * Get the edge AST node representing this connection.
	 * @return The edge AST node.
	 */
	public BaseNode getEdge() {
		return getChild(1);
	}
	
	/**
	 * Get the right (target) node of this connection. 
	 * @return The target node of the connection.
	 */
	public BaseNode getRight() {
		return getChild(2);
	}

	/**
	 * This adds the connection to an IR graph.
	 * This method should only be used by {@link PatternNode#constructIR()}. 
	 * @param gr The IR graph.
	 */	
	protected void addToGraph(Graph gr) {
		// After the AST is checked, this cast must succeed.
		NodeDeclNode left, right;
		EdgeDeclNode edge;
			
		// Again, after the AST is checked, these casts must succeed.
		left = (NodeDeclNode) getLeft();
		right = (NodeDeclNode) getRight();
		edge = (EdgeDeclNode) getEdge();
			
		gr.addConnection(left.getNode(), edge.getEdge(), right.getNode());
	}
	
}
