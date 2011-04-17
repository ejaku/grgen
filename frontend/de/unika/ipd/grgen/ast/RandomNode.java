/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @version $Id: RandomNode.java 26740 2010-01-02 11:21:07Z eja $
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.RandomExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class RandomNode extends ExprNode {
	static {
		setName(RandomNode.class, "random");
	}

	private ExprNode numExpr;

	public RandomNode(Coords coords, ExprNode numExpr) {
		super(coords);

		this.numExpr = numExpr;
		becomeParent(numExpr);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		if(numExpr!=null) children.add(numExpr);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		if(numExpr!=null) childrenNames.add("maximum random number");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		if(numExpr!=null && !numExpr.getType().isEqual(BasicTypeNode.intType)) {
			numExpr.reportError("maximum random number must be of type int");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new RandomExpr(numExpr!=null ? numExpr.checkIR(Expression.class) : null);
	}

	@Override
	public TypeNode getType() {
		// if a parameter was given random returns an random integer number from 0 up to excluding numExpr,
		// otherwise a random double in the range [0,1] is returned
		return numExpr!=null ? BasicTypeNode.intType : BasicTypeNode.doubleType;
	}
}
