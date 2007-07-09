/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.*;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NodeType;
import java.util.Iterator;
import java.util.Collection;
import java.util.HashSet;

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
	
	public Collection<BaseNode> getDirectSuperTypes()
	{
		return getChild(EXTENDS).getChildren();
	}
	
	/**
	 * Construct IR object for this AST node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		NodeType nt = new NodeType(getDecl().getIdentNode().getIdent(), getIRModifiers());
		for(BaseNode n : getChild(BODY).getChildren()) {
			DeclNode decl = (DeclNode)n;
			nt.addMember(decl.getEntity());
		}
		for(BaseNode n : getChild(EXTENDS).getChildren()) {
			NodeTypeNode x = (NodeTypeNode)n;
			nt.addDirectSuperType(x.getNodeType());
		}
		
		// to check overwriting of attributes
		nt.getAllMembers();
		
		return nt;
	}

	public static String getKindStr() {
		return "node type";
	}

	public static String getUseStr() {
		return "node";
	}
}


