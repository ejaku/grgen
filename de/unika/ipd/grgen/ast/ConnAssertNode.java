/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;



import de.unika.ipd.grgen.ast.util.*;

import de.unika.ipd.grgen.ir.Graph;
import java.util.Set;

/**
 * Node that represents a Connection
 * Children are: Node, Edge, Node
 */
public class ConnAssertNode extends BaseNode {
	
	static {
		setName(ConnAssertNode.class, "conn assert");
	}
	
	/** edge names for the children. */
	private static final String[] childrenNames = {
		"edge", "range", "node",
	};
	
	/** Index of the source node. */
	private static final int EDGE = 0;
	
	/** Index of the edge node. */
	private static final int RANGE = 1;
	
	/** Index of the target node. */
	private static final int NODE= 2;
	
	/** Resolver for the nodes. */
	
//	private static final Checker nodeChecker =
//		new MultChecker(new Class[] {
//				NodeDeclNode.class, NodeTypeChangeNode.class
//			});
	
	private static final Resolver edgeResolver =
		new DeclTypeResolver(EdgeTypeNode.class);
	
	private static final Resolver nodeResolver =
		new DeclTypeResolver(NodeTypeNode.class);
	
	private boolean outgoing;
	
	/**
	 * Construct a new connection assertion node.
	 * Graphically: hostnode -> edge [range] -> node
	 * @param edge The edge type connected to the host node.
	 * @param range The allowed number of edges of one type connected to the host node.
	 * @param node The node the host node is connected to, via the edge.
	 * @param outgoing true, if the edge is a out going one.
	 */
	public ConnAssertNode(BaseNode edge, BaseNode range,
						  BaseNode node, boolean outgoing) {
		super(edge.getCoords());
		addChild(edge);
		addChild(range);
		addChild(node);
		this.outgoing = outgoing;
		setChildrenNames(childrenNames);
		addResolver(EDGE, edgeResolver);
		addResolver(NODE, nodeResolver);
	}
	
	/**
	 * Check, if the AST node is correctly built.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(EDGE, EdgeTypeNode.class)
			&& checkChild(RANGE, RangeSpecNode.class)
			&& checkChild(NODE, NodeTypeNode.class);
	}
	
	public boolean isOutgoing() {
		return outgoing;
	}
}
