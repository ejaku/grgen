/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.Variable;


public class MatchNodeFromStorageNode extends NodeDeclNode implements NodeCharacter  {
	static {
		setName(MatchNodeFromStorageNode.class, "match node from storage decl");
	}

	private BaseNode storageUnresolved;
	private VarDeclNode storage = null;
	private QualIdentNode storageAttribute = null;


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
		children.add(getValidVersion(storageUnresolved, storage, storageAttribute));
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
			if(unresolved.resolve() && unresolved.decl instanceof VarDeclNode) {
				storage = (VarDeclNode)unresolved.decl;
			} else {
				reportError("match node from storage expects a parameter variable.");
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
		TypeNode storageType = storage!=null ? storage.getDeclType() : storageAttribute.getDecl().getDeclType();
		if(!(storageType instanceof SetTypeNode || storageType instanceof MapTypeNode || storageType instanceof ArrayTypeNode)) {
			reportError("match node from storage expects a parameter variable of set/map/array type.");
			return false;
		}
		TypeNode storageElementType = null;
		if(storageType instanceof SetTypeNode) {
			storageElementType = ((SetTypeNode)storageType).valueType;
		} else if(storageType instanceof MapTypeNode) {
			storageElementType = ((MapTypeNode)storageType).keyType;
		} else {//if(storageType instanceof ArrayTypeNode)
			storageElementType = ((ArrayTypeNode)storageType).valueType;
		}
		if(!(storageElementType instanceof NodeTypeNode)) {
			reportError("match node from storage expects the element type to be a node type.");
			return false;
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
		if(storage!=null) node.setStorage(storage.checkIR(Variable.class));
		else node.setStorageAttribute(storageAttribute.checkIR(Qualification.class));
		return node;
	}
}
