/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.exprevals.SinCosTanExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class SinCosTanExprNode extends ExprNode {
	static {
		setName(SinCosTanExprNode.class, "sincostan expr");
	}

	int which;
	private ExprNode argumentExpr;
	
	public static final int SIN = 0;
	public static final int COS = 1;
	public static final int TAN = 2;


	public SinCosTanExprNode(Coords coords, int which, ExprNode argumentExpr) {
		super(coords);

		this.which = which;
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
		reportError("valid types for sin(.),cos(.),tan(.) are: (double)");
		return false;
	}

	@Override
	protected IR constructIR() {
		// assumes that the which:int of the AST node uses the same values as the which of the IR expression
		return new SinCosTanExpr(which, argumentExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType() {
		return argumentExpr.getType();
	}
}
