/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.EdgeDeclNode;
import de.unika.ipd.grgen.ast.EdgeTypeNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.TypeDeclNode;
import de.unika.ipd.grgen.ast.TypeNode;

import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.Symbol;
import de.unika.ipd.grgen.parser.Scope;

/**
 * A resolver, that resolves the identifier in an edge node.
 */
public class EdgeResolver extends IdentResolver {

  private static final Class[] edgeClass = {
		EdgeDeclNode.class
	};
	
	private Scope scope;

	private Coords coords;
	
	private boolean negated;


	public EdgeResolver(Scope scope, Coords coords, boolean negated) {
		super(edgeClass);
		this.scope = scope;
		this.coords = coords;
		this.negated = negated;
	}

  /**
   * @see de.unika.ipd.grgen.ast.util.IdentResolver#resolveIdent(de.unika.ipd.grgen.ast.IdentNode)
   */
  protected BaseNode resolveIdent(IdentNode n) {
  	BaseNode d = n.getDecl();
  	BaseNode res = BaseNode.getErrorNode();
  	
  	if(d instanceof TypeDeclNode) {
  		TypeNode ty = (TypeNode) ((TypeDeclNode) d).getDeclType();
  		if(ty instanceof EdgeTypeNode) {
  			Symbol.Definition def = scope.defineAnonymous("edge", coords);
  			res = new EdgeDeclNode(new IdentNode(def), ty, negated);
  		} else
  			reportError(n, "identifier \"" + n + "\" is expected to declare "
  			  + "an edge type not a \"" + ty.getName() + "\"");
  	} else if(d instanceof EdgeDeclNode) {
			res = d;
  	}
  		
  	else
  		reportError(n, "edge or edge type expected");
  	
    return res;
  }


}
