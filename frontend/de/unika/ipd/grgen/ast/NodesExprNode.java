/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.containers.*;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NodesExpr;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the nodes of a node type.
 */
public class NodesExprNode extends ExprNode {
	static {
		setName(NodesExprNode.class, "nodes expr");
	}

	private IdentNode nodeTypeUnresolved;

	private NodeTypeNode nodeType;
		
	public NodesExprNode(Coords coords, IdentNode nodeType) {
		super(coords);
		this.nodeTypeUnresolved = nodeType;
		becomeParent(this.nodeTypeUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(nodeTypeUnresolved, nodeType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("node type");
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
		return true;
	}

	@Override
	protected IR constructIR() {
		return new NodesExpr(nodeType.checkIR(NodeType.class), getType().getType());
	}

	@Override
	public TypeNode getType() {
		return SetTypeNode.getSetType(nodeTypeUnresolved);
	}
}
