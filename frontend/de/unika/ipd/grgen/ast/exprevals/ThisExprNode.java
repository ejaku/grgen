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
import de.unika.ipd.grgen.ir.exprevals.ThisExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ThisExprNode extends ExprNode {
	static {
		setName(ThisExprNode.class, "this");
	}

	public ThisExprNode(Coords coords) {
		super(coords);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		return new ThisExpr();
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.graphType;
	}
}
