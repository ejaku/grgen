/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.map;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.typedecl.ArrayTypeNode;
import de.unika.ipd.grgen.ast.typedecl.IntTypeNode;
import de.unika.ipd.grgen.ast.typedecl.MapTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.map.MapAsArrayExpr;
import de.unika.ipd.grgen.parser.Coords;

public class MapAsArrayNode extends MapFunctionMethodInvocationBaseExprNode
{
	static {
		setName(MapAsArrayNode.class, "map as array expression");
	}

	private ArrayTypeNode arrayTypeNode;

	public MapAsArrayNode(Coords coords, ExprNode targetExpr)
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
		// target type already checked during resolving into this node
		arrayTypeNode = new ArrayTypeNode(getTargetType().valueTypeUnresolved);
		return arrayTypeNode.resolve();
	}

	@Override
	protected boolean checkLocal()
	{
		MapTypeNode targetMapType = getTargetType();
		if(!(targetMapType.keyType instanceof IntTypeNode)) {
			targetExpr.reportError("This argument to map asArray expression must be of type map<int,T>");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return arrayTypeNode;
	}

	@Override
	protected IR constructIR()
	{
		return new MapAsArrayExpr(targetExpr.checkIR(Expression.class), getType().getType());
	}
}
