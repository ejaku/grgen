/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

	// returns DUMMY object only to be used for checking with isValidTargetTypeOfAccumulation
	public static ArrayAccumulationMethodNode getArrayMethodNode(String method)
	{
		if(method.equals("sum"))
			return new ArraySumNode(null, null);
		if(method.equals("prod"))
			return new ArrayProdNode(null, null);
		if(method.equals("min"))
			return new ArrayMinNode(null, null);
		if(method.equals("max"))
			return new ArrayMaxNode(null, null);
		if(method.equals("avg"))
			return new ArrayAvgNode(null, null);
		if(method.equals("med"))
			return new ArrayMedNode(null, null);
		if(method.equals("medUnordered"))
			return new ArrayMedUnorderedNode(null, null);
		if(method.equals("var"))
			return new ArrayVarNode(null, null);
		if(method.equals("dev"))
			return new ArrayDevNode(null, null);
		return null;
	}
}
