
/**
 * DeclNode.java
 *
 *
 * Created: Sat Jul  5 17:52:43 2003
 *
 * @author Sebastian Hack
 * @version 1.0
 */

package de.unika.ipd.grgen.ast;

import java.awt.Color;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;

public abstract class DeclNode extends BaseNode {

	static {
		setName(DeclNode.class, "declaration");
	}

	/** Index of the identifier in the children array */
	protected static final int IDENT = 0;

	/** Index of the type in the children array */	
	protected static final int TYPE = 1;
	
	protected static final int LAST = TYPE;
	
	protected static final String[] declChildrenNames = {
		"name", "type"
	};
	
	/** An invalid declaration. */
	private static final DeclNode invalidDecl = 
		new DeclNode(IdentNode.getInvalid(), BasicTypeNode.errorType) { };

	/** Get an invalid declaration. */
	protected static final DeclNode getInvalid() {
		return invalidDecl;
	}

  /**
   * Create a new decl node 
   * @param n The identifier that declares
   * @param t The type with which is declared
   */
  protected DeclNode(IdentNode n, BaseNode t) {
    super(n.getCoords());
		n.setDecl(this);
    addChild(n);
    addChild(t);
    setChildrenNames(declChildrenNames);
  }

	/**
	 * Get the ident node for this declaration. The ident node represents
	 * the declared identifier.
   * @return An ident node
   */
  public IdentNode getIdentNode() {
		return (IdentNode) getChild(IDENT);
	}
  
  /**
   * Get the type of the declaration
   * @return The type node for the declaration
   */
  public BaseNode getDeclType() {
    return getChild(TYPE);
  }

	/**
   * @see de.unika.ipd.grgen.ast.BaseNode#verify()
   */
  protected boolean check() {
  	return checkChild(IDENT, IdentNode.class)
  	  && checkChild(TYPE, TypeNode.class);	
  }
  
  /**
   * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeColor()
   */
  public Color getNodeColor() {
    return Color.BLUE;
  }
  
  public Entity getEntity() {
  	return (Entity) checkIR(Entity.class);
  }
  
  protected IR constructIR() {
  	Type type = (Type) getDeclType().checkIR(Type.class);
  	return new Entity(getIdentNode().getIdent(), type);
  }

}
