/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.SinCosTanExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class SinCosTanExprNode extends FuncBaseNode
{
	static {
		setName(SinCosTanExprNode.class, "sincostan expr");
	}

	public enum TrigonometryFunctionType
	{
		sin, cos, tan
	}

	TrigonometryFunctionType which;
	private ExprNode argumentExpr;

	public SinCosTanExprNode(Coords coords, TrigonometryFunctionType which, ExprNode argumentExpr)
	{
		super(coords);

		this.which = which;
		this.argumentExpr = becomeParent(argumentExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(argumentExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("arg");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(argumentExpr.getType().isEqual(BasicTypeNode.doubleType)) {
			return true;
		}
		reportError("The only admissible type for " + which + "(.) is: (double)");
		return false;
	}

	@Override
	protected IR constructIR()
	{
		// assumes that the which:int of the AST node uses the same values as the which of the IR expression
		return new SinCosTanExpr(getTrigonometryFunctionType(), argumentExpr.checkIR(Expression.class));
	}

	private SinCosTanExpr.TrigonometryFunctionType getTrigonometryFunctionType()
	{
		switch(which) {
		case sin: return SinCosTanExpr.TrigonometryFunctionType.sin;
		case cos: return SinCosTanExpr.TrigonometryFunctionType.cos;
		case tan: return SinCosTanExpr.TrigonometryFunctionType.tan;
		default: throw new RuntimeException("internal compiler error");
		}
	}

	@Override
	public TypeNode getType()
	{
		return argumentExpr.getType();
	}
}
