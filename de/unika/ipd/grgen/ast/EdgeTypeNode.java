/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Iterator;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;

public class EdgeTypeNode extends InheritanceTypeNode {

	static {
		setName(EdgeTypeNode.class, "edge type");
	}

	private static final int EXTENDS = 0;
	private static final int BODY = 1;

	private static final String[] childrenNames = {
		"extends", "body"
	};

	private static final Checker extendsChecker =
	  new CollectChecker(new SimpleChecker(EdgeTypeNode.class));
	  
	private static final Checker bodyChecker =
		new CollectChecker(new SimpleChecker(MemberDeclNode.class));

	private static final Resolver extendsResolver =
		new CollectResolver(new DeclTypeResolver(EdgeTypeNode.class));

	private static final Resolver bodyResolver =
		new CollectResolver(new DeclTypeResolver(MemberDeclNode.class));


	/**
	 * Make a new edge type node.
	 * @param ext The collect node with all edge classes that this one extends.
	 * @param body The body of the type declaration. It consists of basic
	 * declarations.
	 * @param modifiers The modifiers for this type.
	 */
  public EdgeTypeNode(BaseNode ext, BaseNode body, int modifiers) {
    super(BODY, bodyChecker, bodyResolver,
      EXTENDS, extendsChecker, extendsResolver);
    addChild(ext);
    addChild(body);
    setChildrenNames(childrenNames);
    setModifiers(modifiers);
  }

  /**
   * Get the edge type ir object.
   * @return The edge type ir object for this ast node.
   */
  public EdgeType getEdgeType() {
  	return (EdgeType) checkIR(EdgeType.class);
  }
  
  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
   */
  protected IR constructIR() {
		EdgeType et = new EdgeType(getDecl().getIdentNode().getIdent(), getIRModifiers());
		Iterator ents = getChild(BODY).getChildren();
		while(ents.hasNext()) {
			DeclNode decl = (DeclNode) ents.next();
			et.addMember(decl.getEntity());
		}
		Iterator ext = getChild(EXTENDS).getChildren();
		while(ext.hasNext()) {
			EdgeTypeNode x = (EdgeTypeNode) ext.next();
			et.addInherits(x.getEdgeType());
		}
		return et;
	}
}
