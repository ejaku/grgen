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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the graph elements (nodes or edges) from an index by accessing a range from a certain value to a certain value (part class to be used in a multiple index query).
 */
public class FromIndexAccessFromToPartExprNode extends FromIndexAccessFromToExprNode
{
	static {
		setName(FromIndexAccessFromToPartExprNode.class, "from index access from to part expr");
	}

	int indexShiftCausedByPartNumber;
	FromIndexAccessMultipleFromToExprNode wholeExpr;
	
	public FromIndexAccessFromToPartExprNode(Coords coords, BaseNode index, ExprNode fromExpr, boolean fromExclusive, ExprNode toExpr, boolean toExclusive, int indexShiftCausedByPartNumber, FromIndexAccessMultipleFromToExprNode wholeExpr)
	{
		super(coords, index, fromExpr, fromExclusive, toExpr, toExclusive);
		this.indexShiftCausedByPartNumber = indexShiftCausedByPartNumber;
		this.wholeExpr = wholeExpr;
	}

	protected int indexShift() // the parts in a multiple from to index query are shifted by 3 from each other
	{
		return indexShiftCausedByPartNumber;
	}

	@Override
	protected IdentNode getRoot()
	{
		return wholeExpr.getRoot();
	}

	@Override
	protected String shortSignature()
	{
		return wholeExpr.shortSignature();
	}

	public IndexAccessOrdering constructIRPart()
	{
		if(fromExpr != null)
			fromExpr = fromExpr.evaluate();
		if(toExpr != null)
			toExpr = toExpr.evaluate();
		return new IndexAccessOrdering(index.checkIR(Index.class), true,
				fromOperator(), fromExpr != null ? fromExpr.checkIR(Expression.class) : null, 
				toOperator(), toExpr != null ? toExpr.checkIR(Expression.class) : null);
	}

	@Override
	protected IR constructIR()
	{
		throw new RuntimeException("Not implemented! Only used as part class.");
	}

	@Override
	public TypeNode getType()
	{
		throw new RuntimeException("Not implemented! Only used as part class.");
	}
}
