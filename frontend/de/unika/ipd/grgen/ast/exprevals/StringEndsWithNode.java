/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.exprevals.StringEndsWith;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class StringEndsWithNode extends ExprNode {
	static {
		setName(StringEndsWithNode.class, "string endsWith");
	}

	private ExprNode stringExpr;
	private ExprNode stringToSearchForExpr;


	public StringEndsWithNode(Coords coords, ExprNode stringExpr,
			ExprNode stringToSearchForExpr) {
		super(coords);

		this.stringExpr            = becomeParent(stringExpr);
		this.stringToSearchForExpr = becomeParent(stringToSearchForExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(stringExpr);
		children.add(stringToSearchForExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("string");
		childrenNames.add("stringToSearchFor");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		if(!stringExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringExpr.reportError("This argument to string endsWith expression must be of type string");
			return false;
		}
		if(!stringToSearchForExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringToSearchForExpr.reportError("Argument (string to "
					+ "search for) to string endsWith expression must be of type string");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new StringEndsWith(stringExpr.checkIR(Expression.class),
				stringToSearchForExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.booleanType;
	}
}
