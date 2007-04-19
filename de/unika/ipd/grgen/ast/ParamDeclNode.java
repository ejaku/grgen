package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.*;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.MultChecker;
import de.unika.ipd.grgen.ast.util.Resolver;
import java.awt.Color;

public class ParamDeclNode extends DeclNode implements NodeCharacter, EdgeCharacter {
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
		} else if(isEdge()) {
			EdgeType type = (EdgeType) getDeclType().checkIR(EdgeType.class);
			return new Edge(getIdentNode().getIdent(), type,
							getIdentNode().getAttributes());
		} else {
			return null;
		}
	}
	
	public Node getNode() {
		return (Node)checkIR(Node.class);
	}
	
	public Edge getEdge() {
		return (Edge)checkIR(Edge.class);
	}
}
