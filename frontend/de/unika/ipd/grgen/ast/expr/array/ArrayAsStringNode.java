/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.StringTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayAsString;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayAsStringNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayAsStringNode.class, "array asString");
	}

	private ExprNode valueExpr;

	public ArrayAsStringNode(Coords coords, ExprNode targetExpr, ExprNode valueExpr)
	{
		super(coords, targetExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		// target type already checked during resolving into this node
		targetExpr.getType().resolve(); // call to ensure the array type exists
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		ArrayTypeNode arrayMemberType = getTargetType();
		if(!(arrayMemberType.valueType instanceof StringTypeNode)) {
			targetExpr.reportError("The array function method asString can only be employed on an object of type array<string>"
					+ " (but is employed on an object of type " + arrayMemberType.getTypeName() + ").");
			return false;
		}
		TypeNode valueType = valueExpr.getType();
		if(!valueType.isEqual(BasicTypeNode.stringType)) {
			valueExpr.reportError("The array function method asString expects as argument a value of type string"
					+ " (but is given a value of type " + valueType.getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.stringType;
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		return new ArrayAsString(targetExpr.checkIR(Expression.class), valueExpr.checkIR(Expression.class));
	}
}
