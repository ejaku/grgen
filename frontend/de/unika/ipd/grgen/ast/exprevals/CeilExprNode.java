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
import de.unika.ipd.grgen.ir.exprevals.CeilExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class CeilExprNode extends ExprNode {
	static {
		setName(CeilExprNode.class, "ceil expr");
	}

	private ExprNode argumentExpr;


	public CeilExprNode(Coords coords, ExprNode argumentExpr) {
		super(coords);

		this.argumentExpr = becomeParent(argumentExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(argumentExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("arg");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		if(argumentExpr.getType().isEqual(BasicTypeNode.doubleType)) {
			return true;
		}
		reportError("argument to ceil(.) must be of type double");
		return false;
	}

	@Override
	protected IR constructIR() {
		return new CeilExpr(argumentExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType() {
		return argumentExpr.getType();
	}
}
