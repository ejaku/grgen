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
import de.unika.ipd.grgen.ast.containers.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.InsertInducedSubgraphProc;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding an inserted node of the insertion of an induced subgraph of a node set.
 */
public class InsertInducedSubgraphProcNode extends ProcedureInvocationBaseNode {
	static {
		setName(InsertInducedSubgraphProcNode.class, "insert induced subgraph procedure");
	}

	private ExprNode nodeSetExpr;
	private ExprNode nodeExpr;
	
	Vector<TypeNode> returnTypes;
		
	public InsertInducedSubgraphProcNode(Coords coords, ExprNode nodeSetExpr, ExprNode nodeExpr) {
		super(coords);
		this.nodeSetExpr = nodeSetExpr;
		becomeParent(this.nodeSetExpr);
		this.nodeExpr = nodeExpr;
		becomeParent(this.nodeExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(nodeSetExpr);
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
		if(!(nodeSetExpr.getType() instanceof SetTypeNode)) {
			nodeSetExpr.reportError("set expected as 1st argument to insertInducedSubgraph");
			return false;
		}
		SetTypeNode type = (SetTypeNode)nodeSetExpr.getType();
		if(!(type.valueType instanceof NodeTypeNode)) {
			nodeSetExpr.reportError("set of nodes expected as 1st argument to insertInducedSubgraph");
			return false;
		}
		if(!(nodeExpr.getType() instanceof NodeTypeNode)) {
			nodeExpr.reportError("node expected as 2nd argument to insertInducedSubgraph");
			return false;
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		InsertInducedSubgraphProc insertInduced = new InsertInducedSubgraphProc(
														nodeSetExpr.checkIR(Expression.class), 
														nodeExpr.checkIR(Expression.class));
		for(TypeNode type : getType()) {
			insertInduced.addReturnType(type.getType());
		}
		return insertInduced;
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
