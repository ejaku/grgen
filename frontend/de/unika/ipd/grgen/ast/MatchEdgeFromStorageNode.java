/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Variable;


public class MatchEdgeFromStorageNode extends EdgeDeclNode implements EdgeCharacter {
	static {
		setName(MatchEdgeFromStorageNode.class, "match edge from storage decl");
	}

	private IdentExprNode storageUnresolved;
	private VarDeclNode storage = null;

	
	public MatchEdgeFromStorageNode(IdentNode id, BaseNode newType, int context, IdentExprNode storage, 
			PatternGraphNode directlyNestingLHSGraph) {
		super(id, newType, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph, false);
		this.storageUnresolved = storage;
		becomeParent(this.storageUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(getValidVersion(storageUnresolved, storage));
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
		if(storageUnresolved.resolve() && storageUnresolved.decl instanceof VarDeclNode) {
			storage = (VarDeclNode)storageUnresolved.decl;
		} else {
			reportError("match edge from storage expects a parameter variable.");
			successfullyResolved = false;
		}
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		boolean res = super.checkLocal();
		TypeNode storageType = storage.getDeclType();
		if(!(storageType instanceof SetTypeNode || storageType instanceof MapTypeNode)) {
			reportError("match node from storage expects a parameter variable of set or map type.");
			return false;
		}
		TypeNode storageElementType = null;
		if(storageType instanceof SetTypeNode) {
			storageElementType = ((SetTypeNode)storageType).valueType;
		} else {//targetType instanceof MapTypeNode
			storageElementType = ((MapTypeNode)storageType).keyType;
		}
		if(!(storageElementType instanceof EdgeTypeNode)) {
			reportError("match node from storage expects the element type to be an edge type.");
			return false;
		}
		EdgeTypeNode storageElemType = (EdgeTypeNode)storageElementType;
		EdgeTypeNode expectedStorageElemType = getDeclType();
		if(!expectedStorageElemType.isCompatibleTo(storageElemType)) {
			String expTypeName = expectedStorageElemType.getIdentNode().toString();
			String typeName = storageElemType.getIdentNode().toString();
			ident.reportError("Cannot convert storage element type from\""
					+ typeName + "\" to \"" + expTypeName + "\" in match edge from storage");
			return false;
		}
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		Edge edge = (Edge)super.constructIR();
		edge.setStorage(storage.checkIR(Variable.class));
		return edge;
	}
}

