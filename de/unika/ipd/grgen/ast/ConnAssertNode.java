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
		"src", "src range", "tgt", "tgt range"
	};
	
	/** Index of the source node. */
	private static final int SRC = 0;
	
	/** Index of the source node range. */
	private static final int SRCRANGE = 1;
	
	/** Index of the target node. */
	private static final int TGT = 2;
	
	/** Index of the target node range. */
	private static final int TGTRANGE = 3;
	
	
	/** Resolver for the nodes. */
	private static final Resolver nodeResolver =
		new DeclTypeResolver(NodeTypeNode.class);
	
	/**
	 * Construct a new connection assertion node.
	 * Graphically: hostnode -> edge [range] -> node
	 * @param src The node the host node is connected to, via the edge.
	 * @param srcRange ?
	 * @param tgt
	 * @param tgtRange The allowed number of edges of one type connected to the host node.
	 */
	public ConnAssertNode(BaseNode src, BaseNode srcRange,
						  BaseNode tgt, BaseNode tgtRange) {
		super(src.getCoords());
		addChild(src);
		addChild(srcRange);
		addChild(tgt);
		addChild(tgtRange);
		setChildrenNames(childrenNames);
		addResolver(SRC, 		nodeResolver);
		//addResolver(SRCRANGE, 	null);
		addResolver(TGT, 		nodeResolver);
		//addResolver(TGTRANGE, 	null);
	}
	
	/**
	 * Check, if the AST node is correctly built.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(SRC, 		NodeTypeNode.class)
			&& checkChild(SRCRANGE,	RangeSpecNode.class)
			&& checkChild(TGT,		NodeTypeNode.class)
			&& checkChild(TGTRANGE,	RangeSpecNode.class);
	}
}
