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
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.NodeByNameExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node retrieving a node from a name.
 */
public class NodeByNameExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(NodeByNameExprNode.class, "node by name expr");
	}

	private ExprNode name;
	private ExprNode nodeType;

	public NodeByNameExprNode(Coords coords, ExprNode name, ExprNode nodeType)
	{
		super(coords);
		this.name = name;
		becomeParent(this.name);
		this.nodeType = nodeType;
		becomeParent(this.nodeType);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(name);
		children.add(nodeType);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("name");
		childrenNames.add("nodeType");
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
			reportError("The function nodeByName expects as 1. argument (nameToSearchFor) a value of type string"
					+ " (but is given a value of type " + name.getType().getTypeName() + ").");
			return false;
		}
		if(!(nodeType.getType() instanceof NodeTypeNode)) {
			reportError("The function nodeByName expects as 2. argument (typeToObain) a value of type node type"
					+ " (but is given a value of type " + nodeType.getType().getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		name = name.evaluate();
		nodeType = nodeType.evaluate();
		return new NodeByNameExpr(name.checkIR(Expression.class),
				nodeType.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return nodeType.getType();
	}
}
