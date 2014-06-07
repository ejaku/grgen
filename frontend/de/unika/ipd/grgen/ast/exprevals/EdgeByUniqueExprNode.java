/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
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
import de.unika.ipd.grgen.ir.exprevals.EdgeByUniqueExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node retrieving an edge from a unique id.
 */
public class EdgeByUniqueExprNode extends ExprNode {
	static {
		setName(EdgeByUniqueExprNode.class, "edge by unique expr");
	}

	private ExprNode unique;
	
	private IdentNode edgeTypeUnresolved;
	private EdgeTypeNode edgeType;
	
	public EdgeByUniqueExprNode(Coords coords, ExprNode unique, IdentNode edgeType) {
		super(coords);
		this.unique = unique;
		becomeParent(this.unique);
		this.edgeTypeUnresolved = edgeType;
		becomeParent(this.edgeTypeUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(unique);
		children.add(getValidVersion(edgeTypeUnresolved, edgeType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("unique");
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
		if(!(unique.getType() instanceof IntTypeNode)) {
			reportError("argument of edgeByUnique(.) must be of type int");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new EdgeByUniqueExpr(unique.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType() {
		return edgeType;
	}
}
