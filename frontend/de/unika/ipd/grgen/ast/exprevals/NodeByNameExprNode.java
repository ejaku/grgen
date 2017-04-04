/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.NodeByNameExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node retrieving a node from a name.
 */
public class NodeByNameExprNode extends ExprNode {
	static {
		setName(NodeByNameExprNode.class, "node by name expr");
	}

	private ExprNode name;
	private ExprNode nodeType;
	
	public NodeByNameExprNode(Coords coords, ExprNode name, ExprNode nodeType) {
		super(coords);
		this.name = name;
		becomeParent(this.name);
		this.nodeType = nodeType;
		becomeParent(this.nodeType);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(name);
		children.add(nodeType);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("name");
		childrenNames.add("nodeType");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if(!(name.getType() instanceof StringTypeNode)) {
			reportError("first argument of nodeByName(.,.) must be of type string");
			return false;
		}
		if(!(nodeType.getType() instanceof NodeTypeNode)) {
			reportError("second argument of nodeByName(.,.) must be a node type");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new NodeByNameExpr(name.checkIR(Expression.class), nodeType.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType() {
		return nodeType.getType();
	}
}
