/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.MaxExpr;
import de.unika.ipd.grgen.parser.Coords;

public class MaxExprNode extends ExprNode {
	static {
		setName(MaxExprNode.class, "max expr");
	}

	private ExprNode leftExpr;
	private ExprNode rightExpr;


	public MaxExprNode(Coords coords, ExprNode leftExpr, ExprNode rightExpr) {
		super(coords);

		this.leftExpr = becomeParent(leftExpr);
		this.rightExpr = becomeParent(rightExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(leftExpr);
		children.add(rightExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("left");
		childrenNames.add("right");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		if(leftExpr.getType().isEqual(BasicTypeNode.byteType)
				&& rightExpr.getType().isEqual(BasicTypeNode.byteType)) {
			return true;
		}
		if(leftExpr.getType().isEqual(BasicTypeNode.shortType)
				&& rightExpr.getType().isEqual(BasicTypeNode.shortType)) {
			return true;
		}
		if(leftExpr.getType().isEqual(BasicTypeNode.intType)
				&& rightExpr.getType().isEqual(BasicTypeNode.intType)) {
			return true;
		}
		if(leftExpr.getType().isEqual(BasicTypeNode.longType)
				&& rightExpr.getType().isEqual(BasicTypeNode.longType)) {
			return true;
		}
		if(leftExpr.getType().isEqual(BasicTypeNode.floatType)
				&& rightExpr.getType().isEqual(BasicTypeNode.floatType)) {
			return true;
		}
		if(leftExpr.getType().isEqual(BasicTypeNode.doubleType)
				&& rightExpr.getType().isEqual(BasicTypeNode.doubleType)) {
			return true;
		}
		reportError("valid types for max(.,.) are: (byte,byte),(short,short),(int,int),(long,long),(float,float),(double,double)");
		return false;
	}

	@Override
	protected IR constructIR() {
		return new MaxExpr(leftExpr.checkIR(Expression.class),
				rightExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType() {
		return leftExpr.getType();
	}
}
