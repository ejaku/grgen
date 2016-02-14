/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.AbsExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class AbsExprNode extends ExprNode {
	static {
		setName(AbsExprNode.class, "abs expr");
	}

	private ExprNode argumentExpr;


	public AbsExprNode(Coords coords, ExprNode argumentExpr) {
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
		if(argumentExpr.getType().isEqual(BasicTypeNode.byteType)
			|| argumentExpr.getType().isEqual(BasicTypeNode.shortType)
			|| argumentExpr.getType().isEqual(BasicTypeNode.intType)
			|| argumentExpr.getType().isEqual(BasicTypeNode.longType)
			|| argumentExpr.getType().isEqual(BasicTypeNode.floatType)
			|| argumentExpr.getType().isEqual(BasicTypeNode.doubleType)) {
			return true;
		}
		reportError("valid types for abs(.) are: byte,short,int,long,float,double");
		return false;
	}

	@Override
	protected IR constructIR() {
		return new AbsExpr(argumentExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType() {
		return argumentExpr.getType();
	}
}
