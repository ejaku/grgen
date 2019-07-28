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
import de.unika.ipd.grgen.ir.containers.SetEmptyExpr;
import de.unika.ipd.grgen.parser.Coords;

public class SetEmptyNode extends ExprNode
{
	static {
		setName(SetEmptyNode.class, "set empty expression");
	}

	private ExprNode targetExpr;

	public SetEmptyNode(Coords coords, ExprNode targetExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof SetTypeNode)) {
			targetExpr.reportError("This argument to set empty expression must be of type set<T>");
			return false;
		}
		return true;

	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.booleanType;
	}

	@Override
	protected IR constructIR() {
		return new SetEmptyExpr(targetExpr.checkIR(Expression.class));
	}
}
