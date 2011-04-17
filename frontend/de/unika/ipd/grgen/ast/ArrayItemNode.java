/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.ArrayItem;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayItemNode extends BaseNode {
	static {
		setName(ArrayItemNode.class, "array item");
	}

	protected ExprNode valueExpr;

	public ArrayItemNode(Coords coords, ExprNode valueExpr) {
		super(coords);
		this.valueExpr = becomeParent(valueExpr);
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
		// All checks are done in SetInitNode
		return true;
	}

	@Override
	protected IR constructIR() {
		return new ArrayItem(valueExpr.checkIR(Expression.class));
	}

	protected ArrayItem getArrayItem() {
		return checkIR(ArrayItem.class);
	}
}
