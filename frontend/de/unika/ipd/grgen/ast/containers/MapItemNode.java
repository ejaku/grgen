/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.MapItem;
import de.unika.ipd.grgen.parser.Coords;

public class MapItemNode extends BaseNode {
	static {
		setName(MapInitNode.class, "map item");
	}

	public ExprNode keyExpr;
	public ExprNode valueExpr;

	public MapItemNode(Coords coords, ExprNode keyExpr, ExprNode valueExpr) {
		super(coords);
		this.keyExpr   = becomeParent(keyExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	public void switchParenthoodOfItem(BaseNode throwOut, BaseNode adopt) {
		switchParenthood(throwOut, adopt);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(keyExpr);
		children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("keyExpr");
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		// All checks are done in MapInitNode
		return true;
	}

	@Override
	protected IR constructIR() {
		return new MapItem(keyExpr.checkIR(Expression.class), valueExpr.checkIR(Expression.class));
	}

	protected MapItem getMapItem() {
		return checkIR(MapItem.class);
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
