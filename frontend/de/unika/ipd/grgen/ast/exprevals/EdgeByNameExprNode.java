/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.EdgeByNameExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node retrieving an edge from a name.
 */
public class EdgeByNameExprNode extends FunctionOrBuiltinFunctionInvocationBaseNode
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
			reportError("first argument of edgeByName(.,.) must be of type string");
			return false;
		}
		if(!(edgeType.getType() instanceof EdgeTypeNode)) {
			reportError("second argument of edgeByName(.,.) must be an edge type");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		return new EdgeByNameExpr(name.checkIR(Expression.class),
				edgeType.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return edgeType.getType();
	}
}
