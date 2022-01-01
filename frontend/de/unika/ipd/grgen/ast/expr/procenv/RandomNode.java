/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.procenv;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.procenv.RandomExpr;
import de.unika.ipd.grgen.parser.Coords;

public class RandomNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(RandomNode.class, "random");
	}

	private ExprNode numExpr;

	public RandomNode(Coords coords, ExprNode numExpr)
	{
		super(coords);

		this.numExpr = numExpr;
		becomeParent(numExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		if(numExpr != null)
			children.add(numExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		if(numExpr != null)
			childrenNames.add("maximum random number");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(numExpr != null
				&& !numExpr.getType().isEqual(BasicTypeNode.intType)) {
			numExpr.reportError("maximum random number must be of type int");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		if(numExpr != null)
			numExpr = numExpr.evaluate();
		return new RandomExpr(numExpr != null ? numExpr.checkIR(Expression.class) : null);
	}

	@Override
	public TypeNode getType()
	{
		// if a parameter was given random returns an random integer number from 0 up to excluding numExpr,
		// otherwise a random double in the range [0,1] is returned
		return numExpr != null ? BasicTypeNode.intType : BasicTypeNode.doubleType;
	}
}
