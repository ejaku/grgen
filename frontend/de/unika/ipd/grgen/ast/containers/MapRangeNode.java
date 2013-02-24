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
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.MapRangeExpr;
import de.unika.ipd.grgen.parser.Coords;

public class MapRangeNode extends ExprNode
{
	static {
		setName(MapSizeNode.class, "map range expression");
	}

	private ExprNode targetExpr;

	public MapRangeNode(Coords coords, ExprNode targetExpr)
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
		if(!(targetType instanceof MapTypeNode)) {
			targetExpr.reportError("This argument to map range expression must be of type map<S,T>");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return SetTypeNode.getSetType(((MapTypeNode)targetExpr.getType()).valueTypeUnresolved);
	}

	@Override
	protected IR constructIR() {
		return new MapRangeExpr(targetExpr.checkIR(Expression.class), getType().getType());
	}
}
