/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * A base class for all expression nodes, that result in a declaration.
 */
public abstract class DeclExprNode extends ExprNode {

	private DeclNode decl = null;

  protected DeclExprNode(Coords coords) {
    super(coords);
  }

  /**
   * @see de.unika.ipd.grgen.ast.ExprNode#getType()
   */
  public TypeNode getType() {
  	BaseNode d = getDecl().getDeclType(); 
    return d instanceof TypeNode ? (TypeNode) d : BasicTypeNode.errorType;
  }
  
  protected abstract DeclNode resolveDecl();

	public DeclNode getDecl() {
		
		debug.entering();
		
		if(decl == null)
			decl = resolveDecl();
		
		debug.report(NOTE, "decl: " + decl);
		debug.leaving();
			
		return decl;
	}

}
