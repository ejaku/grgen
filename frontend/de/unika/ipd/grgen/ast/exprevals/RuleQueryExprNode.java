/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.RuleQueryExpr;
import de.unika.ipd.grgen.parser.Coords;

public class RuleQueryExprNode extends ExprNode {
	static {
		setName(RuleQueryExprNode.class, "rule query");
	}

	private CallActionNode callAction;

	private TypeNode arrayOfMatchTypeUnresolved;
	private TypeNode arrayOfMatchType;

	public RuleQueryExprNode(Coords coords, CallActionNode callAction, TypeNode arrayOfMatchType) {
		super(coords);

		this.callAction = becomeParent(callAction);
		this.arrayOfMatchTypeUnresolved = becomeParent(arrayOfMatchType);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(callAction);
		children.add(getValidVersion(arrayOfMatchTypeUnresolved, arrayOfMatchType));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("callAction");
		childrenNames.add("arrayOfMatchType");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		if(arrayOfMatchTypeUnresolved.resolve()) {
			arrayOfMatchType = arrayOfMatchTypeUnresolved;
		}
		return arrayOfMatchType != null;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}
	
	public CallActionNode getCallAction() {
		return callAction;
	}

	@Override
	protected IR constructIR() {
		return new RuleQueryExpr(getType().getType());
	}

	@Override
	public TypeNode getType() {
		return arrayOfMatchType;
	}
}
