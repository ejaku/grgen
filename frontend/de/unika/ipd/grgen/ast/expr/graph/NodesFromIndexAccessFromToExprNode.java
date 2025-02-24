/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.NodesFromIndexAccessFromToExpr;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the nodes from an index by accessing a range from a certain value to a certain value (one or both may be optional).
 */
public class NodesFromIndexAccessFromToExprNode extends FromIndexAccessFromToExprNode
{
	static {
		setName(NodesFromIndexAccessFromToExprNode.class, "nodes from index access from to expr");
	}

	public NodesFromIndexAccessFromToExprNode(Coords coords, ExprNode index, ExprNode fromExpr, boolean fromExclusive, ExprNode toExpr, boolean toExclusive)
	{
		super(coords, index, fromExpr, fromExclusive, toExpr, toExclusive);
	}

	@Override
	protected IdentNode getRoot()
	{
		return getNodeRoot();
	}

	@Override
	protected String shortSignature()
	{
		return "nodesFromIndex" + fromPart() + toPart() + "(" + argumentsPart() + ")";
	}

	@Override
	protected IR constructIR()
	{
		if(fromExpr != null)
			fromExpr = fromExpr.evaluate();
		if(toExpr != null)
			toExpr = toExpr.evaluate();
		return new NodesFromIndexAccessFromToExpr(
				new IndexAccessOrdering(index.checkIR(Index.class), true,
						fromOperator(), fromExpr != null ? fromExpr.checkIR(Expression.class) : null, 
						toOperator(), toExpr != null ? toExpr.checkIR(Expression.class) : null),
				getType().getType());
	}
}
