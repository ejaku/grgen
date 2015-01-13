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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.Nameof;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the name of some node/edge or the graph.
 */
public class NameofNode extends ExprNode {
	static {
		setName(NameofNode.class, "nameof");
	}

	private ExprNode namedEntity; // null if name of main graph is requested

	public NameofNode(Coords coords, ExprNode namedEntity) {
		super(coords);
		this.namedEntity = namedEntity;
		becomeParent(this.namedEntity);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		if(namedEntity!=null)
			children.add(namedEntity);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		if(namedEntity!=null)
			childrenNames.add("named entity");
		return childrenNames;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal() {
		if(namedEntity != null) {
			if(namedEntity.getType().isEqual(BasicTypeNode.graphType)) {
				return true;
			} 
			if(namedEntity.getType() instanceof EdgeTypeNode) {
				return true;
			}
			if(namedEntity.getType() instanceof NodeTypeNode) {
				return true;
			}

			reportError("nameof(.) expects an entity of node or edge or subgraph type");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		if(namedEntity==null) {
			return new Nameof(null, getType().getType());
		}
		return new Nameof(namedEntity.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.stringType;
	}
	
	public boolean noDefElementInCondition() {
		if(namedEntity!=null)
			return namedEntity.noDefElementInCondition();
		else
			return true;
	}
}
