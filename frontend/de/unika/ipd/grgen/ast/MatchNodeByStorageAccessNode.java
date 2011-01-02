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

import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Variable;


public class MatchNodeByStorageAccessNode extends NodeDeclNode implements NodeCharacter  {
	static {
		setName(MatchNodeByStorageAccessNode.class, "match node by storage access decl");
	}

	private IdentExprNode storageUnresolved;
	private VarDeclNode storage = null;
	private IdentExprNode accessorUnresolved;
	private ConstraintDeclNode accessor = null;
	
	public MatchNodeByStorageAccessNode(IdentNode id, BaseNode type, int context, 
			IdentExprNode storage, IdentExprNode accessor,
			PatternGraphNode directlyNestingLHSGraph) {
		super(id, type, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph, false);
		this.storageUnresolved = storage;
		becomeParent(this.storageUnresolved);
		this.accessorUnresolved = accessor;
		becomeParent(this.accessorUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(getValidVersion(storageUnresolved, storage));
		children.add(getValidVersion(accessorUnresolved, accessor));
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
		childrenNames.add("accessor");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = super.resolveLocal();
		if(storageUnresolved.resolve() && storageUnresolved.decl instanceof VarDeclNode) {
			storage = (VarDeclNode)storageUnresolved.decl;
		} else {
			reportError("match node by storage access expects a parameter variable as storage.");
			successfullyResolved = false;
		}
		if(accessorUnresolved.resolve() && accessorUnresolved.decl instanceof ConstraintDeclNode) {
			accessor = (ConstraintDeclNode)accessorUnresolved.decl;
		} else {
			reportError("match node by storage access expects a pattern element as accessor.");
			successfullyResolved = false;
		}
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		boolean res = super.checkLocal();
		TypeNode storageType = storage.getDeclType();
		if(!(storageType instanceof MapTypeNode)) {
			reportError("match node by storage access expects a parameter variable of map type.");
			return false;
		}
		TypeNode expectedStorageKeyType = ((MapTypeNode)storageType).keyType;
		TypeNode storageKeyType = accessor.getDeclType();
		if(!storageKeyType.isCompatibleTo(expectedStorageKeyType)) {
			String expTypeName = expectedStorageKeyType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)expectedStorageKeyType).getIdentNode().toString() : expectedStorageKeyType.toString();
			String typeName = storageKeyType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)storageKeyType).getIdentNode().toString() : storageKeyType.toString();
			ident.reportError("Cannot convert storage element type from\""
					+ typeName + "\" to \"" + expTypeName + "\" in match node by storage access");
			return false;
		}
		TypeNode storageElementType = ((MapTypeNode)storageType).valueType;
		if(!(storageElementType instanceof NodeTypeNode)) {
			reportError("match node by storage access expects the target type to be a node type.");
			return false;
		}
		NodeTypeNode storageElemType = (NodeTypeNode)storageElementType;
		NodeTypeNode expectedStorageElemType = getDeclType();
		if(!expectedStorageElemType.isCompatibleTo(storageElemType)) {
			String expTypeName = expectedStorageElemType.getIdentNode().toString();
			String typeName = storageElemType.getIdentNode().toString();
			ident.reportError("Cannot convert storage element type from \""
					+ typeName + "\" to \"" + expTypeName + "\" in match node by storage access");
			return false;
		}
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		Node node = (Node)super.constructIR();
		if (isIRAlreadySet()) { // break endless recursion in case of cycle in usage
			return getIR();
		} else{
			setIR(node);			
		}
		node.setStorage(storage.checkIR(Variable.class));
		node.setAccessor(accessor.checkIR(GraphEntity.class));
		return node;
	}
}
