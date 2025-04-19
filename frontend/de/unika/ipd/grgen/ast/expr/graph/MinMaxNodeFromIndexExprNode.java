/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.graph.MinMaxFromIndexExpr;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the bottom node from an index with the lowest value or the top node from an index with the highest value.
 */
public class MinMaxNodeFromIndexExprNode extends FromIndexAccessExprNode
{
	static {
		setName(MinMaxNodeFromIndexExprNode.class, "min/max node from index expr");
	}

	boolean isMin;
	
	public MinMaxNodeFromIndexExprNode(Coords coords, BaseNode index, boolean isMin)
	{
		super(coords, index);
		this.isMin = isMin;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(indexUnresolved, index));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("index");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return super.resolveLocal();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		return super.checkLocal();
	}
	
	@Override
	protected IdentNode getRoot()
	{
		return getNodeRoot();
	}

	@Override
	protected String shortSignature()
	{
		return isMin ? "minNodeFromIndex(.)" : "maxNodeFromIndex(.)";
	}

	@Override
	public TypeNode getType()
	{
		return getRoot().getDecl().getDeclType();
	}

	@Override
	protected IR constructIR()
	{
		return new MinMaxFromIndexExpr(index.checkIR(Index.class), isMin,
				getType().getType());
	}
}
