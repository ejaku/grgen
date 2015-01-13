/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.containers.SetPeekExpr;
import de.unika.ipd.grgen.parser.Coords;

public class SetPeekNode extends ExprNode
{
	static {
		setName(SetPeekNode.class, "set peek");
	}

	private ExprNode targetExpr;
	private ExprNode numberExpr;

	public SetPeekNode(Coords coords, ExprNode targetExpr, ExprNode numberExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
		this.numberExpr = becomeParent(numberExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(numberExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("numberExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof SetTypeNode)) {
			targetExpr.reportError("This argument to set peek expression must be of type set<T>");
			return false;
		}
		if(!numberExpr.getType().isEqual(BasicTypeNode.intType)) {
			numberExpr.reportError("Argument (number) to "
					+ "set peek expression must be of type int");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return ((SetTypeNode)targetExpr.getType()).valueType;
	}

	@Override
	protected IR constructIR() {
		return new SetPeekExpr(targetExpr.checkIR(Expression.class),
				numberExpr.checkIR(Expression.class));
	}
}
