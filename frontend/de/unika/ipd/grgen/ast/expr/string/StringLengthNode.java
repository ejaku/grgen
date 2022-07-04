/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.string;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.string.StringLength;
import de.unika.ipd.grgen.parser.Coords;

public class StringLengthNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(StringLengthNode.class, "string length");
	}

	private ExprNode stringExpr;

	public StringLengthNode(Coords coords, ExprNode stringExpr)
	{
		super(coords);

		this.stringExpr = becomeParent(stringExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(stringExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("string");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(!stringExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringExpr.reportError("The string function method length can only be employed on an object of type string"
					+ " (but is employed on an object of type " + stringExpr.getType() + ").");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		stringExpr = stringExpr.evaluate();
		return new StringLength(stringExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.intType;
	}
}
