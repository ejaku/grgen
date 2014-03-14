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
import de.unika.ipd.grgen.ir.exprevals.Uniqueof;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the name of some node/edge or the graph.
 */
public class UniqueofExprNode extends ExprNode {
	static {
		setName(UniqueofExprNode.class, "uniqueof");
	}

	private ExprNode entity;

	public UniqueofExprNode(Coords coords, ExprNode entity) {
		super(coords);
		this.entity = entity;
		becomeParent(this.entity);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(entity);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("entity");
		return childrenNames;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal() {
		if(entity != null) {
			if(entity.getType() instanceof EdgeTypeNode) {
				return true;
			}
			if(entity.getType() instanceof NodeTypeNode) {
				return true;
			}

			reportError("uniqueof(.) expects an entity of node or edge type");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new Uniqueof(entity.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.intType;
	}
	
	public boolean noDefElementInCondition() {
		return entity.noDefElementInCondition();
	}
}
