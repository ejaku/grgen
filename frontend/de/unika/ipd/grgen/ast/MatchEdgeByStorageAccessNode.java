/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ast.containers.*;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Qualification;
import de.unika.ipd.grgen.ir.StorageAccess;
import de.unika.ipd.grgen.ir.StorageAccessIndex;
import de.unika.ipd.grgen.ir.Variable;


public class MatchEdgeByStorageAccessNode extends EdgeDeclNode implements EdgeCharacter  {
	static {
		setName(MatchEdgeByStorageAccessNode.class, "match edge by storage access decl");
	}

	private BaseNode storageUnresolved;
	private VarDeclNode storage = null;
	private QualIdentNode storageAttribute = null;
	private EdgeDeclNode storageGlobalVariable = null;
	private IdentExprNode accessorUnresolved;
	private ConstraintDeclNode accessor = null;
	// TODO: auch vardeclnode f�r int et al, und qualidentnode f�r attribute zulassen

	public MatchEdgeByStorageAccessNode(IdentNode id, BaseNode type, int context,
			BaseNode storage, IdentExprNode accessor,
			PatternGraphNode directlyNestingLHSGraph) {
		super(id, type, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
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
		children.add(getValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(getValidVersion(storageUnresolved, storage, storageAttribute, storageGlobalVariable));
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
		if(storageUnresolved instanceof IdentExprNode) {
			IdentExprNode unresolved = (IdentExprNode)storageUnresolved;
			if(unresolved.resolve()) {
				if(unresolved.decl instanceof VarDeclNode) {
					storage = (VarDeclNode)unresolved.decl;
				} else if(unresolved.decl instanceof EdgeDeclNode) {
					storageGlobalVariable = (EdgeDeclNode)unresolved.decl;
				} else {
					reportError("match edge by storage access expects a parameter or global variable.");
					successfullyResolved = false;
				}
			} else {
				reportError("match edge by storage access expects a parameter or global variable.");
				successfullyResolved = false;
			}
		} else if(storageUnresolved instanceof QualIdentNode) {
			QualIdentNode unresolved = (QualIdentNode)storageUnresolved;
			if(unresolved.resolve()) {
				storageAttribute = unresolved;
			} else {
				reportError("match edge by storage attribute access expects a storage attribute.");
				successfullyResolved = false;
			}
		} else {
			reportError("internal error - invalid match edge by storage attribute");
			successfullyResolved = false;
		}

		if(accessorUnresolved.resolve() && accessorUnresolved.decl instanceof ConstraintDeclNode) {
			accessor = (ConstraintDeclNode)accessorUnresolved.decl;
		} else {
			reportError("match edge by storage access expects a pattern element as accessor.");
			successfullyResolved = false;
		}
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		boolean res = super.checkLocal();
		if((context&CONTEXT_LHS_OR_RHS)==CONTEXT_RHS) {
			reportError("Can't employ match edge by storage on RHS");
			return false;
		}
		TypeNode storageType = storage!=null ? storage.getDeclType() : storageGlobalVariable!=null ? storageGlobalVariable.getDeclType() : storageAttribute.getDecl().getDeclType();
		if(!(storageType instanceof MapTypeNode)) { // TODO: allow array/deque
			reportError("match edge by storage access expects a parameter variable of map type.");
			return false;
		}
		TypeNode expectedStorageKeyType = ((MapTypeNode)storageType).keyType;
		TypeNode storageKeyType = accessor.getDeclType();
		if(!storageKeyType.isCompatibleTo(expectedStorageKeyType)) {
			String expTypeName = expectedStorageKeyType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)expectedStorageKeyType).getIdentNode().toString() : expectedStorageKeyType.toString();
			String typeName = storageKeyType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)storageKeyType).getIdentNode().toString() : storageKeyType.toString();
			ident.reportError("Cannot convert storage element type from \""
					+ typeName + "\" to \"" + expTypeName + "\" in match edge by storage access");
			return false;
		}
		TypeNode storageElementType = ((MapTypeNode)storageType).valueType;
		if(!(storageElementType instanceof EdgeTypeNode)) {
			reportError("match edge by storage access expects the target type to be an edge type.");
			return false;
		}
		EdgeTypeNode storageElemType = (EdgeTypeNode)storageElementType;
		EdgeTypeNode expectedStorageElemType = getDeclType();
		if(!expectedStorageElemType.isCompatibleTo(storageElemType)) {
			String expTypeName = expectedStorageElemType.getIdentNode().toString();
			String typeName = storageElemType.getIdentNode().toString();
			ident.reportError("Cannot convert storage element type from \""
					+ typeName + "\" to \"" + expTypeName + "\" match edge by storage access");
			return false;
		}
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		Edge edge = (Edge)super.constructIR();
		if (isIRAlreadySet()) { // break endless recursion in case of cycle in usage
			return getIR();
		} else{
			setIR(edge);
		}
		if(storage!=null) edge.setStorage(new StorageAccess(storage.checkIR(Variable.class)));
		else if(storageAttribute!=null) edge.setStorage(new StorageAccess(storageAttribute.checkIR(Qualification.class)));
//		else edge.setStorage(new StorageAccess(storageGlobalVariable.checkIR(Edge.class)));
		edge.setStorageIndex(new StorageAccessIndex(accessor.checkIR(GraphEntity.class)));
		return edge;
	}
}
