/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.ArrayAsDequeExpr;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayAsDequeNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayAsDequeNode.class, "array as deque expression");
	}

	private DequeTypeNode dequeTypeNode;

	public ArrayAsDequeNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		// target type already checked during resolving into this node
		dequeTypeNode = new DequeTypeNode(getTargetType().valueTypeUnresolved);
		return dequeTypeNode.resolve();
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return dequeTypeNode;
	}

	@Override
	protected IR constructIR()
	{
		return new ArrayAsDequeExpr(targetExpr.checkIR(Expression.class), getType().getType());
	}
}
