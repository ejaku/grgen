/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessFromToExpr;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the edges from an index by accessing a range from a certain value to a certain value (one or both may be optional).
 */
public class EdgesFromIndexAccessFromToExprNode extends FromIndexAccessFromToExprNode
{
	static {
		setName(EdgesFromIndexAccessFromToExprNode.class, "edges from index access from to expr");
	}

	private SetTypeNode setTypeNode;

	public EdgesFromIndexAccessFromToExprNode(Coords coords, BaseNode index, ExprNode fromExpr, boolean fromExclusive, ExprNode toExpr, boolean toExclusive)
	{
		super(coords, index, fromExpr, fromExclusive, toExpr, toExclusive);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();
		setTypeNode = new SetTypeNode(getRoot());
		successfullyResolved &= setTypeNode.resolve();
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
		return "edgesFromIndex" + fromPart() + toPart() + "(" + argumentsPart() + ")";
	}

	@Override
	public TypeNode getType()
	{
		return setTypeNode;
	}

	@Override
	protected IR constructIR()
	{
		if(fromExpr != null)
			fromExpr = fromExpr.evaluate();
		if(toExpr != null)
			toExpr = toExpr.evaluate();
		return new EdgesFromIndexAccessFromToExpr(
				new IndexAccessOrdering(index.checkIR(Index.class), true,
						fromOperator(), fromExpr != null ? fromExpr.checkIR(Expression.class) : null, 
						toOperator(), toExpr != null ? toExpr.checkIR(Expression.class) : null),
				getType().getType());
	}
}
