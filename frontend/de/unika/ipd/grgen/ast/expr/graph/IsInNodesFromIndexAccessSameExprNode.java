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
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.IsInNodesFromIndexAccessSameExpr;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding whether the given node is in the nodes from an index by accessing using a comparison for equality.
 */
public class IsInNodesFromIndexAccessSameExprNode extends FromIndexAccessSameExprNode
{
	static {
		setName(IsInNodesFromIndexAccessSameExprNode.class, "is in nodes from index access same expr");
	}

	private ExprNode candidateExpr;
	
	public IsInNodesFromIndexAccessSameExprNode(Coords coords, ExprNode candidateExpr, BaseNode index, ExprNode expr)
	{
		super(coords, index, expr);
		this.candidateExpr = candidateExpr;
		becomeParent(this.candidateExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(candidateExpr);
		children.add(getValidVersion(indexUnresolved, index));
		children.add(expr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("candidateExpr");
		childrenNames.add("index");
		childrenNames.add("expr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();
		successfullyResolved &= candidateExpr.resolve();
		successfullyResolved &= getType().resolve();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = super.checkLocal();
		TypeNode indexedEntityRootType = getRoot().getDecl().getDeclType();
		TypeNode candidateType = candidateExpr.getType();
		if(!candidateType.isCompatibleTo(indexedEntityRootType)) {
			reportError("The function " + shortSignature() + " expects as 1. argument (candidateExpr) a value of type " + indexedEntityRootType
					+ " (but is given a value of type " + candidateType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		return res;
	}

	@Override
	protected int indexShift()
	{
		return 1;
	}

	@Override
	protected IdentNode getRoot()
	{
		return getNodeRoot();
	}

	@Override
	protected String shortSignature()
	{
		return "isInNodesFromIndexSame(.,.,.)";
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.booleanType;
	}

	@Override
	protected IR constructIR()
	{
		candidateExpr = candidateExpr.evaluate();
		expr = expr.evaluate();
		return new IsInNodesFromIndexAccessSameExpr(candidateExpr.checkIR(Expression.class),
				new IndexAccessEquality(index.checkIR(Index.class), expr.checkIR(Expression.class)),
				getType().getType());
	}
}
