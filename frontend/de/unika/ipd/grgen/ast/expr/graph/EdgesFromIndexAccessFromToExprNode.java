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
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.EdgesFromIndexAccessFromToExpr;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the edges from an index by accessing a range from a certain value to a certain value (one or both may be optional).
 */
public class EdgesFromIndexAccessFromToExprNode extends EdgesFromIndexAccessExprNode
{
	static {
		setName(EdgesFromIndexAccessFromToExprNode.class, "edges from index access from to expr");
	}

	private ExprNode fromExpr;
	private boolean fromExclusive;
	private ExprNode toExpr;
	private boolean toExclusive;

	public EdgesFromIndexAccessFromToExprNode(Coords coords, ExprNode index, ExprNode fromExpr, boolean fromExclusive, ExprNode toExpr, boolean toExclusive)
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
				reportError("The function " + shortSignature() + " expects as 2. argument (fromExpr) a value of type " + expTypeName
						+ " (but is given a value of type " + typeName + ").");
				return false;
			}
		}
		if(toExpr != null) {
			String argumentNumber = fromExpr != null ? "3" : "2"; 
			TypeNode toIndexAccessType = toExpr.getType();
			if(!toIndexAccessType.isCompatibleTo(expectedIndexAccessType)) {
				String expTypeName = expectedIndexAccessType.getTypeName();
				String typeName = toIndexAccessType.getTypeName();
				reportError("The function " + shortSignature() + " expects as " + argumentNumber + ". argument (toExpr) a value of type " + expTypeName
						+ " (but is given a value of type " + typeName + ").");
				return false;
			}
		}
		return res;
	}

	protected String shortSignature()
	{
		return "edgesFromIndex" + fromPart() + toPart() + "(" + argumentsPart() + ")";
	}

	private String fromPart()
	{
		if(fromExpr == null)
			return "";
		return fromExclusive ? "FromExclusive" : "From";
	}

	private String toPart()
	{
		if(toExpr == null)
			return "";
		return toExclusive ? "ToExclusive" : "To";
	}

	private String argumentsPart()
	{
		StringBuilder sb = new StringBuilder();
		sb.append(".");
		if(fromExpr != null)
			sb.append(",.");
		if(toExpr != null)
			sb.append(",.");
		return sb.toString();
	}

	@Override
	protected IR constructIR()
	{
		if(fromExpr != null)
			fromExpr = fromExpr.evaluate();
		if(toExpr != null)
			toExpr = toExpr.evaluate();
		return new EdgesFromIndexAccessFromToExpr(
				new IndexAccessOrdering(index.checkIR(Index.class), true,
						fromOperator(), fromExpr != null ? fromExpr.checkIR(Expression.class) : null, 
						toOperator(), toExpr != null ? toExpr.checkIR(Expression.class) : null),
				getType().getType());
	}

	private OperatorDeclNode.Operator fromOperator()
	{
		return fromExclusive ? OperatorDeclNode.Operator.GT : OperatorDeclNode.Operator.GE;
	}

	private OperatorDeclNode.Operator toOperator()
	{
		return toExclusive ? OperatorDeclNode.Operator.LT : OperatorDeclNode.Operator.LE;
	}
}
