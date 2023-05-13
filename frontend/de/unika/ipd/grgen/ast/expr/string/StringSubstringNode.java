/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.expr.string.StringSubstring;
import de.unika.ipd.grgen.parser.Coords;

public class StringSubstringNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(StringSubstringNode.class, "string substring");
	}

	private ExprNode stringExpr;
	private ExprNode startExpr;
	private ExprNode lengthExpr;

	public StringSubstringNode(Coords coords, ExprNode stringExpr, ExprNode startExpr, ExprNode lengthExpr)
	{
		super(coords);

		this.stringExpr = becomeParent(stringExpr);
		this.startExpr = becomeParent(startExpr);
		this.lengthExpr = becomeParent(lengthExpr);
	}

	public StringSubstringNode(Coords coords, ExprNode stringExpr, ExprNode startExpr)
	{
		super(coords);

		this.stringExpr = becomeParent(stringExpr);
		this.startExpr = becomeParent(startExpr);
		this.lengthExpr = null;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(stringExpr);
		children.add(startExpr);
		if(lengthExpr != null)
			children.add(lengthExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("string");
		childrenNames.add("start");
		if(lengthExpr != null)
			childrenNames.add("length");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(!stringExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringExpr.reportError("The string function method substring can only be employed on an object of type string"
					+ " (but is employed on an object of type " + stringExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!startExpr.getType().isEqual(BasicTypeNode.intType)) {
			startExpr.reportError("The string function method substring expects as 1. argument (startPosition) a value of type int"
					+ " (but is given a value of type " + startExpr.getType().getTypeName() + ").");
			return false;
		}
		if(lengthExpr != null) {
			if(!lengthExpr.getType().isEqual(BasicTypeNode.intType)) {
				lengthExpr.reportError("The string function method substring expects as 2. argument (length) a value of type int"
						+ " (but is given a value of type " + lengthExpr.getType().getTypeName() + ").");
				return false;
			}
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		stringExpr = stringExpr.evaluate();
		startExpr = startExpr.evaluate();
		if(lengthExpr != null)
			lengthExpr = lengthExpr.evaluate();
		return new StringSubstring(stringExpr.checkIR(Expression.class),
				startExpr.checkIR(Expression.class),
				lengthExpr != null ? lengthExpr.checkIR(Expression.class) : null);
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.stringType;
	}
}
