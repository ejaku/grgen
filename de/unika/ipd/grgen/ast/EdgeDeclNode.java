/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import java.awt.Color;

public class EdgeDeclNode extends ConstraintDeclNode implements EdgeCharacter {
	
	static {
		setName(EdgeDeclNode.class, "edge declaration");
	}
	
	private static final Resolver typeResolver =
		new DeclTypeResolver(EdgeTypeNode.class);
	
	public EdgeDeclNode(IdentNode n, BaseNode e, BaseNode constraints) {
		super(n, e, constraints);
		setName("edge");
		addResolver(TYPE, typeResolver);
	}
	
	protected boolean check() {
		return super.check()
			&& checkChild(TYPE, EdgeTypeNode.class);
	}
	
	/**
	 * Edges have more info to give
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeInfo()
	 */
	protected String extraNodeInfo() {
		return "";
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	public Color getNodeColor() {
		return Color.YELLOW;
	}
	
	/**
	 * Get the ir object correctly casted.
	 * @return The edge ir object.
	 */
	public Edge getEdge() {
		return (Edge) checkIR(Edge.class);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		// This must be ok after checking all nodes.
		TypeNode tn = (TypeNode) getDeclType();
		EdgeType et = (EdgeType) tn.checkIR(EdgeType.class);
		IdentNode ident = getIdentNode();
		
		Edge edge = new Edge(ident.getIdent(), et, ident.getAttributes());
		edge.setConstraints(getConstraints());
		
		return edge;
	}
	
}
