/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.AnonymousEdge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;

/**
 * An anonymous edge decl node.
 */
public class AnonymousEdgeDeclNode extends EdgeDeclNode {

	static {
		setName(AnonymousEdgeDeclNode.class, "anonymous edge");
	}

  /**
   * @param n The identifier of the anonymous edge.
   * @param e The type of the edge.
   * @param negated Shall the edge be negated.
   */
  public AnonymousEdgeDeclNode(IdentNode id, BaseNode type, boolean negated) {
    super(id, type, negated);
  }

	public AnonymousEdgeDeclNode(IdentNode id, BaseNode type) {
		this(id, type, false);
	}

  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
   */
  protected IR constructIR() {
		TypeNode tn = (TypeNode) getDeclType();
		EdgeType et = (EdgeType) tn.checkIR(EdgeType.class); 
		
		return new AnonymousEdge(getIdentNode().getIdent(), et, isNegated());
 }

}
