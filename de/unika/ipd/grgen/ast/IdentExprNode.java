/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.Resolver;

/**
 * An expression consisting of an identifier.
 */
public class IdentExprNode extends DeclExprNode {

	static {
		setName(IdentExprNode.class, "IdentExpr");
	}

	private static final int IDENT = 0;
	
	private static final Resolver identResolver =
		new DeclResolver(DeclNode.class);
		
	private IdentNode ident;

  /**
   * Make a new identifier expression node.
   * @param coords The source code coordinates.
   */
  public IdentExprNode(IdentNode ident) {
    super(ident.getCoords());
    this.ident = ident;
    addChild(ident);
    
  }

	public IdentNode getIdent() {
		return ident; 
	}

  /**
   * @see de.unika.ipd.grgen.ast.DeclExprNode#resolveDecl()
   */
  protected DeclNode resolveDecl() {
  	return (DeclNode) ident.getDecl();
  }

  /**
   * @see de.unika.ipd.grgen.ast.ExprNode#fixupDeclaration(de.unika.ipd.grgen.ast.ScopeOwner)
   */
  protected void fixupDeclaration(ScopeOwner owner) {
  	debug.entering();
  	debug.report(NOTE, "fixup ident " + ident + " with scope owner " + owner);
		debug.report(NOTE, "ident def before: " + ident.getSymDef());
		owner.fixupDefinition(ident);
		debug.report(NOTE, "ident def after: " + ident.getSymDef());
		debug.leaving();
  }

}
