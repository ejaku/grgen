/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.ExpressionPair;
import de.unika.ipd.grgen.parser.Coords;

public class ExprPairNode extends BaseNode
{
	static {
		setName(ExprPairNode.class, "expr pair");
	}

	public ExprNode keyExpr; // first
	public ExprNode valueExpr; // second

	public ExprPairNode(Coords coords, ExprNode keyExpr, ExprNode valueExpr)
	{
		super(coords);
		this.keyExpr = becomeParent(keyExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(keyExpr);
		children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("keyExpr");
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		// All checks are done in MapInitNode
		return true;
	}

	@Override
	protected IR constructIR()
	{
		keyExpr = keyExpr.evaluate();
		valueExpr = valueExpr.evaluate();
		return new ExpressionPair(keyExpr.checkIR(Expression.class), valueExpr.checkIR(Expression.class));
	}

	public ExpressionPair getExpressionPair()
	{
		return checkIR(ExpressionPair.class);
	}

	public boolean noDefElement(String containingConstruct)
	{
		return keyExpr.noDefElement(containingConstruct) & valueExpr.noDefElement(containingConstruct);
	}

	public boolean noIteratedReference(String containingConstruct)
	{
		return keyExpr.noIteratedReference(containingConstruct) & valueExpr.noIteratedReference(containingConstruct);
	}

	public boolean iteratedNotReferenced(String iterName)
	{
		return keyExpr.iteratedNotReferenced(iterName) & valueExpr.iteratedNotReferenced(iterName);
	}
}
