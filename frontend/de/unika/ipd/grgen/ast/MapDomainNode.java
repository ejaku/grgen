/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: MapAccessExprNode.java 22995 2008-10-18 14:51:18Z buchwald $
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MapDomainExpr;
import de.unika.ipd.grgen.parser.Coords;

public class MapDomainNode extends ExprNode
{
	static {
		setName(MapSizeNode.class, "map domain expression");
	}

	ExprNode targetExpr;

	public MapDomainNode(Coords coords, ExprNode targetExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		return childrenNames;
	}

	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof MapTypeNode)) {
			targetExpr.reportError("Argument to map size expression must be of type map<S,T>");
			return false;
		}
		return true;
	}

	public TypeNode getType() {
		return SetTypeNode.getSetType(((MapTypeNode)targetExpr.getType()).keyTypeUnresolved);
	}

	protected IR constructIR() {
		return new MapDomainExpr(targetExpr.checkIR(Expression.class), getType().getType());
	}
}
