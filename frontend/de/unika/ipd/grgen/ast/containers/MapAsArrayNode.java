/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.containers.MapAsArrayExpr;
import de.unika.ipd.grgen.parser.Coords;

public class MapAsArrayNode extends ExprNode
{
	static {
		setName(MapAsArrayNode.class, "map as array expression");
	}

	private ExprNode targetExpr;

	public MapAsArrayNode(Coords coords, ExprNode targetExpr)
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
	protected boolean resolveLocal() {
		getType().resolve(); // call to ensure the set type exists
		return true;
	}
	
	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof MapTypeNode)) {
			targetExpr.reportError("This argument to map asArray expression must be of type map<int,T>");
			return false;
		}
		MapTypeNode targetMapType = (MapTypeNode)targetType;
		if(!(targetMapType.keyType instanceof IntTypeNode)) {
			targetExpr.reportError("This argument to map asArray expression must be of type map<int,T>");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return ArrayTypeNode.getArrayType(((MapTypeNode)targetExpr.getType()).valueTypeUnresolved);
	}

	@Override
	protected IR constructIR() {
		return new MapAsArrayExpr(targetExpr.checkIR(Expression.class), getType().getType());
	}
}
