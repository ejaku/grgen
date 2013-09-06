/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.exprevals.LogExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class LogExprNode extends ExprNode {
	static {
		setName(LogExprNode.class, "log expr");
	}

	private ExprNode leftExpr;
	private ExprNode rightExpr;


	public LogExprNode(Coords coords, ExprNode leftExpr, ExprNode rightExpr) {
		super(coords);

		this.leftExpr = becomeParent(leftExpr);
		this.rightExpr = becomeParent(rightExpr);
	}

	public LogExprNode(Coords coords, ExprNode leftExpr) {
		super(coords);

		this.leftExpr = becomeParent(leftExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(leftExpr);
		if(rightExpr!=null) children.add(rightExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("left");
		if(rightExpr!=null) childrenNames.add("right");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		if(rightExpr!=null) {
			if(leftExpr.getType().isEqual(BasicTypeNode.doubleType)
					&& rightExpr.getType().isEqual(BasicTypeNode.doubleType)) {
				return true;
			}
			reportError("valid types for log(.,.) are: (double,double)");
			return false;
		} else {
			if(leftExpr.getType().isEqual(BasicTypeNode.doubleType)) {
				return true;
			}
			reportError("valid types for log(.) are: double");
			return false;
		}
	}

	@Override
	protected IR constructIR() {
		if(rightExpr!=null) 
			return new LogExpr(leftExpr.checkIR(Expression.class),
					rightExpr.checkIR(Expression.class));
		else
			return new LogExpr(leftExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.doubleType;
	}
}
