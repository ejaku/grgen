/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ir.containers.DequeSubdequeExpr;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class DequeSubdequeNode extends ExprNode
{
	static {
		setName(DequeSubdequeNode.class, "deque subdeque");
	}

	private ExprNode targetExpr;
	private ExprNode startExpr;
	private ExprNode lengthExpr;

	public DequeSubdequeNode(Coords coords, ExprNode targetExpr, ExprNode startExpr, ExprNode lengthExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
		this.startExpr = becomeParent(startExpr);
		this.lengthExpr = becomeParent(lengthExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(startExpr);
		children.add(lengthExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("startExpr");
		childrenNames.add("lengthExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof DequeTypeNode)) {
			targetExpr.reportError("This argument to deque subdeque expression must be of type deque<T>");
			return false;
		}
		if(!startExpr.getType().isEqual(BasicTypeNode.intType)) {
			startExpr.reportError("First argument (start position) to "
					+ "subdeque expression must be of type int");
			return false;
		}
		if(!lengthExpr.getType().isEqual(BasicTypeNode.intType)) {
			lengthExpr.reportError("Second argument (length) to subdeque "
					+ "expression must be of type int");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return ((DequeTypeNode)targetExpr.getType());
	}

	@Override
	protected IR constructIR() {
		return new DequeSubdequeExpr(targetExpr.checkIR(Expression.class),
				startExpr.checkIR(Expression.class), lengthExpr.checkIR(Expression.class));
	}
}
