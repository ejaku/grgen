/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ir.containers.ArrayOrderAscending;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayOrderAscendingNode extends ExprNode
{
	static {
		setName(ArrayOrderAscendingNode.class, "array ordere ascending");
	}

	private ExprNode targetExpr;

	public ArrayOrderAscendingNode(Coords coords, ExprNode targetExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof ArrayTypeNode)) {
			targetExpr.reportError("This argument to array orderAscending expression must be of type array<T>");
			return false;
		}
		ArrayTypeNode arrayType = (ArrayTypeNode)targetType;
		if(!(arrayType.valueType.equals(BasicTypeNode.byteType))
			&&!(arrayType.valueType.equals(BasicTypeNode.shortType))
			&& !(arrayType.valueType.equals(BasicTypeNode.intType))
			&& !(arrayType.valueType.equals(BasicTypeNode.longType))
			&& !(arrayType.valueType.equals(BasicTypeNode.floatType))
			&& !(arrayType.valueType.equals(BasicTypeNode.doubleType))
			&& !(arrayType.valueType.equals(BasicTypeNode.stringType))
			&& !(arrayType.valueType.equals(BasicTypeNode.booleanType))
			&& !(arrayType.valueType instanceof EnumTypeNode)) {
			targetExpr.reportError("array orderAscending only available for arrays of type byte,short,int,long,float,double,string,boolean,enum");
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return ((ArrayTypeNode)targetExpr.getType());
	}

	@Override
	protected IR constructIR() {
		return new ArrayOrderAscending(targetExpr.checkIR(Expression.class));
	}
}
