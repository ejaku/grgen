/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
			startExpr.reportError("First argument (start position) to subarray expression must be of type int");
			return false;
		}
		if(!lengthExpr.getType().isEqual(BasicTypeNode.intType)) {
			lengthExpr.reportError("Second argument (length) to subarray expression must be of type int");
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
		return new ArraySubarrayExpr(targetExpr.checkIR(Expression.class),
				startExpr.checkIR(Expression.class),
				lengthExpr.checkIR(Expression.class));
	}
}
