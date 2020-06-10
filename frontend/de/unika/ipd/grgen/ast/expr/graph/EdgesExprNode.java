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
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.EdgesExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the edges of an edge type.
 */
public class EdgesExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(EdgesExprNode.class, "edges expr");
	}

	private ExprNode edgeType;
	private SetTypeNode setTypeNode;

	public EdgesExprNode(Coords coords, ExprNode edgeType)
	{
		super(coords);
		this.edgeType = edgeType;
		becomeParent(this.edgeType);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(edgeType);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("edge type");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		setTypeNode = new SetTypeNode(getEdgeRootOfMatchingDirectedness(edgeType));
		return setTypeNode.resolve();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(!(edgeType.getType() instanceof EdgeTypeNode)) {
			reportError("argument of edges(.) must be an edge type");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		edgeType = edgeType.evaluate();
		return new EdgesExpr(edgeType.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return setTypeNode;
	}
}
