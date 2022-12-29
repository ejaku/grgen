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
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.StorageAccess;
import de.unika.ipd.grgen.ir.pattern.StorageAccessIndex;
import de.unika.ipd.grgen.ir.pattern.Variable;

public class MatchEdgeByStorageAccessDeclNode extends MatchEdgeFromByStorageDeclNode
{
	static {
		setName(MatchEdgeByStorageAccessDeclNode.class, "match edge by storage access decl");
	}

	private IdentExprNode accessorUnresolved;
	private ConstraintDeclNode accessor = null;

	public MatchEdgeByStorageAccessDeclNode(IdentNode id, BaseNode type, int context,
			BaseNode storage, IdentExprNode accessor,
			PatternGraphLhsNode directlyNestingLHSGraph)
	{
		super(id, type, context, storage, directlyNestingLHSGraph);
		this.accessorUnresolved = accessor;
		becomeParent(this.accessorUnresolved);
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
		children.add(getValidVersion(accessorUnresolved, accessor));
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
		childrenNames.add("accessor");
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
					reportError("Match edge by storage access expects an edge storage parameter or an edge global variable"
							+ " (but" + emptyWhenAnonymousPostfix(" ") + " is given neither)" + ".");
					successfullyResolved = false;
				}
			} else {
				reportError("Match edge by storage access expects an edge storage parameter or an edge global variable"
						+ " (but" + emptyWhenAnonymousPostfix(" ") + " is given neither)" + ".");
				successfullyResolved = false;
			}
		} else if(storageUnresolved instanceof QualIdentNode) {
			QualIdentNode unresolved = (QualIdentNode)storageUnresolved;
			if(unresolved.resolve()) {
				storageAttribute = unresolved;
			} else {
				reportError("Match edge by storage attribute access expects a storage attribute"
						+ " (but" + emptyWhenAnonymousPostfix(" ") + " is given " + unresolved + ").");
				successfullyResolved = false;
			}
		} else {
			reportError("Internal error - invalid match edge by storage attribute"
					+ " (for " + getIdentNode() + ").");
			successfullyResolved = false;
		}

		if(accessorUnresolved.resolve() && accessorUnresolved.decl instanceof ConstraintDeclNode) {
			accessor = (ConstraintDeclNode)accessorUnresolved.decl;
		} else {
			reportError("Match edge by storage access expects a pattern element as accessor"
					+ " (but" + emptyWhenAnonymousPostfix(" ") + " is given " + accessorUnresolved + ").");
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
			reportError("Cannot employ match edge by storage access in the rewrite part"
					+ " (as it occurs in match edge" + emptyWhenAnonymousPostfix(" ") + " by " + getStorageName() + ")" + ".");
			return false;
		}
		TypeNode storageType = getStorageType();
		if(!(storageType instanceof MapTypeNode)) {
			reportError("Match edge by storage access expects a map type"
					+ " (but" + emptyWhenAnonymousPostfix(" ") + " is given " + storageType.getTypeName() + " by " + getStorageName() + ").");
			return false;
		}
		TypeNode expectedStorageKeyType = ((MapTypeNode)storageType).keyType;
		TypeNode storageKeyType = accessor.getDeclType();
		if(!storageKeyType.isCompatibleTo(expectedStorageKeyType)) {
			String expTypeName = expectedStorageKeyType.toStringWithDeclarationCoords();
			String typeName = storageKeyType.toStringWithDeclarationCoords();
			ident.reportError("Cannot convert " + typeName
					+ " to the expected map key type " + expTypeName + " in match edge by storage access"
					+ " (" + emptyWhenAnonymous("of " + getIdentNode() + " ") + "accessing " + getStorageName() + ").");
			return false;
		}
		TypeNode storageElementType = ((MapTypeNode)storageType).valueType;
		if(!(storageElementType instanceof EdgeTypeNode)) {
			reportError("Match edge by storage access expects a map mapping to an edge type"
					+ " (but" + emptyWhenAnonymousPostfix(" ") + " is given a map mapping to " 
					+ storageElementType.getKind() + " " + storageElementType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		EdgeTypeNode storageElemType = (EdgeTypeNode)storageElementType;
		EdgeTypeNode expectedStorageElemType = getDeclType();
		if(!expectedStorageElemType.isCompatibleTo(storageElemType)) {
			String expTypeName = expectedStorageElemType.toStringWithDeclarationCoords();
			String typeName = storageElemType.toStringWithDeclarationCoords();
			ident.reportError("Cannot convert map value type " + typeName
					+ " to the expected pattern element type " + expTypeName + " in match edge by storage access"
					+ " (" + emptyWhenAnonymous("of " + getIdentNode() + " ") + "accessing " + getStorageName() + ").");
			return false;
		}
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		if(isIRAlreadySet()) { // break endless recursion in case of cycle in usage
			return getIR();
		}

		Edge edge = (Edge)super.constructIR();

		setIR(edge);

		if(storage != null)
			edge.setStorage(new StorageAccess(storage.checkIR(Variable.class)));
		else if(storageAttribute != null)
			edge.setStorage(new StorageAccess(storageAttribute.checkIR(Qualification.class)));
		//else edge.setStorage(new StorageAccess(storageGlobalVariable.checkIR(Edge.class)));
		edge.setStorageIndex(new StorageAccessIndex(accessor.checkIR(GraphEntity.class)));
		return edge;
	}	
}
