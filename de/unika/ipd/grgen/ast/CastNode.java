/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclTypeChecker;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A cast operator for expressions.
 */
public class CastNode extends OpNode {

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
    super(coords, OperatorSignature.CAST);
    addResolver(TYPE, typeResolver);
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

}
