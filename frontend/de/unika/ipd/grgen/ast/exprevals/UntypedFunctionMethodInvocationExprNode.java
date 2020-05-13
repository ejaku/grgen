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
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.UntypedFunctionMethodInvocationExpr;

/**
 * Invocation of a function method on an untyped target - result untyped
 */
public class UntypedFunctionMethodInvocationExprNode extends FunctionMethodInvocationBaseNode
{
	static {
		setName(UntypedFunctionMethodInvocationExprNode.class, "untyped function method invocation expression");
	}

	public UntypedFunctionMethodInvocationExprNode(Coords coords, CollectNode<ExprNode> arguments)
	{
		super(coords);
		this.arguments = becomeParent(arguments);
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
			ufmi.addArgument(expr.checkIR(Expression.class));
		}
		return ufmi;
	}
}
