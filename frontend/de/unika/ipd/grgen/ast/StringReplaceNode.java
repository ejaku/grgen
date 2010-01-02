/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
import de.unika.ipd.grgen.ir.StringReplace;
import de.unika.ipd.grgen.parser.Coords;

public class StringReplaceNode extends ExprNode {
	static {
		setName(StringReplaceNode.class, "string replace");
	}

	private ExprNode stringExpr;
	private ExprNode startExpr;
	private ExprNode lengthExpr;
	private ExprNode replaceStrExpr;


	public StringReplaceNode(Coords coords, ExprNode stringExpr,
			ExprNode startExpr, ExprNode lengthExpr, ExprNode replaceStrExpr) {
		super(coords);

		this.stringExpr     = becomeParent(stringExpr);
		this.startExpr      = becomeParent(startExpr);
		this.lengthExpr     = becomeParent(lengthExpr);
		this.replaceStrExpr = becomeParent(replaceStrExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(stringExpr);
		children.add(startExpr);
		children.add(lengthExpr);
		children.add(replaceStrExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("string");
		childrenNames.add("start");
		childrenNames.add("length");
		childrenNames.add("replaceStrExpr");
		return childrenNames;
	}

	@Override
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
		if(!replaceStrExpr.getType().isEqual(BasicTypeNode.stringType)) {
			replaceStrExpr.reportError("Fourth argument (replacement string) "
					+ "to substring expression must be of type string");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new StringReplace(stringExpr.checkIR(Expression.class),
				startExpr.checkIR(Expression.class),
				lengthExpr.checkIR(Expression.class),
				replaceStrExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.stringType;
	}
}
