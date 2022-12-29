/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArraySubarrayExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArraySubarrayNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArraySubarrayNode.class, "array subarray");
	}

	private ExprNode startExpr;
	private ExprNode lengthExpr;

	public ArraySubarrayNode(Coords coords, ExprNode targetExpr, ExprNode startExpr, ExprNode lengthExpr)
	{
		super(coords, targetExpr);
		this.startExpr = becomeParent(startExpr);
		this.lengthExpr = becomeParent(lengthExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(startExpr);
		children.add(lengthExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("startExpr");
		childrenNames.add("lengthExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		if(!startExpr.getType().isEqual(BasicTypeNode.intType)) {
			startExpr.reportError("The array function method subarray expects as 1. argument (start position) a value of type int"
					+ " (but is given a value of type " + startExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!lengthExpr.getType().isEqual(BasicTypeNode.intType)) {
			lengthExpr.reportError("The array function method subarray expects as 2. argument (length) a value of type int"
					+ " (but is given a value of type " + lengthExpr.getType().getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return getTargetType();
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		startExpr = startExpr.evaluate();
		lengthExpr = lengthExpr.evaluate();
		return new ArraySubarrayExpr(targetExpr.checkIR(Expression.class),
				startExpr.checkIR(Expression.class),
				lengthExpr.checkIR(Expression.class));
	}
}
