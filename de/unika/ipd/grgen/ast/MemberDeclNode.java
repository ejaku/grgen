/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
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

  /**
   * @param n Identifier which declared the member.
   * @param t Type with which the member was declared. 
   */
  public MemberDeclNode(IdentNode n, BaseNode t) {
    super(n, t);
		addResolver(TYPE, typeResolver);
  }

}
