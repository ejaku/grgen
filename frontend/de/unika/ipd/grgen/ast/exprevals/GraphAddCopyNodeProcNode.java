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
import de.unika.ipd.grgen.ir.exprevals.GraphAddCopyNodeProc;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for adding a copy of a node to graph.
 */
public class GraphAddCopyNodeProcNode extends ProcedureInvocationBaseNode {
	static {
		setName(GraphAddCopyNodeProcNode.class, "graph add copy node procedure");
	}

	private ExprNode oldNode;
	
	Vector<TypeNode> returnTypes;

	public GraphAddCopyNodeProcNode(Coords coords, ExprNode nodeType) {
		super(coords);
		this.oldNode = nodeType;
		becomeParent(this.oldNode);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(oldNode);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("old node");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if(!(oldNode.getType() instanceof NodeTypeNode)) {
			reportError("argument of addCopy(.) must be a node");
			return false;
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		GraphAddCopyNodeProc addCopyNode = new GraphAddCopyNodeProc(oldNode.checkIR(Expression.class));
		for(TypeNode type : getType()) {
			addCopyNode.addReturnType(type.getType());
		}
		return addCopyNode;
	}

	@Override
	public Vector<TypeNode> getType() {
		if(returnTypes==null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(oldNode.getType());
		}
		return returnTypes;
	}
}
