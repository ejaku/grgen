/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.containers.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.StorageAccess;
import de.unika.ipd.grgen.ir.Variable;


public class MatchNodeFromStorageNode extends NodeDeclNode implements NodeCharacter  {
	static {
		setName(MatchNodeFromStorageNode.class, "match node from storage decl");
	}

	private BaseNode storageUnresolved;
	private VarDeclNode storage = null;
	private QualIdentNode storageAttribute = null;
	private NodeDeclNode storageGlobalVariable = null;


	public MatchNodeFromStorageNode(IdentNode id, BaseNode type, int context, BaseNode storage,
			PatternGraphNode directlyNestingLHSGraph) {
		super(id, type, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		this.storageUnresolved = storage;
		becomeParent(this.storageUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(getValidVersion(storageUnresolved, storage, storageAttribute, storageGlobalVariable));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("constraints");
		childrenNames.add("storage");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = super.resolveLocal();
		if(storageUnresolved instanceof IdentExprNode) {
			IdentExprNode unresolved = (IdentExprNode)storageUnresolved;
			if(unresolved.resolve()) {
				if(unresolved.decl instanceof VarDeclNode) {
					storage = (VarDeclNode)unresolved.decl;
				} else if(unresolved.decl instanceof NodeDeclNode) {
					storageGlobalVariable = (NodeDeclNode)unresolved.decl;
				} else {
					reportError("match node from storage expects a parameter or global variable.");
					successfullyResolved = false;
				}
			} else {
				reportError("match node from storage expects a parameter or global variable.");
				successfullyResolved = false;
			}
		} else if(storageUnresolved instanceof QualIdentNode) {
			QualIdentNode unresolved = (QualIdentNode)storageUnresolved;
			if(unresolved.resolve()) {
				storageAttribute = unresolved;
			} else {
				reportError("match node from storage attribute expects a storage attribute.");
				successfullyResolved = false;
			}
		} else {
			reportError("internal error - invalid match node from storage attribute");
			successfullyResolved = false;
		}
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		boolean res = super.checkLocal();
		if((context&CONTEXT_LHS_OR_RHS)==CONTEXT_RHS) {
			reportError("Can't employ match node from storage on RHS");
			return false;
		}
		TypeNode storageType = storage!=null ? storage.getDeclType() : storageGlobalVariable!=null ? storageGlobalVariable.getDeclType() : storageAttribute.getDecl().getDeclType();
		if(!(storageType instanceof SetTypeNode || storageType instanceof MapTypeNode 
				|| storageType instanceof ArrayTypeNode || storageType instanceof DequeTypeNode)) {
			if(storageGlobalVariable == null) {
				reportError("match node from storage expects a parameter variable of collection type (set|map|array|deque).");
				return false;
			}
		}
		TypeNode storageElementType = null;
		if(storageType instanceof SetTypeNode) {
			storageElementType = ((SetTypeNode)storageType).valueType;
		} else if(storageType instanceof MapTypeNode) {
			storageElementType = ((MapTypeNode)storageType).keyType;
		} else if(storageType instanceof ArrayTypeNode) {
			storageElementType = ((ArrayTypeNode)storageType).valueType;
		} else if(storageType instanceof DequeTypeNode) {
			storageElementType = ((DequeTypeNode)storageType).valueType;
		} else {
			storageElementType = storageGlobalVariable.getDeclType();
		}
		if(!(storageElementType instanceof NodeTypeNode)) {
			if(storageGlobalVariable != null) {
				reportError("match node from storage expects the element type to be a node type.");
				return false;
			} else {
				reportError("match node from storage global variable expects a node type.");
				return false;			
			}
		}
		NodeTypeNode storageElemType = (NodeTypeNode)storageElementType;
		NodeTypeNode expectedStorageElemType = getDeclType();
		if(!expectedStorageElemType.isCompatibleTo(storageElemType)) {
			String expTypeName = expectedStorageElemType.getIdentNode().toString();
			String typeName = storageElemType.getIdentNode().toString();
			ident.reportError("Cannot convert storage element type from \""
					+ typeName + "\" to \"" + expTypeName + "\" in match node from storage");
			return false;
		}
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		Node node = (Node)super.constructIR();
		if(storage!=null) node.setStorage(new StorageAccess(storage.checkIR(Variable.class)));
		else if(storageAttribute!=null) node.setStorage(new StorageAccess(storageAttribute.checkIR(Qualification.class)));
//		else node.setStorage(new StorageAccess(storageGlobalVariable.checkIR(Node.class)));
		return node;
	}
}
