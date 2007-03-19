package de.unika.ipd.grgen.ast;

import java.awt.Color;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.MultChecker;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.EdgeType;

public class ParamDeclNode extends DeclNode {
	static {
		setName(NodeDeclNode.class, "node");
	}
	
	private static final Resolver typeResolver =
		new DeclTypeResolver(new Class[] { NodeTypeNode.class, EdgeTypeNode.class });
	
	private static final Checker typeChecker =
		new MultChecker(new Class[] { NodeTypeNode.class, EdgeTypeNode.class } );
	
	public ParamDeclNode(IdentNode n, BaseNode t) {
		super(n, t);
		addResolver(TYPE, typeResolver);
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeColor()
	 */
	public Color getNodeColor() {
		return isNode() ? Color.GREEN : Color.YELLOW;
	}
	
	public boolean isNode() {
		return getChild(TYPE) instanceof NodeTypeNode;
	}

	public boolean isEdge() {
		return getChild(TYPE) instanceof EdgeTypeNode;
	}

	@Override
	protected boolean check() {
	  	return checkChild(IDENT, IdentNode.class)
	  	  && checkChild(TYPE, typeChecker);
	}

	@Override
	protected IR constructIR() {
		if(isNode()) {
		  	NodeType type = (NodeType) getDeclType().checkIR(NodeType.class);
		  	return new Node(getIdentNode().getIdent(), type,
					getIdentNode().getAttributes());
		} else {
		  	EdgeType type = (EdgeType) getDeclType().checkIR(EdgeType.class);
		  	return new Edge(getIdentNode().getIdent(), type,
					getIdentNode().getAttributes());
		}
	}

}
