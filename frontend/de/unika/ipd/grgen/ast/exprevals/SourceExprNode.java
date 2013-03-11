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
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.SourceExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the source node of an edge.
 */
public class SourceExprNode extends ExprNode {
	static {
		setName(SourceExprNode.class, "source expr");
	}

	private IdentNode edgeUnresolved;
	private IdentNode nodeTypeUnresolved;

	private EdgeDeclNode edgeDecl;
	private NodeTypeNode nodeType;
	
	public SourceExprNode(Coords coords, IdentNode edge, IdentNode nodeType) {
		super(coords);
		this.edgeUnresolved = edge;
		becomeParent(this.edgeUnresolved);
		this.nodeTypeUnresolved = nodeType;
		becomeParent(this.nodeTypeUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(edgeUnresolved, edgeDecl));
		children.add(getValidVersion(nodeTypeUnresolved, nodeType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("edge");
		childrenNames.add("nodeType");
		return childrenNames;
	}

	private static final DeclarationResolver<EdgeDeclNode> edgeResolver =
		new DeclarationResolver<EdgeDeclNode>(EdgeDeclNode.class);
	private static final DeclarationTypeResolver<NodeTypeNode> nodeTypeResolver =
		new DeclarationTypeResolver<NodeTypeNode>(NodeTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		edgeDecl = edgeResolver.resolve(edgeUnresolved, this);
		nodeType = nodeTypeResolver.resolve(nodeTypeUnresolved, this);
		return edgeDecl!=null && nodeType!=null && getType().resolve();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		return new SourceExpr(edgeDecl.checkIR(Edge.class), getType().getType());
	}

	@Override
	public TypeNode getType() {
		return nodeType;
	}
	
	public boolean noDefElementInCondition() {
		if(edgeDecl.defEntityToBeYieldedTo) {
			edgeDecl.reportError("A def entity ("+edgeDecl+") can't be accessed from an if");
			return false;
		}
		return true;
	}
}
