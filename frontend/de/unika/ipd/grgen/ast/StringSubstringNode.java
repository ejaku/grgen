/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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
import de.unika.ipd.grgen.ir.StringSubstring;
import de.unika.ipd.grgen.parser.Coords;

public class StringSubstringNode extends ExprNode {
	static {
		setName(StringSubstringNode.class, "string substring");
	}
	
	ExprNode stringExpr, startExpr, lengthExpr;
	

	public StringSubstringNode(Coords coords, ExprNode stringExpr,
			ExprNode startExpr, ExprNode lengthExpr) {
		super(coords);
		
		this.stringExpr = becomeParent(stringExpr);
		this.startExpr  = becomeParent(startExpr);
		this.lengthExpr = becomeParent(lengthExpr);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(stringExpr);
		children.add(startExpr);
		children.add(lengthExpr);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("string");
		childrenNames.add("start");
		childrenNames.add("length");
		return childrenNames;
	}

	protected boolean checkLocal() {
		if(!stringExpr.getType().isEqual(BasicTypeNode.stringType)) {
			stringExpr.reportError("First argument to substring expression must be of type string");
			return false;
		}
		if(!startExpr.getType().isEqual(BasicTypeNode.intType)) {
			startExpr.reportError("Second argument (start position) to "
					+ "substring expression must be of type int");
			return false;
		}
		if(!lengthExpr.getType().isEqual(BasicTypeNode.intType)) {
			lengthExpr.reportError("Third argument (length) to substring "
					+ "expression must be of type int");
			return false;
		}
		return true;
	}
	
	protected IR constructIR() {
		return new StringSubstring(stringExpr.checkIR(Expression.class),
				startExpr.checkIR(Expression.class),
				lengthExpr.checkIR(Expression.class));
	}
	
	public TypeNode getType() {
		return BasicTypeNode.stringType;
	}
}
