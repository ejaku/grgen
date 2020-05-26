/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.parser.Coords;

public abstract class ArrayAccumulationMethodNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayAccumulationMethodNode.class, "array accumulation method");
	}

	protected ArrayAccumulationMethodNode(Coords coords, ExprNode targetExpr)
	{
		super(coords, targetExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		return childrenNames;
	}

	// returns whether an array of the given type can be accumulated by this accumulation method
	public abstract boolean isValidTargetTypeOfAccumulation(TypeNode type);

	// returns the types allowed as target types of this accumulation method
	public abstract String getValidTargetTypesOfAccumulation();

	// returns DUMMY object only to be used for checking with isValidTargetTypeOfAccumulation
	public static ArrayAccumulationMethodNode getArrayMethodNode(String method)
	{
		switch(method) {
		case "sum":
			return new ArraySumNode(null, null);
		case "prod":
			return new ArrayProdNode(null, null);
		case "min":
			return new ArrayMinNode(null, null);
		case "max":
			return new ArrayMaxNode(null, null);
		case "avg":
			return new ArrayAvgNode(null, null);
		case "med":
			return new ArrayMedNode(null, null);
		case "medUnordered":
			return new ArrayMedUnorderedNode(null, null);
		case "var":
			return new ArrayVarNode(null, null);
		case "dev":
			return new ArrayDevNode(null, null);
		default:
			return null;
		}
	}
}
