/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.ArraySubarrayExpr;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArraySubarrayNode extends ExprNode
{
	static {
		setName(ArraySubarrayNode.class, "array subarray");
	}

	private ExprNode targetExpr;
	private ExprNode startExpr;
	private ExprNode lengthExpr;

	public ArraySubarrayNode(Coords coords, ExprNode targetExpr, ExprNode startExpr, ExprNode lengthExpr)
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
		if(!(targetType instanceof ArrayTypeNode)) {
			targetExpr.reportError("This argument to array subarray expression must be of type array<T>");
			return false;
		}
		if(!startExpr.getType().isEqual(BasicTypeNode.intType)) {
			startExpr.reportError("First argument (start position) to "
					+ "subarray expression must be of type int");
			return false;
		}
		if(!lengthExpr.getType().isEqual(BasicTypeNode.intType)) {
			lengthExpr.reportError("Second argument (length) to subarray "
					+ "expression must be of type int");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return ((ArrayTypeNode)targetExpr.getType());
	}

	@Override
	protected IR constructIR() {
		return new ArraySubarrayExpr(targetExpr.checkIR(Expression.class), 
				startExpr.checkIR(Expression.class), lengthExpr.checkIR(Expression.class));
	}
}
