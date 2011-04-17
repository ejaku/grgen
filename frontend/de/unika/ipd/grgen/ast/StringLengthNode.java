/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.StringLength;
import de.unika.ipd.grgen.parser.Coords;

public class StringLengthNode extends ExprNode {
	static {
		setName(StringLengthNode.class, "string length");
	}

	private ExprNode stringExpr;


	public StringLengthNode(Coords coords, ExprNode stringExpr) {
		super(coords);

		this.stringExpr = becomeParent(stringExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(stringExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("string");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		if(!stringExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringExpr.reportError("This argument to string length expression must be of type string");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new StringLength(stringExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.intType;
	}
}
