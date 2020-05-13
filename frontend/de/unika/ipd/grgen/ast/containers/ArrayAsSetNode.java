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
import de.unika.ipd.grgen.ir.containers.ArrayAsSetExpr;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayAsSetNode extends ContainerFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayAsSetNode.class, "array as set expression");
	}

	private SetTypeNode setTypeNode;

	public ArrayAsSetNode(Coords coords, ExprNode targetExpr)
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
		setTypeNode = new SetTypeNode(((ArrayTypeNode)targetExpr.getType()).valueTypeUnresolved);
		return setTypeNode.resolve();
	}

	@Override
	protected boolean checkLocal()
	{
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof ArrayTypeNode)) {
			targetExpr.reportError("This argument to array as set expression must be of type array<T>");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return setTypeNode;
	}

	@Override
	protected IR constructIR()
	{
		return new ArrayAsSetExpr(targetExpr.checkIR(Expression.class), getType().getType());
	}
}
