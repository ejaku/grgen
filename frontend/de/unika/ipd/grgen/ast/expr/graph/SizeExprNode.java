/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.graph.SizeExpr;
import de.unika.ipd.grgen.parser.Coords;

public class SizeExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(SizeExprNode.class, "size expr");
	}

	public SizeExprNode(Coords coords)
	{
		super(coords);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		return new SizeExpr();
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.intType;
	}
}
