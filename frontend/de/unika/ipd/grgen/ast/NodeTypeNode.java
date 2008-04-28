/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.CollectPairResolver;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.MemberInit;
import de.unika.ipd.grgen.ir.NodeType;

/**
 * A class representing a node type
 */
public class NodeTypeNode extends InheritanceTypeNode {
	static {
		setName(NodeTypeNode.class, "node type");
	}

	CollectNode<BaseNode> body;
	CollectNode<NodeTypeNode> extend;

	/**
	 * Create a new node type
	 * @param ext The collect node containing the node types which are extended by this type.
	 * @param body the collect node with body declarations
	 * @param modifiers Type modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public NodeTypeNode(CollectNode<IdentNode> ext, CollectNode<BaseNode> body,
						int modifiers, String externalName) {
		this.extendUnresolved = ext;
		becomeParent(this.extendUnresolved);
		this.bodyUnresolved = body;
		becomeParent(this.bodyUnresolved);
		setModifiers(modifiers);
		setExternalName(externalName);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(extendUnresolved, extend));
		children.add(getValidVersion(bodyUnresolved, body));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("extends");
		childrenNames.add("body");
		return childrenNames;
	}

	private static final CollectResolver<NodeTypeNode> extendResolver =	new CollectResolver<NodeTypeNode>(
			new DeclarationTypeResolver<NodeTypeNode>(NodeTypeNode.class));

	private static final CollectPairResolver<BaseNode> bodyResolver = new CollectPairResolver<BaseNode>(
			new DeclarationPairResolver<MemberDeclNode, MemberInitNode>(MemberDeclNode.class, MemberInitNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		body = bodyResolver.resolve(bodyUnresolved, this);
		extend = extendResolver.resolve(extendUnresolved, this);

		return body != null && extend != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal()  {
		return super.checkLocal();
	}

	/**
	 * Get the IR node type for this AST node.
	 * @return The correctly casted IR node type.
	 */
	public NodeType getNodeType() {
		return checkIR(NodeType.class);
	}

	/**
	 * Construct IR object for this AST node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		NodeType nt = new NodeType(getDecl().getIdentNode().getIdent(),
								   getIRModifiers(), getExternalName());

		constructIR(nt);

		return nt;
	}

	/** @see de.unika.ipd.grgen.ast.ScopeOwner#fixupDefinition(de.unika.ipd.grgen.ast.IdentNode) */
    public boolean fixupDefinition(IdentNode id) {
		assert isResolved();

		boolean found = super.fixupDefinition(id, false);

		if(!found) {
			for(InheritanceTypeNode inh : extend.getChildren()) {
				boolean result = inh.fixupDefinition(id);

				if(found && result) {
					error.error(getIdentNode().getCoords(), "Identifier " + id + " is ambiguous");
				}

				found = found || result;
			}
		}

		return found;
    }

	protected void doGetCompatibleToTypes(Collection<TypeNode> coll) {
		assert isResolved();

		for(InheritanceTypeNode inh : extend.getChildren()) {
			coll.add(inh);
			coll.addAll(inh.getCompatibleToTypes());
		}
    }

	protected void constructIR(InheritanceType inhType) {
		for(BaseNode n : body.getChildren()) {
			if(n instanceof DeclNode) {
				DeclNode decl = (DeclNode)n;
				inhType.addMember(decl.getEntity());
			}
			else if(n instanceof MemberInitNode) {
				MemberInitNode mi = (MemberInitNode)n;
				inhType.addMemberInit(mi.checkIR(MemberInit.class));
			}
		}
		for(InheritanceTypeNode inh : extend.getChildren()) {
			inhType.addDirectSuperType((InheritanceType)inh.getType());
		}

		// to check overwriting of attributes
		inhType.getAllMembers();
    }

	public static String getKindStr() {
		return "node type";
	}

	public static String getUseStr() {
		return "node type";
	}

	@Override
	public Collection<NodeTypeNode> getDirectSuperTypes() {
		assert isResolved();

	    return extend.getChildren();
    }

	@Override
		protected void getMembers(Map<String, DeclNode> members) {
		assert isResolved();

		for(BaseNode n : body.getChildren()) {
			if(n instanceof DeclNode) {
				DeclNode decl = (DeclNode)n;

				DeclNode old=members.put(decl.getIdentNode().toString(), decl);
				if(old!=null && !(old instanceof AbstractMemberDeclNode)) {
					// TODO this should be part of a check (that return false)
					error.error(decl.getCoords(), "member " + decl.toString() +" of " +
									getUseString() + " " + getIdentNode() +
									" already defined in " + old.getParents() + "." // TODO improve error message
							   );
				}
			}
		}
	}
}

