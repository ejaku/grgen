/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.StorageAccess;
import de.unika.ipd.grgen.ir.pattern.Variable;

public class MatchEdgeFromStorageDeclNode extends MatchEdgeFromByStorageDeclNode
{
	static {
		setName(MatchEdgeFromStorageDeclNode.class, "match edge from storage decl");
	}

	public MatchEdgeFromStorageDeclNode(IdentNode id, BaseNode type, int context, BaseNode storage,
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
		children.add(getValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
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
				} else if(unresolved.decl instanceof EdgeDeclNode) {
					storageGlobalVariable = (EdgeDeclNode)unresolved.decl;
				} else {
					reportError("Match edge from storage expects an edge storage parameter or an edge global variable"
							+ " (but" + emptyWhenAnonymousPostfix(" ") + " is given neither).");
					successfullyResolved = false;
				}
			} else {
				reportError("Match edge from storage expects an edge storage parameter or an edge global variable"
						+ " (but" + emptyWhenAnonymousPostfix(" ") + " is given neither).");
				successfullyResolved = false;
			}
		} else if(storageUnresolved instanceof QualIdentNode) {
			QualIdentNode unresolved = (QualIdentNode)storageUnresolved;
			if(unresolved.resolve()) {
				storageAttribute = unresolved;
			} else {
				reportError("Match edge from storage attribute expects a storage attribute"
						+ " (but" + emptyWhenAnonymousPostfix(" ") + " is given " + unresolved + ").");
				successfullyResolved = false;
			}
		} else {
			reportError("Internal error - invalid match edge from storage attribute"
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
			reportError("Cannot employ match edge from storage in the rewrite part"
					+ " (as it occurs in match edge" + emptyWhenAnonymousPostfix(" ") + " from " + getStorageName() + ").");
			return false;
		}
		TypeNode storageType = getStorageType();
		if(!(storageType instanceof ContainerTypeNode)) {
			if(storageGlobalVariable == null) {
				reportError("Match edge from storage expects a collection type"
						+ " (but" + emptyWhenAnonymousPostfix(" ") + " is given " + storageType.getTypeName() + " by " + getStorageName() + ").");
				return false;
			}
		}
		TypeNode storageElementType = null;
		if(storageType instanceof ContainerTypeNode) {
			storageElementType = ((ContainerTypeNode)storageType).getElementType();
		} else {
			storageElementType = storageGlobalVariable.getDeclType();
		}
		if(!(storageElementType instanceof EdgeTypeNode)) {
			if(storageGlobalVariable == null) {
				reportError("Match edge from storage expects the element type to be an edge type"
						+ " (but" + emptyWhenAnonymousPostfix(" ") + " is given " + storageElementType.getKind() + " "
						+ storageElementType.toStringWithDeclarationCoords() + " accessing " + getStorageName() + ").");
				return false;
			} else {
				reportError("Match edge from storage global variable expects an edge type"
						+ " (but" + emptyWhenAnonymousPostfix(" ") + " is given " + storageElementType.getKind() + " "
						+ storageElementType.toStringWithDeclarationCoords() + " accessing " + getStorageName() + ").");
				return false;
			}
		}
		EdgeTypeNode storageElemType = (EdgeTypeNode)storageElementType;
		EdgeTypeNode expectedStorageElemType = getDeclType();
		if(!expectedStorageElemType.isCompatibleTo(storageElemType)) {
			String expTypeName = expectedStorageElemType.toStringWithDeclarationCoords();
			String typeName = storageElemType.toStringWithDeclarationCoords();
			ident.reportError("Cannot convert storage element type from " + typeName
					+ " to the expected " + expTypeName + " in match edge from storage"
					+ " (" + emptyWhenAnonymous("of " + getIdentNode() + " ") + "accessing " + getStorageName() + ").");
			return false;
		}
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		Edge edge = (Edge)super.constructIR();
		if(storage != null)
			edge.setStorage(new StorageAccess(storage.checkIR(Variable.class)));
		else if(storageAttribute != null)
			edge.setStorage(new StorageAccess(storageAttribute.checkIR(Qualification.class)));
		//else edge.setStorage(new StorageAccess(storageGlobalVariable.checkIR(Edge.class)));
		return edge;
	}
}
