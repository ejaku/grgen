
package de.unika.ipd.grgen.ast;

import java.awt.Color;

import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;

public class EdgeDeclNode extends DeclNode implements EdgeCharacter {

	static {
		setName(EdgeDeclNode.class, "edge declaration");
	}
	
	private static final Resolver typeResolver = 
		new DeclTypeResolver(EdgeTypeNode.class); 

  protected boolean negated;

  public EdgeDeclNode(IdentNode n, BaseNode e, boolean negated) {
    super(n, e);
    setName("edge");
		this.negated = negated;
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
    return "negated: " + negated;
  }

  /**
   * Check, if the edge is negated.
   * @return true, if the edge is negated, false, if not.
   */
  public boolean isNegated() {
    return negated;
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
   */
  public Color getNodeColor() {
    return negated ? Color.CYAN : Color.YELLOW;
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
		
		Edge edge = new Edge(getIdentNode().getIdent(), et, isNegated());
		return edge;
	}

}
