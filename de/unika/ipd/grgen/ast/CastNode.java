/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashSet;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclTypeChecker;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A cast operator for expressions.
 */
public class CastNode extends ExprNode {

	/** The type child index. */
	private final static int TYPE = 0;

	/** The expression child index. */	
	private final static int EXPR = 1;

	/** The checker for the type operand. */
	static private final Checker typeChecker = 
		new DeclTypeChecker(BasicTypeNode.class);

	/** The resolver for the type */
	static private final Resolver typeResolver =
		new DeclTypeResolver(BasicTypeNode.class);

  /**
   * Make a new cast node.
   * @param coords The source code coordiantes.
   */
  public CastNode(Coords coords) {
    super(coords);
    addResolver(TYPE, typeResolver);
  }
  
  /**
   * Make a new cast not with a target type and an expression
   * @param coords The source code coordinates.
   * @param targetType The target type.
   * @param expr The expression to be casted.
   */
  public CastNode(Coords coords, TypeNode targetType, BaseNode expr) {
  	this(coords);
  	addChild(targetType);
  	addChild(expr);
  }
  
  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#check()
   * A cast node is valid, if the second child is an expression node 
   * and the first node is a type node identifier.
   */
  protected boolean check() {
		return checkChild(TYPE, typeChecker)
			&& checkChild(EXPR, ExprNode.class);
  }

	/**
	 * Check the types of this cast.
	 * Check if the expression can be casted to the given type.
	 * @see de.unika.ipd.grgen.ast.BaseNode#typeCheck()
	 */
	protected boolean typeCheck() {
		Collection castableToTypes = new HashSet();
		ExprNode exp = (ExprNode) getChild(EXPR);
		BasicTypeNode bt = (BasicTypeNode) getChild(TYPE);
		exp.getType().getCastableToTypes(castableToTypes);
		
		boolean result = castableToTypes.contains(bt);
		if(!result)
			reportError("Illegal cast from " + exp.getType() + " to " + bt);

		return result;
	}

  /**
   * @see de.unika.ipd.grgen.ast.ExprNode#eval()
   */
  protected ConstNode eval() {
  	ExprNode expr = (ExprNode) getChild(EXPR);
  	TypeNode type = (TypeNode) getChild(TYPE);
  	TypeNode argType = expr.getType();
		ConstNode arg = expr.evaluate();

		return arg.castTo(type);		
  }

  /**
   * @see de.unika.ipd.grgen.ast.ExprNode#getType()
   */
  public TypeNode getType() {
  	TypeNode type = (TypeNode) getChild(TYPE);
  	return type;
  }

  /**
   * @see de.unika.ipd.grgen.ast.ExprNode#isConstant()
   */
  public boolean isConstant() {
    ExprNode expr = (ExprNode) getChild(EXPR);
    return expr.isConstant();
  }

}
