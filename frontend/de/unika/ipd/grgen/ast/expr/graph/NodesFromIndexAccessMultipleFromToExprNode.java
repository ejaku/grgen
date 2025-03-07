/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.HashSet;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.graph.NodesFromIndexAccessMultipleFromToExpr;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the nodes from multiple indices (by accessing a range from a certain value to a certain value, each time).
 */
public class NodesFromIndexAccessMultipleFromToExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(NodesFromIndexAccessMultipleFromToExprNode.class, "nodes from index access multiple from to expr");
	}

	protected CollectNode<FromIndexAccessFromToPartExprNode> indexAccessExprs = new CollectNode<FromIndexAccessFromToPartExprNode>();
	private SetTypeNode setTypeNode;

	public NodesFromIndexAccessMultipleFromToExprNode(Coords coords)
	{
		super(coords);

		this.indexAccessExprs = becomeParent(indexAccessExprs);
	}

	public void addIndexAccessExpr(FromIndexAccessFromToPartExprNode expr)
	{
		indexAccessExprs.addChild(expr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(indexAccessExprs);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("indexAccessExprs");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;
		setTypeNode = new SetTypeNode(getRoot());
		successfullyResolved &= setTypeNode.resolve();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean successfullyChecked = true;
		
		TypeNode expectedEntityType = getRoot().getDecl().getDeclType();
		int indexShift = 0;
		for(FromIndexAccessFromToPartExprNode indexAccessExpr : indexAccessExprs.getChildren()) {
			TypeNode entityType = indexAccessExpr.index.getType();
			if(!entityType.isCompatibleTo(expectedEntityType)) {
				int indexArgumentNumber = 1 + indexShift;
				reportError("The function " + shortSignature() + " expects as " + indexArgumentNumber + ". argument (index) a value of type index on " + expectedEntityType.toStringWithDeclarationCoords()
						+ " (but is given a value of type index on " + entityType.toStringWithDeclarationCoords() + ").");
				successfullyChecked = false;
			}
			indexShift += 3;
		}
		
		if(!successfullyChecked)
			return false;
		
		for(int i = 0; i < indexAccessExprs.getChildren().size(); ++i) {
			FromIndexAccessFromToPartExprNode indexAccessExpr = indexAccessExprs.get(i);
			InheritanceTypeNode entityType = indexAccessExpr.index.getType();

			for(int j = i + 1; j < indexAccessExprs.getChildren().size(); ++j) {
				FromIndexAccessFromToPartExprNode indexAccessExpr2 = indexAccessExprs.get(j);
				InheritanceTypeNode entityType2 = indexAccessExpr2.index.getType();
				
				if(!InheritanceTypeNode.hasCommonSubtype(entityType, entityType2)) {
					reportWarning("The indexed type " + entityType.toStringWithDeclarationCoords() + " of the " + (i*3 + 1) + ". argument (index)"
									+ " and the indexed type " + entityType2.toStringWithDeclarationCoords() + " of the " + (j*3 + 1) + ". argument (index)"
									+ " have no common subtype, thus the content of these indices is disjoint, and the index join will always be empty.");
				}
			}
		}
		
		indexShift = 0;
		HashSet<IndexDeclNode> indicesUsed = new HashSet<IndexDeclNode>();
		for(FromIndexAccessFromToPartExprNode indexAccessExpr : indexAccessExprs.getChildren()) {
			int indexArgumentNumber = 1 + indexShift;
			if(indicesUsed.contains(indexAccessExpr.index)) {
				reportWarning("The function " + shortSignature() + " uses as " + indexArgumentNumber + ". argument (index) the index " + ((IdentExprNode)indexAccessExpr.indexUnresolved).decl.getDecl().toString()
						+ " for another time (combine the queried ranges into one).");
			} else {
				indicesUsed.add(indexAccessExpr.index);
			}
			indexShift += 3;
		}
		
		return true;
	}

	protected IdentNode getRoot()
	{
		return getNodeRoot();
	}

	protected String shortSignature()
	{
		return "nodesFromIndexMultipleFromTo" + "(" + argumentsPart() + ")";
	}

	protected String argumentsPart()
	{
		StringBuilder sb = new StringBuilder();
		boolean first = true;
		for(@SuppressWarnings("unused") FromIndexAccessFromToExprNode indexAccessExpr : indexAccessExprs.getChildren())
		{
			if(first) {
				first = false;
			} else {
				sb.append(",");
			}
			sb.append(".,.,.");
		}
		return sb.toString();
	}

	@Override
	public TypeNode getType()
	{
		return setTypeNode;
	}

	@Override
	protected IR constructIR()
	{
		Vector<IndexAccessOrdering> indexAccesses = new Vector<IndexAccessOrdering>();
		for(FromIndexAccessFromToPartExprNode indexAccessExpr : indexAccessExprs.getChildren())
		{
			indexAccesses.add(indexAccessExpr.constructIRPart());
		}
		return new NodesFromIndexAccessMultipleFromToExpr(indexAccesses, getType().getType());
	}
}
