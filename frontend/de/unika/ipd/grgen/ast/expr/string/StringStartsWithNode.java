/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
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
import de.unika.ipd.grgen.ir.expr.string.StringStartsWith;
import de.unika.ipd.grgen.parser.Coords;

public class StringStartsWithNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(StringStartsWithNode.class, "string startsWith");
	}

	private ExprNode stringExpr;
	private ExprNode stringToSearchForExpr;

	public StringStartsWithNode(Coords coords, ExprNode stringExpr, ExprNode stringToSearchForExpr)
	{
		super(coords);

		this.stringExpr = becomeParent(stringExpr);
		this.stringToSearchForExpr = becomeParent(stringToSearchForExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(stringExpr);
		children.add(stringToSearchForExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("string");
		childrenNames.add("stringToSearchFor");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(!stringExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringExpr.reportError("The string function method startsWith can only be employed on an object of type string"
					+ " (but is employed on an object of type " + stringExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!stringToSearchForExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringToSearchForExpr.reportError("The string function method startsWith expects as argument (stringToSearchFor) a value of type string"
					+ " (but is given a value of type " + stringToSearchForExpr.getType().getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		stringExpr = stringExpr.evaluate();
		stringToSearchForExpr = stringToSearchForExpr.evaluate();
		return new StringStartsWith(stringExpr.checkIR(Expression.class),
				stringToSearchForExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.booleanType;
	}
}
