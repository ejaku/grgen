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
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the graph elements (nodes or edges) from an index by accessing a range from a certain value to a certain value (one or both may be optional) (base class for the specific node or edge versions).
 */
public abstract class FromIndexAccessFromToExprNode extends FromIndexAccessExprNode
{
	static {
		setName(FromIndexAccessFromToExprNode.class, "from index access from to expr");
	}

	protected ExprNode fromExpr;
	protected boolean fromExclusive;
	protected ExprNode toExpr;
	protected boolean toExclusive;

	public FromIndexAccessFromToExprNode(Coords coords, BaseNode index, ExprNode fromExpr, boolean fromExclusive, ExprNode toExpr, boolean toExclusive)
	{
		super(coords, index);
		this.fromExpr = fromExpr;
		becomeParent(this.fromExpr);
		this.fromExclusive = fromExclusive;
		this.toExpr = toExpr;
		becomeParent(this.toExpr);
		this.toExclusive = toExclusive;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(indexUnresolved, index));
		if(fromExpr != null)
			children.add(fromExpr);
		if(toExpr != null)
			children.add(toExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("index");
		if(fromExpr != null)
			childrenNames.add("fromExpr");
		if(toExpr != null)
			childrenNames.add("toExpr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();
		if(fromExpr != null)
			successfullyResolved &= fromExpr.resolve();
		if(toExpr != null)
			successfullyResolved &= toExpr.resolve();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = super.checkLocal();
		TypeNode expectedIndexAccessType = index.getExpectedAccessType();
		if(fromExpr != null) {
			TypeNode fromIndexAccessType = fromExpr.getType();
			if(!fromIndexAccessType.isCompatibleTo(expectedIndexAccessType)) {
				String expTypeName = expectedIndexAccessType.getTypeName();
				String typeName = fromIndexAccessType.getTypeName();
				int fromArgumentNumber = 2 + indexShift();
				reportError("The function " + shortSignature() + " expects as " + fromArgumentNumber + ". argument (fromExpr) a value of type " + expTypeName
						+ " (but is given a value of type " + typeName + ").");
				return false;
			}
		}
		if(toExpr != null) {
			TypeNode toIndexAccessType = toExpr.getType();
			if(!toIndexAccessType.isCompatibleTo(expectedIndexAccessType)) {
				String expTypeName = expectedIndexAccessType.getTypeName();
				String typeName = toIndexAccessType.getTypeName();
				int toArgumentNumber = (fromExpr != null ? 3 : 2) + indexShift();
				reportError("The function " + shortSignature() + " expects as " + toArgumentNumber + ". argument (toExpr) a value of type " + expTypeName
						+ " (but is given a value of type " + typeName + ").");
				return false;
			}
		}
		return res;
	}

	protected String fromPart()
	{
		if(fromExpr == null)
			return "";
		return fromExclusive ? "FromExclusive" : "From";
	}

	protected String toPart()
	{
		if(toExpr == null)
			return "";
		return toExclusive ? "ToExclusive" : "To";
	}

	protected String argumentsPart()
	{
		StringBuilder sb = new StringBuilder();
		sb.append(".");
		if(fromExpr != null)
			sb.append(",.");
		if(toExpr != null)
			sb.append(",.");
		return sb.toString();
	}

	protected OperatorDeclNode.Operator fromOperator()
	{
		return fromExclusive ? OperatorDeclNode.Operator.GT : OperatorDeclNode.Operator.GE;
	}

	protected OperatorDeclNode.Operator toOperator()
	{
		return toExclusive ? OperatorDeclNode.Operator.LT : OperatorDeclNode.Operator.LE;
	}
}
