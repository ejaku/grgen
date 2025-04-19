/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.expr.string.StringReplace;
import de.unika.ipd.grgen.parser.Coords;

public class StringReplaceNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(StringReplaceNode.class, "string replace");
	}

	private ExprNode stringExpr;
	private ExprNode startExpr;
	private ExprNode lengthExpr;
	private ExprNode replaceStrExpr;

	public StringReplaceNode(Coords coords, ExprNode stringExpr,
			ExprNode startExpr, ExprNode lengthExpr, ExprNode replaceStrExpr)
	{
		super(coords);

		this.stringExpr = becomeParent(stringExpr);
		this.startExpr = becomeParent(startExpr);
		this.lengthExpr = becomeParent(lengthExpr);
		this.replaceStrExpr = becomeParent(replaceStrExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(stringExpr);
		children.add(startExpr);
		children.add(lengthExpr);
		children.add(replaceStrExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("string");
		childrenNames.add("start");
		childrenNames.add("length");
		childrenNames.add("replaceStrExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(!stringExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringExpr.reportError("The string function method replace can only be employed on an object of type string"
					+ " (but is employed on an object of type " + stringExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!startExpr.getType().isEqual(BasicTypeNode.intType)) {
			startExpr.reportError("The string function method replace expects as 1. argument (startPosition) a value of type int"
					+ " (but is given a value of type " + startExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!lengthExpr.getType().isEqual(BasicTypeNode.intType)) {
			lengthExpr.reportError("The string function method replace expects as 2. argument (length) a value of type int"
					+ " (but is given a value of type " + lengthExpr.getType().getTypeName() + ").");
			return false;
		}
		if(!replaceStrExpr.getType().isEqual(BasicTypeNode.stringType)) {
			replaceStrExpr.reportError("The string function method replace expects as 3. argument (replacementString) a value of type string"
					+ " (but is given a value of type " + replaceStrExpr.getType().getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		stringExpr = stringExpr.evaluate();
		startExpr = startExpr.evaluate();
		lengthExpr = lengthExpr.evaluate();
		replaceStrExpr = replaceStrExpr.evaluate();
		return new StringReplace(stringExpr.checkIR(Expression.class),
				startExpr.checkIR(Expression.class),
				lengthExpr.checkIR(Expression.class),
				replaceStrExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.stringType;
	}
}
