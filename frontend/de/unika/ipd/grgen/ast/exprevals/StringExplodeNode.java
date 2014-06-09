/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
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
import de.unika.ipd.grgen.ast.containers.ArrayTypeNode;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.StringExplode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class StringExplodeNode extends ExprNode {
	static {
		setName(StringExplodeNode.class, "string explode");
	}

	private ExprNode stringExpr;
	private ExprNode stringToSplitAtExpr;


	public StringExplodeNode(Coords coords, ExprNode stringExpr,
			ExprNode stringToSplitAtExpr) {
		super(coords);

		this.stringExpr            = becomeParent(stringExpr);
		this.stringToSplitAtExpr = becomeParent(stringToSplitAtExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(stringExpr);
		children.add(stringToSplitAtExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("string");
		childrenNames.add("stringToSplitAt");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		if(!stringExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringExpr.reportError("This argument to string explode expression must be of type string");
			return false;
		}
		if(!stringToSplitAtExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringToSplitAtExpr.reportError("Argument (string to "
					+ "split at) to string explode expression must be of type string");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new StringExplode(stringExpr.checkIR(Expression.class),
				stringToSplitAtExpr.checkIR(Expression.class),
				getType().getType());
	}

	@Override
	public TypeNode getType() {
		return ArrayTypeNode.getArrayType(((StringTypeNode)stringExpr.getType()).getIdentNode());
	}
}
