/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.StringLastIndexOf;
import de.unika.ipd.grgen.parser.Coords;

public class StringLastIndexOfNode extends ExprNode {
	static {
		setName(StringLastIndexOfNode.class, "string lastIndexOf");
	}

	private ExprNode stringExpr;
	private ExprNode stringToSearchForExpr;
	private ExprNode startIndexExpr;


	public StringLastIndexOfNode(Coords coords, ExprNode stringExpr,
			ExprNode stringToSearchForExpr) {
		super(coords);

		this.stringExpr            = becomeParent(stringExpr);
		this.stringToSearchForExpr = becomeParent(stringToSearchForExpr);
	}

	public StringLastIndexOfNode(Coords coords, ExprNode stringExpr,
			ExprNode stringToSearchForExpr, ExprNode startIndexExpr) {
		super(coords);

		this.stringExpr            = becomeParent(stringExpr);
		this.stringToSearchForExpr = becomeParent(stringToSearchForExpr);
		this.startIndexExpr        = becomeParent(startIndexExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(stringExpr);
		children.add(stringToSearchForExpr);
		if(startIndexExpr!=null)
			children.add(startIndexExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("string");
		childrenNames.add("stringToSearchFor");
		if(startIndexExpr!=null)
			childrenNames.add("startIndex");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		if(!stringExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringExpr.reportError("This argument to string lastIndexOf expression must be of type string");
			return false;
		}
		if(!stringToSearchForExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringToSearchForExpr.reportError("Argument (string to "
					+ "search for) to string lastIndexOf expression must be of type string");
			return false;
		}
		if(startIndexExpr!=null
				&& !startIndexExpr.getType().isEqual(BasicTypeNode.intType)) {
				startIndexExpr.reportError("Argument (start index) to string lastIndexOf expression must be of type int");
				return false;
			}
		return true;
	}

	@Override
	protected IR constructIR() {
		if(startIndexExpr!=null)
			return new StringLastIndexOf(stringExpr.checkIR(Expression.class),
					stringToSearchForExpr.checkIR(Expression.class),
					startIndexExpr.checkIR(Expression.class));
		else
			return new StringLastIndexOf(stringExpr.checkIR(Expression.class),
					stringToSearchForExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.intType;
	}
}
