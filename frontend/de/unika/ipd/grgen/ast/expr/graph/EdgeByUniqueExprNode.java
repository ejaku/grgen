/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.EdgeByUniqueExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node retrieving an edge from a unique id.
 */
public class EdgeByUniqueExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(EdgeByUniqueExprNode.class, "edge by unique expr");
	}

	private ExprNode unique;
	private ExprNode edgeType;

	public EdgeByUniqueExprNode(Coords coords, ExprNode unique, ExprNode edgeType)
	{
		super(coords);
		this.unique = unique;
		becomeParent(this.unique);
		this.edgeType = edgeType;
		becomeParent(this.edgeType);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(unique);
		children.add(edgeType);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("unique");
		childrenNames.add("edgeType");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(!(unique.getType() instanceof IntTypeNode)) {
			reportError("The function edgeByUnique expects as 1. argument (uniqueIdToSearchFor) a value of type int"
					+ " (but is given a value of type " + unique.getType() + ").");
			return false;
		}
		if(!(edgeType.getType() instanceof EdgeTypeNode)) {
			reportError("The function edgeByUnique expects as 2. argument (typeToObtain) a value of type edge type"
					+ " (but is given a value of type " + edgeType.getType() + ").");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		unique = unique.evaluate();
		edgeType = edgeType.evaluate();
		return new EdgeByUniqueExpr(unique.checkIR(Expression.class),
				edgeType.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return edgeType.getType();
	}
}
