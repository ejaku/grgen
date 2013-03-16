/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.OppositeExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the opposite node of an edge and a node.
 */
public class OppositeExprNode extends ExprNode {
	static {
		setName(OppositeExprNode.class, "opposite expr");
	}

	private ExprNode edge;
	private ExprNode node;
	
	private IdentNode nodeTypeUnresolved;
	private NodeTypeNode nodeType;
	
	public OppositeExprNode(Coords coords, ExprNode edge, ExprNode node, IdentNode nodeType) {
		super(coords);
		this.edge = edge;
		becomeParent(this.edge);
		this.node = node;
		becomeParent(this.node);
		this.nodeTypeUnresolved = nodeType;
		becomeParent(this.nodeTypeUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(edge);
		children.add(node);
		children.add(getValidVersion(nodeTypeUnresolved, nodeType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("edge");
		childrenNames.add("node");
		childrenNames.add("nodeType");
		return childrenNames;
	}

	private static final DeclarationTypeResolver<NodeTypeNode> nodeTypeResolver =
		new DeclarationTypeResolver<NodeTypeNode>(NodeTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		nodeType = nodeTypeResolver.resolve(nodeTypeUnresolved, this);
		return nodeType!=null && getType().resolve();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if(!(edge.getType() instanceof EdgeTypeNode)) {
			reportError("first argument of opposite(.,.) must be an edge type");
			return false;
		}
		if(!(node.getType() instanceof NodeTypeNode)) {
			reportError("second argument of opposite(.,.) must be a node type");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new OppositeExpr(edge.checkIR(Expression.class), node.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType() {
		return nodeType;
	}
}
