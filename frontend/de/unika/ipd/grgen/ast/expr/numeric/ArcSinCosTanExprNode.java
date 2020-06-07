/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.numeric;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.numeric.ArcSinCosTanExpr;
import de.unika.ipd.grgen.parser.Coords;

public class ArcSinCosTanExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(ArcSinCosTanExprNode.class, "arcsincostan expr");
	}

	public enum ArcusTrigonometryFunctionType
	{
		arcsin,
		arccos,
		arctan
	}

	ArcusTrigonometryFunctionType which;
	private ExprNode argumentExpr;
	
	public ArcSinCosTanExprNode(Coords coords, ArcusTrigonometryFunctionType which, ExprNode argumentExpr)
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
		return new ArcSinCosTanExpr(getArcusTrigonometryFunctionType(), argumentExpr.checkIR(Expression.class));
	}
	
	private ArcSinCosTanExpr.ArcusTrigonometryFunctionType getArcusTrigonometryFunctionType()
	{
		switch(which) {
		case arcsin: return ArcSinCosTanExpr.ArcusTrigonometryFunctionType.arcsin;
		case arccos: return ArcSinCosTanExpr.ArcusTrigonometryFunctionType.arccos;
		case arctan: return ArcSinCosTanExpr.ArcusTrigonometryFunctionType.arctan;
		default: throw new RuntimeException("internal compiler error");
		}
	}

	@Override
	public TypeNode getType()
	{
		return argumentExpr.getType();
	}
}
