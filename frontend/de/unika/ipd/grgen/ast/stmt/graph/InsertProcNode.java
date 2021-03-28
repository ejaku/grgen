/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.stmt.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.graph.InsertProc;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for inserting the subgraph to the given main graph (destroying the original graph).
 */
public class InsertProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(InsertProcNode.class, "insert procedure");
	}

	private ExprNode graphExpr;

	public InsertProcNode(Coords coords, ExprNode graphExpr)
	{
		super(coords);
		this.graphExpr = graphExpr;
		becomeParent(this.graphExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(graphExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("graphExpr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(!(graphExpr.getType().equals(BasicTypeNode.graphType))) {
			reportError("argument of insert(.) must be of graph type (the subgraph to insert into the current graph)");
			return false;
		}
		return true;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		graphExpr = graphExpr.evaluate();
		InsertProc insert = new InsertProc(graphExpr.checkIR(Expression.class));
		return insert;
	}
}
