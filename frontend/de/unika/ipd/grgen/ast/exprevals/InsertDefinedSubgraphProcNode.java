/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
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
import de.unika.ipd.grgen.ir.exprevals.InsertDefinedSubgraphProc;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding an inserted edge of the insertion of a defined subgraph of an edge set.
 */
public class InsertDefinedSubgraphProcNode extends ProcedureInvocationBaseNode {
	static {
		setName(InsertDefinedSubgraphProcNode.class, "insert defined subgraph procedure");
	}

	private ExprNode edgeSetExpr;
	private ExprNode edgeExpr;
		
	Vector<TypeNode> returnTypes;

	public InsertDefinedSubgraphProcNode(Coords coords, ExprNode edgeSetExpr, ExprNode edgeExpr) {
		super(coords);
		this.edgeSetExpr = edgeSetExpr;
		becomeParent(this.edgeSetExpr);
		this.edgeExpr = edgeExpr;
		becomeParent(this.edgeExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(edgeSetExpr);
		children.add(edgeExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("edgeSetExpr");
		childrenNames.add("edgeExpr");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if(!(edgeSetExpr.getType() instanceof SetTypeNode)) {
			edgeSetExpr.reportError("set expected as 1st argument to insertDefinedSubgraph");
			return false;
		}
		SetTypeNode type = (SetTypeNode)edgeSetExpr.getType();
		if(!(type.valueType instanceof EdgeTypeNode)) {
			edgeSetExpr.reportError("set of edges expected as 1st argument to insertDefinedSubgraph");
			return false;
		}
		if(!(edgeExpr.getType() instanceof EdgeTypeNode)) {
			edgeExpr.reportError("edge expected as 2nd argument to insertDefinedSubgraph");
			return false;
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		InsertDefinedSubgraphProc insertDefined = new InsertDefinedSubgraphProc(
														edgeSetExpr.checkIR(Expression.class), 
														edgeExpr.checkIR(Expression.class));
		for(TypeNode type : getType()) {
			insertDefined.addReturnType(type.getType());
		}
		return insertDefined;
	}

	@Override
	public Vector<TypeNode> getType() {
		if(returnTypes==null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(edgeExpr.getType());
		}
		return returnTypes;
	}
}
