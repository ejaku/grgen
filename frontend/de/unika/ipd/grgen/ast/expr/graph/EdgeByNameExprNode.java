/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.EdgeByNameExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node retrieving an edge from a name.
 */
public class EdgeByNameExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(EdgeByNameExprNode.class, "edge by name expr");
	}

	private ExprNode name;
	private ExprNode edgeType;

	public EdgeByNameExprNode(Coords coords, ExprNode name, ExprNode edgeType)
	{
		super(coords);
		this.name = name;
		becomeParent(this.name);
		this.edgeType = edgeType;
		becomeParent(this.edgeType);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(name);
		children.add(edgeType);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("name");
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
		if(!(name.getType() instanceof StringTypeNode)) {
			reportError("The function edgeByName expects as 1. argument (nameToSearchFor) a value of type string"
					+ " (but is given a value of type " + name.getType().getTypeName() + ").");
			return false;
		}
		if(!(edgeType.getType() instanceof EdgeTypeNode)) {
			reportError("The function edgeByName expects as 2. argument (typeToObtain) a value of type edge type"
					+ " (but is given a value of type " + edgeType.getType().getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		name = name.evaluate();
		edgeType = edgeType.evaluate();
		return new EdgeByNameExpr(name.checkIR(Expression.class),
				edgeType.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return edgeType.getType();
	}
}
