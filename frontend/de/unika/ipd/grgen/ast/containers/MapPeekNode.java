/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.MapPeekExpr;
import de.unika.ipd.grgen.parser.Coords;

public class MapPeekNode extends ExprNode
{
	static {
		setName(MapPeekNode.class, "map peek");
	}

	private ExprNode targetExpr;
	private ExprNode numberExpr;

	public MapPeekNode(Coords coords, ExprNode targetExpr, ExprNode numberExpr)
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
		if(!(targetType instanceof MapTypeNode)) {
			targetExpr.reportError("This argument to map peek expression must be of type map<S,T>");
			return false;
		}
		if(!numberExpr.getType().isEqual(BasicTypeNode.intType)) {
			numberExpr.reportError("Argument (number) to "
					+ "map peek expression must be of type int");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return ((MapTypeNode)targetExpr.getType()).keyType;
	}

	@Override
	protected IR constructIR() {
		return new MapPeekExpr(targetExpr.checkIR(Expression.class),
				numberExpr.checkIR(Expression.class));
	}
}
