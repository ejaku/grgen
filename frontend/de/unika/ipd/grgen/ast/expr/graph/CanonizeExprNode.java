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
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.CanonizeExpr;
import de.unika.ipd.grgen.parser.Coords;

public class CanonizeExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(CanonizeExprNode.class, "canonize expr");
	}

	private ExprNode graphExpr;

	public CanonizeExprNode(Coords coords, ExprNode graphExpr)
	{
		super(coords);

		this.graphExpr = becomeParent(graphExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(graphExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("graph");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(graphExpr.getType().isEqual(BasicTypeNode.graphType)) {
			return true;
		} else {
			reportError("The function canonize expects as argument a value of type graph"
					+ " (but is given a value of type " + graphExpr.getType().getTypeName() + ").");
			return false;
		}
	}

	@Override
	protected IR constructIR()
	{
		graphExpr = graphExpr.evaluate();
		return new CanonizeExpr(graphExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.stringType;
	}
}
