/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;
import java.util.Iterator;

import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.IdentExprResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Base class for all expression nodes.
 */
public abstract class ExprNode extends BaseNode {

	static {
		setName(ExprNode.class, "expression");
	}
	
	protected static final Resolver identExprResolver = 
		new IdentExprResolver(new DeclResolver(DeclNode.class));

  /**
   * Make a new expression
   */
  public ExprNode(Coords coords) {
  	super(coords);
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
   */
  public Color getNodeColor() {
		return Color.PINK;
  }
  
  /**
   * Get the type of the expression.
   * @return The type of this expression node.
   */
  public abstract TypeNode getType();

	protected void fixupDeclaration(ScopeOwner owner) {
		int i = 0;
		
		for(Iterator it = getChildren(); it.hasNext(); i++) {
			Object obj = it.next();
		
			if(obj instanceof ExprNode) {
				ExprNode node = (ExprNode) obj;
				node.fixupDeclaration(owner);
			} else
				reportError("Child is " + i + " is not an expression");
		}
	}
	
	public DeclNode getDecl() {
		return DeclNode.getInvalid();
	}

}
