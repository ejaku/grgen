/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.decl.pattern;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.StorageAccess;
import de.unika.ipd.grgen.ir.pattern.Variable;

public class MatchNodeFromStorageDeclNode extends MatchNodeFromByStorageDeclNode
{
	static {
		setName(MatchNodeFromStorageDeclNode.class, "match node from storage decl");
	}

	public MatchNodeFromStorageDeclNode(IdentNode id, BaseNode type, int context, BaseNode storage,
			PatternGraphLhsNode directlyNestingLHSGraph)
	{
		super(id, type, context, storage, directlyNestingLHSGraph);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(getValidVersion(storageUnresolved, storage, storageAttribute, storageGlobalVariable));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("constraints");
		childrenNames.add("storage");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();
		if(storageUnresolved instanceof IdentExprNode) {
			IdentExprNode unresolved = (IdentExprNode)storageUnresolved;
			if(unresolved.resolve()) {
				if(unresolved.decl instanceof VarDeclNode) {
					storage = (VarDeclNode)unresolved.decl;
				} else if(unresolved.decl instanceof NodeDeclNode) {
					storageGlobalVariable = (NodeDeclNode)unresolved.decl;
				} else {
					reportError("Match node from storage expects a node parameter or a global variable"
							+ " (" + getIdentNode() + " is given neither).");
					successfullyResolved = false;
				}
			} else {
				reportError("Match node from storage expects a node parameter or a global variable"
						+ " (" + getIdentNode() + " is given neither).");
				successfullyResolved = false;
			}
		} else if(storageUnresolved instanceof QualIdentNode) {
			QualIdentNode unresolved = (QualIdentNode)storageUnresolved;
			if(unresolved.resolve()) {
				storageAttribute = unresolved;
			} else {
				reportError("Match node from storage attribute expects a storage attribute"
						+ " (" + getIdentNode() + " is given " + unresolved + ").");
				successfullyResolved = false;
			}
		} else {
			reportError("Internal error - invalid match node from storage attribute"
					+ " (for " + getIdentNode() + ").");
			successfullyResolved = false;
		}
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = super.checkLocal();
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			reportError("Cannot employ match node from storage in the rewrite part.");
			return false;
		}
		TypeNode storageType = getStorageType();
		if(!(storageType instanceof ContainerTypeNode)) {
			if(storageGlobalVariable == null) {
				reportError("Match node from storage expects a parameter variable of collection type"
						+ " (" + getIdentNode() + " is given " + storageType.getTypeName() + ").");
				return false;
			}
		}
		TypeNode storageElementType = null;
		if(storageType instanceof ContainerTypeNode) {
			storageElementType = ((ContainerTypeNode)storageType).getElementType();
		} else {
			storageElementType = storageGlobalVariable.getDeclType();
		}
		if(!(storageElementType instanceof NodeTypeNode)) {
			if(storageGlobalVariable == null) {
				reportError("Match node from storage expects the element type to be a node type"
						+ " (" + getIdentNode() + " is given " + storageElementType.getTypeName() + ").");
				return false;
			} else {
				reportError("Match node from storage global variable expects a node type"
						+ " (" + getIdentNode() + " is given " + storageElementType.getTypeName() + ").");
				return false;
			}
		}
		NodeTypeNode storageElemType = (NodeTypeNode)storageElementType;
		NodeTypeNode expectedStorageElemType = getDeclType();
		if(!expectedStorageElemType.isCompatibleTo(storageElemType)) {
			String expTypeName = expectedStorageElemType.getTypeName();
			String typeName = storageElemType.getTypeName();
			ident.reportError("Cannot convert storage element type from " + typeName
					+ " to " + expTypeName + " in match node from storage"
					+ " (for " + getIdentNode() + ").");
			return false;
		}
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		Node node = (Node)super.constructIR();
		if(storage != null)
			node.setStorage(new StorageAccess(storage.checkIR(Variable.class)));
		else if(storageAttribute != null)
			node.setStorage(new StorageAccess(storageAttribute.checkIR(Qualification.class)));
		//else node.setStorage(new StorageAccess(storageGlobalVariable.checkIR(Node.class)));
		return node;
	}
}
