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
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.CountEdgesFromIndexAccessSameExpr;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the count of edges from an index by accessing using a comparison for equality.
 */
public class CountEdgesFromIndexAccessSameExprNode extends FromIndexAccessSameExprNode
{
	static {
		setName(CountEdgesFromIndexAccessSameExprNode.class, "count edges from index access same expr");
	}

	public CountEdgesFromIndexAccessSameExprNode(Coords coords, BaseNode index, ExprNode expr)
	{
		super(coords, index, expr);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();
		successfullyResolved &= getType().resolve();
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
		return "countEdgesFromIndexSame(.,.)";
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.intType;
	}

	@Override
	protected IR constructIR()
	{
		expr = expr.evaluate();
		return new CountEdgesFromIndexAccessSameExpr(
				new IndexAccessEquality(index.checkIR(Index.class), expr.checkIR(Expression.class)),
				getType().getType());
	}
}
