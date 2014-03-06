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
import de.unika.ipd.grgen.ir.exprevals.NodesExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the nodes of a node type.
 */
public class NodesExprNode extends ExprNode {
	static {
		setName(NodesExprNode.class, "nodes expr");
	}

	private ExprNode nodeType;
	private IdentNode resultNodeType;
		
	public NodesExprNode(Coords coords, ExprNode nodeType, IdentNode resultNodeType) {
		super(coords);
		this.nodeType = nodeType;
		becomeParent(this.nodeType);
		this.resultNodeType = resultNodeType;
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

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		return getType().resolve();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if(!(nodeType.getType() instanceof NodeTypeNode)) {
			reportError("argument of nodes(.) must be a node type");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new NodesExpr(nodeType.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType() {
		return SetTypeNode.getSetType(resultNodeType);
	}
}
