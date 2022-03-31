/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.invocation;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.invocation.MultiRuleQueryExpr;
import de.unika.ipd.grgen.parser.Coords;

public class MultiRuleQueryExprNode extends ExprNode
{
	static {
		setName(MultiRuleQueryExprNode.class, "multi rule query");
	}

	private CollectNode<ExprNode> ruleQueries;
	private IdentNode matchClass;

	private TypeNode arrayOfMatchTypeUnresolved;
	private TypeNode arrayOfMatchType;

	public MultiRuleQueryExprNode(Coords coords, CollectNode<ExprNode> ruleQueries, IdentNode matchClass,
			TypeNode arrayOfMatchType)
	{
		super(coords);

		this.ruleQueries = becomeParent(ruleQueries);
		this.matchClass = becomeParent(matchClass);
		this.arrayOfMatchTypeUnresolved = becomeParent(arrayOfMatchType);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ruleQueries);
		children.add(matchClass);
		children.add(getValidVersion(arrayOfMatchTypeUnresolved, arrayOfMatchType));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ruleQueries");
		childrenNames.add("matchClass");
		childrenNames.add("arrayOfMatchType");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		if(arrayOfMatchTypeUnresolved.resolve()) {
			arrayOfMatchType = arrayOfMatchTypeUnresolved;
		}
		return arrayOfMatchType != null;
	}

	@Override
	protected boolean checkLocal()
	{
		// all actions must implement the match classes of the employed filters
		for(ExprNode ruleQuery : ruleQueries.getChildren()) {
			CallActionNode actionCall = ((RuleQueryExprNode)ruleQuery).getCallAction();
			MultiCallActionNode.checkWhetherCalledActionImplementsMatchClass(matchClass.getIdent().toString(), null,
					actionCall);
		}

		return true;
	}

	@Override
	protected IR constructIR()
	{
		return new MultiRuleQueryExpr(getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return arrayOfMatchType;
	}
}
