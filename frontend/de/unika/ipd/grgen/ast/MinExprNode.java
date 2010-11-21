/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MinExpr;
import de.unika.ipd.grgen.parser.Coords;

public class MinExprNode extends ExprNode {
	static {
		setName(MinExprNode.class, "min expr");
	}

	private ExprNode leftExpr;
	private ExprNode rightExpr;


	public MinExprNode(Coords coords, ExprNode leftExpr, ExprNode rightExpr) {
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
		if(leftExpr.getType().isEqual(BasicTypeNode.intType)
				&& rightExpr.getType().isEqual(BasicTypeNode.intType)) {
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
		reportError("valid types for min(.,.) are: (int,int),(float,float),(double,double)");
		return false;
	}

	@Override
	protected IR constructIR() {
		return new MinExpr(leftExpr.checkIR(Expression.class),
				rightExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType() {
		return leftExpr.getType();
	}
}
