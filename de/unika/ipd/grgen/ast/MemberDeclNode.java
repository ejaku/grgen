/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.MultChecker;
import de.unika.ipd.grgen.ast.util.Resolver;

/**
 * A compound type member declaration. 
 */
public class MemberDeclNode extends DeclNode {

	static {
		setName(MemberDeclNode.class, "member declaration");
	}

	private static final Resolver typeResolver =
		new DeclTypeResolver(TypeNode.class); 

	private static final Checker typeChecker = 
		new MultChecker(new Class[] { BasicTypeNode.class, EnumTypeNode.class });

  /**
   * @param n Identifier which declared the member.
   * @param t Type with which the member was declared. 
   */
  public MemberDeclNode(IdentNode n, BaseNode t) {
    super(n, t);
		addResolver(TYPE, typeResolver);
  }
  
  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#check()
   */
  protected boolean check() {
		return checkChild(TYPE, typeChecker);
  }

}
