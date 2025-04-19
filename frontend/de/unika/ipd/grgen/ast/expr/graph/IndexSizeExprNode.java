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
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.graph.IndexSizeExpr;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the size of an index, i.e. the number of elements/count of elements stored in the index.
 */
public class IndexSizeExprNode extends FromIndexAccessExprNode
{
	static {
		setName(IndexSizeExprNode.class, "index size expr");
	}

	public IndexSizeExprNode(Coords coords, BaseNode index)
	{
		super(coords, index);
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
		return true; // do not call checkLocal of super / ensure it is not called (to prevent an invalid node/edge type check)
	}
	
	@Override
	protected IdentNode getRoot()
	{
		return null;
	}

	@Override
	protected String shortSignature()
	{
		return "indexSize(.)";
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.intType;
	}

	@Override
	protected IR constructIR()
	{
		return new IndexSizeExpr(index.checkIR(Index.class),
				getType().getType());
	}
}
