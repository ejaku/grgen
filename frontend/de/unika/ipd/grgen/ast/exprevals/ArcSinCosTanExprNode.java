/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
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
import de.unika.ipd.grgen.ir.exprevals.ArcSinCosTanExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArcSinCosTanExprNode extends ExprNode {
	static {
		setName(ArcSinCosTanExprNode.class, "arcsincostan expr");
	}

	int which;
	private ExprNode argumentExpr;
	
	public static final int ARC_SIN = 0;
	public static final int ARC_COS = 1;
	public static final int ARC_TAN = 2;


	public ArcSinCosTanExprNode(Coords coords, int which, ExprNode argumentExpr) {
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
		reportError("valid types for arcsin(.),arccos(.),arctan(.) are: (double)");
		return false;
	}

	@Override
	protected IR constructIR() {
		// assumes that the which:int of the AST node uses the same values as the which of the IR expression
		return new ArcSinCosTanExpr(which, argumentExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType() {
		return argumentExpr.getType();
	}
}
