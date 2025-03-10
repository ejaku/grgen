/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessSameExpr;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the edges from an index as array by accessing using a comparison for equality.
 */
public class EdgesFromIndexAccessSameAsArrayExprNode extends FromIndexAccessSameExprNode
{
	static {
		setName(EdgesFromIndexAccessSameAsArrayExprNode.class, "edges from index access same as array expr");
	}

	private ArrayTypeNode arrayTypeNode;

	public EdgesFromIndexAccessSameAsArrayExprNode(Coords coords, BaseNode index, ExprNode expr)
	{
		super(coords, index, expr);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();
		arrayTypeNode = new ArrayTypeNode(getRoot());
		successfullyResolved &= arrayTypeNode.resolve();
		return successfullyResolved;
	}

	@Override
	protected IdentNode getRoot()
	{
		return getEdgeRoot();
	}

	@Override
	protected String shortSignature()
	{
		return "edgesFromIndexSameAsArray(.,.)";
	}

	@Override
	public TypeNode getType()
	{
		return arrayTypeNode;
	}

	@Override
	protected IR constructIR()
	{
		expr = expr.evaluate();
		return new EdgesFromIndexAccessSameExpr(
				new IndexAccessEquality(index.checkIR(Index.class), expr.checkIR(Expression.class)),
				getType().getType());
	}
}
