/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.containers.ArrayPeekExpr;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayPeekNode extends ExprNode
{
	static {
		setName(ArrayPeekNode.class, "array peek");
	}

	private ExprNode targetExpr;
	private ExprNode numberExpr;

	public ArrayPeekNode(Coords coords, ExprNode targetExpr, ExprNode numberExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
		this.numberExpr = becomeParent(numberExpr);
	}

	public ArrayPeekNode(Coords coords, ExprNode targetExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		if(numberExpr!=null) children.add(numberExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		if(numberExpr!=null) childrenNames.add("numberExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof ArrayTypeNode)) {
			targetExpr.reportError("This argument to array peek expression must be of type array<T>");
			return false;
		}
		if(numberExpr!=null && !numberExpr.getType().isEqual(BasicTypeNode.intType)) {
			numberExpr.reportError("Argument (number) to "
					+ "array peek expression must be of type int");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return ((ArrayTypeNode)targetExpr.getType()).valueType;
	}

	@Override
	protected IR constructIR() {
		return new ArrayPeekExpr(targetExpr.checkIR(Expression.class),
				numberExpr!=null ? numberExpr.checkIR(Expression.class) : null);
	}
}
