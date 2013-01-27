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
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.EdgesExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the edges of an edge type.
 */
public class EdgesExprNode extends ExprNode {
	static {
		setName(EdgesExprNode.class, "edges expr");
	}

	private IdentNode edgeTypeUnresolved;

	private EdgeTypeNode edgeType;
	
	public EdgesExprNode(Coords coords, IdentNode edgeType) {
		super(coords);
		this.edgeTypeUnresolved = edgeType;
		becomeParent(this.edgeTypeUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(edgeTypeUnresolved, edgeType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("edge type");
		return childrenNames;
	}

	private static final DeclarationTypeResolver<EdgeTypeNode> edgeTypeResolver =
		new DeclarationTypeResolver<EdgeTypeNode>(EdgeTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		edgeType = edgeTypeResolver.resolve(edgeTypeUnresolved, this);
		return edgeType!=null && getType().resolve();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		return new EdgesExpr(edgeType.checkIR(EdgeType.class), getType().getType());
	}

	@Override
	public TypeNode getType() {
		return SetTypeNode.getSetType(edgeTypeUnresolved);
	}	
}
