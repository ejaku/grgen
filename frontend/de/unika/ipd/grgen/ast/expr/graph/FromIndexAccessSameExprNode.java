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
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the graph elements (nodes or edges) from an index by accessing using a comparison for equality (base class for the specific node or edge classes).
 */
public abstract class FromIndexAccessSameExprNode extends FromIndexAccessExprNode
{
	static {
		setName(FromIndexAccessSameExprNode.class, "from index access same expr");
	}

	protected ExprNode expr;

	public FromIndexAccessSameExprNode(Coords coords, BaseNode index, ExprNode expr)
	{
		super(coords, index);
		this.expr = expr;
		becomeParent(this.expr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(indexUnresolved, index));
		children.add(expr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("index");
		childrenNames.add("expr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();
		successfullyResolved &= expr.resolve();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = super.checkLocal();
		TypeNode expectedIndexAccessType = index.getExpectedAccessType();
		TypeNode indexAccessType = expr.getType();
		if(!indexAccessType.isCompatibleTo(expectedIndexAccessType)) {
			String expTypeName = expectedIndexAccessType.getTypeName();
			String typeName = indexAccessType.getTypeName();
			int argumentNumber = 2 + indexShift();
			reportError("The function " + shortSignature() + " expects as " + argumentNumber + ". argument (expr) a value of type " + expTypeName
					+ " (but is given a value of type " + typeName + ").");
			return false;
		}
		return res;
	}
}
