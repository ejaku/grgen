/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.exprevals.NodeByUniqueExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node retrieving a node from a unique id.
 */
public class NodeByUniqueExprNode extends ExprNode {
	static {
		setName(NodeByUniqueExprNode.class, "node by unique expr");
	}

	private ExprNode unique;
	
	private IdentNode nodeTypeUnresolved;
	private NodeTypeNode nodeType;
	
	public NodeByUniqueExprNode(Coords coords, ExprNode unique, IdentNode nodeType) {
		super(coords);
		this.unique = unique;
		becomeParent(this.unique);
		this.nodeTypeUnresolved = nodeType;
		becomeParent(this.nodeTypeUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(unique);
		children.add(getValidVersion(nodeTypeUnresolved, nodeType));
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
		if(!(unique.getType() instanceof IntTypeNode)) {
			reportError("argument of nodeByUnique(.) must be of type int");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new NodeByUniqueExpr(unique.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType() {
		return nodeType;
	}
}
