/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import java.util.Set;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.EdgeResolver;
import de.unika.ipd.grgen.ast.util.MultChecker;
import de.unika.ipd.grgen.ast.util.OptionalResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.Graph;

/**
 * Node that represents a Connection
 * Children are: Node, Edge, Node
 */
public class ConnectionNode extends BaseNode implements ConnectionCharacter {

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
	private static final Resolver oldNodeResolver =
		new DeclResolver(NodeDeclNode.class);

	private static final Resolver nodeResolver =
		new OptionalResolver(new DeclResolver(NodeDeclNode.class));

	private static final Checker nodeChecker =
		new MultChecker(new Class[] {
			NodeDeclNode.class, NodeTypeChangeNode.class
		});
		

	/**
	 * Construct a new connection node.
	 * A connection node has two node nodes and one edge node
	 * @param loc Location in the source code
	 * @param n1 First node
	 * @param edge Edge that connects n1 with n2
	 * @param n2 Second node.
	 */
	public ConnectionNode(BaseNode n1, BaseNode edge, BaseNode n2) {
		super(edge.getCoords());
		addChild(n1);
		addChild(edge);
		addChild(n2);
		setChildrenNames(childrenNames);
		addResolver(LEFT, nodeResolver);
		addResolver(RIGHT, nodeResolver);
		addResolver(EDGE, new EdgeResolver(getScope(), edge.getCoords()));
	}
	
	/**
	 * Check, if the AST node is correctly built.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(LEFT, NodeCharacter.class)
			&& checkChild(EDGE, EdgeCharacter.class)
			&& checkChild(RIGHT, NodeCharacter.class);
	}
	
	/**
	 * This adds the connection to an IR graph.
	 * This method should only be used by {@link PatternNode#constructIR()}.
	 * @param gr The IR graph.
	 */
	public void addToGraph(Graph gr) {
		// After the AST is checked, this cast must succeed.
		NodeCharacter left, right;
		EdgeCharacter edge;
			
		// Again, after the AST is checked, these casts must succeed.
		left = (NodeCharacter) getChild(LEFT);
		right = (NodeCharacter) getChild(RIGHT);
		edge = (EdgeCharacter) getChild(EDGE);
			
		gr.addConnection(left.getNode(), edge.getEdge(), right.getNode());
	}
	
  /**
   * @see de.unika.ipd.grgen.ast.ConnectionCharacter#addEdges(java.util.Set)
   */
	public void addEdge(Set set) {
		set.add(getChild(EDGE));
	}
  
	public EdgeCharacter getEdge() {
		return (EdgeCharacter) getChild(EDGE);
	}

	public NodeDeclNode getSrc() {
		return (NodeDeclNode) getChild(LEFT);
	}
	
	public NodeDeclNode getTgt() {
		return (NodeDeclNode) getChild(RIGHT);
	}

  /**
   * @see de.unika.ipd.grgen.ast.ConnectionCharacter#addNodes(java.util.Set)
   */
  public void addNodes(Set set) {
		set.add(getChild(LEFT));
		set.add(getChild(RIGHT));
  }

}
