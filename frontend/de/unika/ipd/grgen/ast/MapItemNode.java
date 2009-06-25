/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id$
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MapItem;
import de.unika.ipd.grgen.parser.Coords;

public class MapItemNode extends BaseNode {
	static {
		setName(MapInitNode.class, "map item");
	}

	ExprNode keyExpr;
	ExprNode valueExpr;

	public MapItemNode(Coords coords, ExprNode keyExpr, ExprNode valueExpr) {
		super(coords);
		this.keyExpr   = becomeParent(keyExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(keyExpr);
		children.add(valueExpr);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("keyExpr");
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	protected boolean resolveLocal() {
		return true;
	}

	protected boolean checkLocal() {
		// All checks are done in MapInitNode
		return true;
	}

	protected IR constructIR() {
		return new MapItem(keyExpr.checkIR(Expression.class), valueExpr.checkIR(Expression.class));
	}

	public MapItem getMapItem() {
		return checkIR(MapItem.class);
	}
}
