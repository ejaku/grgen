/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.DequeItem;
import de.unika.ipd.grgen.parser.Coords;

public class DequeItemNode extends BaseNode {
	static {
		setName(DequeItemNode.class, "deque item");
	}

	protected ExprNode valueExpr;

	public DequeItemNode(Coords coords, ExprNode valueExpr) {
		super(coords);
		this.valueExpr = becomeParent(valueExpr);
	}

	public void switchParenthoodOfItem(BaseNode throwOut, BaseNode adopt) {
		switchParenthood(throwOut, adopt);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		// All checks are done in DequeInitNode
		return true;
	}

	@Override
	protected IR constructIR() {
		return new DequeItem(valueExpr.checkIR(Expression.class));
	}

	protected DequeItem getDequeItem() {
		return checkIR(DequeItem.class);
	}
	
	public boolean noDefElementInCondition() {
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode)
				res &= ((ExprNode)child).noDefElementInCondition();
		}
		return res;
	}
}
