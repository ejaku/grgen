/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.GraphAddNodeProc;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for adding a node to graph.
 */
public class GraphAddNodeProcNode extends ProcedureInvocationBaseNode {
	static {
		setName(GraphAddNodeProcNode.class, "graph add node procedure");
	}

	private ExprNode nodeType;
	
	Vector<TypeNode> returnTypes;

	public GraphAddNodeProcNode(Coords coords, ExprNode nodeType) {
		super(coords);
		this.nodeType = nodeType;
		becomeParent(this.nodeType);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(nodeType);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("node type");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if(!(nodeType.getType() instanceof NodeTypeNode)) {
			reportError("argument of add(.) must be a node type");
			return false;
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		GraphAddNodeProc addNode = new GraphAddNodeProc(nodeType.checkIR(Expression.class));
		for(TypeNode type : getType()) {
			addNode.addReturnType(type.getType());
		}
		return addNode;
	}

	@Override
	public Vector<TypeNode> getType() {
		if(returnTypes==null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(nodeType.getType());
		}
		return returnTypes;
	}
}
