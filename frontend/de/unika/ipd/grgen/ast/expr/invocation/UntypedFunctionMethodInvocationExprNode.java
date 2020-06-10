/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.invocation;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.UntypedFunctionMethodInvocationExpr;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * Invocation of a function method on an untyped target - result untyped
 */
public class UntypedFunctionMethodInvocationExprNode extends FunctionInvocationBaseNode
{
	static {
		setName(UntypedFunctionMethodInvocationExprNode.class, "untyped function method invocation expression");
	}

	public UntypedFunctionMethodInvocationExprNode(Coords coords, CollectNode<ExprNode> arguments)
	{
		super(coords, arguments);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("arguments");
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
		return true;
	}

	@Override
	public TypeNode getType()
	{
		assert isResolved();
		return BasicTypeNode.untypedType;
	}

	@Override
	protected IR constructIR()
	{
		UntypedFunctionMethodInvocationExpr ufmi = new UntypedFunctionMethodInvocationExpr(
				BasicTypeNode.untypedType.checkIR(Type.class));
		for(ExprNode expr : arguments.getChildren()) {
			expr = expr.evaluate();
			ufmi.addArgument(expr.checkIR(Expression.class));
		}
		return ufmi;
	}
}
