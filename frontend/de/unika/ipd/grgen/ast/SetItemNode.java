/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: MapItemNode.java 22993 2008-10-18 14:12:51Z buchwald $
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.SetItem;
import de.unika.ipd.grgen.parser.Coords;

public class SetItemNode extends BaseNode {
	static {
		setName(SetItemNode.class, "set item");
	}

	ExprNode valueExpr;

	public SetItemNode(Coords coords, ExprNode valueExpr) {
		super(coords);
		this.valueExpr = becomeParent(valueExpr);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(valueExpr);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	protected boolean resolveLocal() {
		return true;
	}

	protected boolean checkLocal() {
		// All checks are done in SetInitNode
		return true;
	}

	protected IR constructIR() {
		return new SetItem(valueExpr.checkIR(Expression.class));
	}

	public SetItem getSetItem() {
		return checkIR(SetItem.class);
	}
}
