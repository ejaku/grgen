/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessFromToExpr;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the edges from an index as array by accessing a range from a certain value to a certain value (one or both may be optional).
 */
public class EdgesFromIndexAccessFromToAsArrayExprNode extends FromIndexAccessFromToExprNode
{
	static {
		setName(EdgesFromIndexAccessFromToAsArrayExprNode.class, "edges from index access from to as array expr");
	}

	private ArrayTypeNode arrayTypeNode;
	private boolean ascending;

	public EdgesFromIndexAccessFromToAsArrayExprNode(Coords coords, BaseNode index, boolean ascending, ExprNode fromExpr, boolean fromExclusive, ExprNode toExpr, boolean toExclusive)
	{
		super(coords, index, fromExpr, fromExclusive, toExpr, toExclusive);
		this.ascending = ascending;
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
		return "edgesFromIndex" + fromPart() + toPart() + "AsArray" + (ascending ? "Ascending" : "Descending") + "(" + argumentsPart() + ")";
	}

	@Override
	public TypeNode getType()
	{
		return arrayTypeNode;
	}

	@Override
	protected OperatorDeclNode.Operator fromOperator()
	{
		if(ascending)
			return fromExclusive ? OperatorDeclNode.Operator.GT : OperatorDeclNode.Operator.GE;
		else
			return fromExclusive ? OperatorDeclNode.Operator.LT : OperatorDeclNode.Operator.LE;
	}

	@Override
	protected OperatorDeclNode.Operator toOperator()
	{
		if(ascending)
			return toExclusive ? OperatorDeclNode.Operator.LT : OperatorDeclNode.Operator.LE;
		else
			return toExclusive ? OperatorDeclNode.Operator.GT : OperatorDeclNode.Operator.GE;
	}

	@Override
	protected IR constructIR()
	{
		if(fromExpr != null)
			fromExpr = fromExpr.evaluate();
		if(toExpr != null)
			toExpr = toExpr.evaluate();
		return new EdgesFromIndexAccessFromToExpr(
				new IndexAccessOrdering(index.checkIR(Index.class), ascending,
						fromOperator(), fromExpr != null ? fromExpr.checkIR(Expression.class) : null, 
						toOperator(), toExpr != null ? toExpr.checkIR(Expression.class) : null),
				getType().getType());
	}
}
