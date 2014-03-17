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
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.EdgeByNameExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node retrieving an edge from a name.
 */
public class EdgeByNameExprNode extends ExprNode {
	static {
		setName(EdgeByNameExprNode.class, "edge by name expr");
	}

	private ExprNode name;
	
	private IdentNode edgeTypeUnresolved;
	private EdgeTypeNode edgeType;
	
	public EdgeByNameExprNode(Coords coords, ExprNode name, IdentNode edgeType) {
		super(coords);
		this.name = name;
		becomeParent(this.name);
		this.edgeTypeUnresolved = edgeType;
		becomeParent(this.edgeTypeUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(name);
		children.add(getValidVersion(edgeTypeUnresolved, edgeType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("name");
		childrenNames.add("edgeType");
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
		if(!(name.getType() instanceof StringTypeNode)) {
			reportError("argument of edgeByName(.) must be of type string");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new EdgeByNameExpr(name.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType() {
		return edgeType;
	}
}
