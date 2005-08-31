/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.*;

import de.unika.ipd.grgen.ir.ConnAssert;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import java.util.Iterator;

public class EdgeTypeNode extends InheritanceTypeNode {
	
	static {
		setName(EdgeTypeNode.class, "edge type");
	}
	
	private static final int EXTENDS = 0;
	private static final int CAS = 1;
	private static final int BODY = 2;
	
	private static final String[] childrenNames = {
		"extends", "cas", "body"
	};
	
	private static final Checker extendsChecker =
		new CollectChecker(new SimpleChecker(EdgeTypeNode.class));
	
	private static final Checker casChecker = // TODO use this
		new CollectChecker(new SimpleChecker(ConnAssertNode.class));
	
	private static final Checker bodyChecker =
		new CollectChecker(new SimpleChecker(MemberDeclNode.class));
	
	private static final Resolver extendsResolver =
		new CollectResolver(new DeclTypeResolver(EdgeTypeNode.class));
	
	private static final Resolver casResolver =
		new CollectResolver(new DeclTypeResolver(ConnAssertNode.class));
	
	private static final Resolver bodyResolver =
		new CollectResolver(new DeclTypeResolver(MemberDeclNode.class));
	
	
	/**
	 * Make a new edge type node.
	 * @param ext The collect node with all edge classes that this one extends.
	 * @param body The body of the type declaration. It consists of basic
	 * declarations.
	 * @param modifiers The modifiers for this type.
	 */
	public EdgeTypeNode(BaseNode ext, BaseNode cas,  BaseNode body, int modifiers) {
		super(BODY, bodyChecker, bodyResolver,
			  EXTENDS, extendsChecker, extendsResolver);
		addChild(ext);
		addChild(cas);
		addChild(body);
		setChildrenNames(childrenNames);
		addResolver(CAS, casResolver);
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
		Iterator<BaseNode> ents = getChild(BODY).getChildren();
		while(ents.hasNext()) {
			DeclNode decl = (DeclNode) ents.next();
			et.addMember(decl.getEntity());
		}
		Iterator<BaseNode> ext = getChild(EXTENDS).getChildren();
		while(ext.hasNext()) {
			EdgeTypeNode etn = (EdgeTypeNode) ext.next();
			et.addSuperType(etn.getEdgeType());
		}
		for(Iterator<BaseNode> it = getChild(CAS).getChildren(); it.hasNext();) {
			ConnAssertNode can = (ConnAssertNode)it.next();
			et.addConnAssert((ConnAssert)can.checkIR(ConnAssert.class));
		}
		return et;
	}
}
