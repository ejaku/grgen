/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.InsertCopyProc;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for inserting a copy of the subgraph to the given main graph.
 */
public class InsertCopyProcNode extends ProcedureInvocationBaseNode {
	static {
		setName(InsertCopyProcNode.class, "insert copy procedure");
	}

	private ExprNode graphExpr;
	private ExprNode nodeExpr;
	
	Vector<TypeNode> returnTypes;
		
	public InsertCopyProcNode(Coords coords, ExprNode nodeSetExpr, ExprNode nodeExpr) {
		super(coords);
		this.graphExpr = nodeSetExpr;
		becomeParent(this.graphExpr);
		this.nodeExpr = nodeExpr;
		becomeParent(this.nodeExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(graphExpr);
		children.add(nodeExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("nodeSetExpr");
		childrenNames.add("nodeExpr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if(!(graphExpr.getType().equals(BasicTypeNode.graphType))) {
			reportError("first argument of insertCopy(.,.) must be of graph type (the subgraph to insert into the current graph)");
			return false;
		}
		if(!(nodeExpr.getType() instanceof NodeTypeNode)) {
			nodeExpr.reportError("node expected as 2nd argument to insertCopy");
			return false;
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		InsertCopyProc insertCopy = new InsertCopyProc(graphExpr.checkIR(Expression.class), 
														nodeExpr.checkIR(Expression.class));
		for(TypeNode type : getType()) {
			insertCopy.addReturnType(type.getType());
		}
		return insertCopy;
	}

	@Override
	public Vector<TypeNode> getType() {
		if(returnTypes==null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(nodeExpr.getType());
		}
		return returnTypes;
	}
}
