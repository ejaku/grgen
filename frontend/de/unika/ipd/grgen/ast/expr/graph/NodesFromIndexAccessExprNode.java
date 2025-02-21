/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the nodes from an index (base class without constraints, the constrained ones inherit from this one).
 */
public abstract class NodesFromIndexAccessExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(NodesFromIndexAccessExprNode.class, "nodes from index access expr");
	}

	protected ExprNode indexUnresolved;
	protected IndexDeclNode index;
	private SetTypeNode setTypeNode;

	protected NodesFromIndexAccessExprNode(Coords coords, ExprNode index)
	{
		super(coords);
		this.indexUnresolved = index;
		becomeParent(this.indexUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(indexUnresolved, index));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("index");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;
		if(!(indexUnresolved instanceof IdentExprNode)) {
			reportError("Index identifier (expression) required.");
			successfullyResolved = false;
		} else {
			boolean indexResolved = indexUnresolved.resolve();
			if(indexResolved) {
				if(((IdentExprNode)indexUnresolved).decl instanceof IndexDeclNode) {
					index = (IndexDeclNode)((IdentExprNode)indexUnresolved).decl;
				} else {
					reportError("Index (type) required.");
				}
			}
		}
		successfullyResolved &= index != null;
		setTypeNode = new SetTypeNode(getNodeRoot(null));
		successfullyResolved &= setTypeNode.resolve();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal()
	{
		boolean res = true;
		TypeNode expectedEntityType = getNodeRoot(null).getDecl().getDeclType();
		TypeNode entityType = index.getType();
		if(!entityType.isCompatibleTo(expectedEntityType)) {
			reportError("The function " + shortSignature() + " expects as 1. argument (index " + indexUnresolved + ") a value of type index on " + expectedEntityType.toStringWithDeclarationCoords()
					+ " (but is given a value of type index on " + entityType.toStringWithDeclarationCoords() + ").");
			return false;
		}	
		return res;
	}

	protected abstract String shortSignature();
	
	protected abstract IR constructIR();
	
	@Override
	public TypeNode getType()
	{
		return setTypeNode;
	}
}
