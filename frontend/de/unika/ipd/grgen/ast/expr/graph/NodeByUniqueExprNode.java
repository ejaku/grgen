/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.IntTypeNode;
import de.unika.ipd.grgen.ast.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.NodeByUniqueExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node retrieving a node from a unique id.
 */
public class NodeByUniqueExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(NodeByUniqueExprNode.class, "node by unique expr");
	}

	private ExprNode unique;
	private ExprNode nodeType;

	public NodeByUniqueExprNode(Coords coords, ExprNode unique, ExprNode nodeType)
	{
		super(coords);
		this.unique = unique;
		becomeParent(this.unique);
		this.nodeType = nodeType;
		becomeParent(this.nodeType);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(unique);
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
		if(!(unique.getType() instanceof IntTypeNode)) {
			reportError("first argument of nodeByUnique(.,.) must be of type int");
			return false;
		}
		if(!(nodeType.getType() instanceof NodeTypeNode)) {
			reportError("second argument of nodeByUnique(.,.) must be a node type");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		return new NodeByUniqueExpr(unique.checkIR(Expression.class),
				nodeType.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return nodeType.getType();
	}
}
