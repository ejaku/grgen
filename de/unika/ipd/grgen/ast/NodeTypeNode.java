/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.*;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NodeType;
import java.util.Iterator;

/**
 * A class representing a node type
 */
public class NodeTypeNode extends InheritanceTypeNode {
	
	static {
		setName(NodeTypeNode.class, "node type");
	}
	
	private static final int EXTENDS = 0;
	private static final int BODY = 1;
	
	private static final String[] childrenNames = {
		"extends", "body"
	};
	
	private static final Checker extendsChecker =
		new CollectChecker(new SimpleChecker(NodeTypeNode.class));
	
	private static final Checker bodyChecker =
		new CollectChecker(new SimpleChecker(MemberDeclNode.class));
	
	private static final Resolver extendsResolver =
		new CollectResolver(new DeclTypeResolver(NodeTypeNode.class));
	
	private static final Resolver bodyResolver =
		new CollectResolver(new DeclResolver(MemberDeclNode.class));
	
	/**
	 * Create a new node type
	 * @param ext The collect node containing the node types which are extended
	 * by this type.
	 * @param body the collect node with body declarations
	 * @param modifiers Type modifiers for this type.
	 */
	public NodeTypeNode(BaseNode ext, BaseNode body, int modifiers) {
		super(BODY, bodyChecker, bodyResolver,
			  EXTENDS, extendsChecker, extendsResolver);
		
		addChild(ext);
		addChild(body);
		setChildrenNames(childrenNames);
		setModifiers(modifiers);
	}
	
	/**
	 * Get the IR node type for this AST node.
	 * @return The correctly casted IR node type.
	 */
	public NodeType getNodeType() {
		return (NodeType) checkIR(NodeType.class);
	}
	
	/**
	 * Construct IR object for this AST node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		NodeType nt = new NodeType(getDecl().getIdentNode().getIdent(), getIRModifiers());
		Iterator ents = getChild(BODY).getChildren();
		while(ents.hasNext()) {
			DeclNode decl = (DeclNode) ents.next();
			nt.addMember(decl.getEntity());
		}
		Iterator ext = getChild(EXTENDS).getChildren();
		while(ext.hasNext()) {
			NodeTypeNode x = (NodeTypeNode) ext.next();
			nt.addInherits(x.getNodeType());
		}
		return nt;
	}
}
