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
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.NodesFromIndexAccessSameExpr;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the nodes from an index by accessing using a comparison for equality.
 */
public class NodesFromIndexAccessSameExprNode extends FromIndexAccessSameExprNode
{
	static {
		setName(NodesFromIndexAccessSameExprNode.class, "nodes from index access same expr");
	}

	private SetTypeNode setTypeNode;

	public NodesFromIndexAccessSameExprNode(Coords coords, BaseNode index, ExprNode expr)
	{
		super(coords, index, expr);
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
		return getNodeRoot();
	}

	@Override
	protected String shortSignature()
	{
		return "nodesFromIndexSame(.,.)";
	}

	@Override
	public TypeNode getType()
	{
		return setTypeNode;
	}

	@Override
	protected IR constructIR()
	{
		expr = expr.evaluate();
		return new NodesFromIndexAccessSameExpr(
				new IndexAccessEquality(index.checkIR(Index.class), expr.checkIR(Expression.class)),
				getType().getType());
	}
}
