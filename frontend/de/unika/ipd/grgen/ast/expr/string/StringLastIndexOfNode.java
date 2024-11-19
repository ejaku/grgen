/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.expr.string.StringLastIndexOf;
import de.unika.ipd.grgen.parser.Coords;

public class StringLastIndexOfNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(StringLastIndexOfNode.class, "string lastIndexOf");
	}

	private ExprNode stringExpr;
	private ExprNode stringToSearchForExpr;
	private ExprNode startIndexExpr;

	public StringLastIndexOfNode(Coords coords, ExprNode stringExpr, ExprNode stringToSearchForExpr)
	{
		super(coords);

		this.stringExpr = becomeParent(stringExpr);
		this.stringToSearchForExpr = becomeParent(stringToSearchForExpr);
	}

	public StringLastIndexOfNode(Coords coords, ExprNode stringExpr, ExprNode stringToSearchForExpr,
			ExprNode startIndexExpr)
	{
		super(coords);

		this.stringExpr = becomeParent(stringExpr);
		this.stringToSearchForExpr = becomeParent(stringToSearchForExpr);
		this.startIndexExpr = becomeParent(startIndexExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(stringExpr);
		children.add(stringToSearchForExpr);
		if(startIndexExpr != null)
			children.add(startIndexExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("string");
		childrenNames.add("stringToSearchFor");
		if(startIndexExpr != null)
			childrenNames.add("startIndex");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(!stringExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringExpr.reportError("The string function method lastIndexOf can only be employed on an object of type string"
					+ " (but is employed on an object of type " + stringExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!stringToSearchForExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringToSearchForExpr.reportError("The string function method lastIndexOf expects as 1. argument (stringToSearchFor) a value of type string"
					+ " (but is given a value of type " + stringToSearchForExpr.getType().getTypeName() + ").");
			return false;
		}
		if(startIndexExpr != null
				&& !startIndexExpr.getType().isEqual(BasicTypeNode.intType)) {
			startIndexExpr.reportError("The string function method lastIndexOf expects as 2. argument (startIndex) a value of type int"
					+ " (but is given a value of type " + startIndexExpr.getType().getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		stringExpr = stringExpr.evaluate();
		stringToSearchForExpr = stringToSearchForExpr.evaluate();
		if(startIndexExpr != null) {
			startIndexExpr = startIndexExpr.evaluate();
			return new StringLastIndexOf(stringExpr.checkIR(Expression.class),
					stringToSearchForExpr.checkIR(Expression.class),
					startIndexExpr.checkIR(Expression.class));
		} else {
			return new StringLastIndexOf(stringExpr.checkIR(Expression.class),
					stringToSearchForExpr.checkIR(Expression.class));
		}
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.intType;
	}
}
