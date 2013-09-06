/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
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
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.exprevals.Qualification;
import de.unika.ipd.grgen.ir.containers.DequeRemoveItem;
import de.unika.ipd.grgen.ir.containers.DequeVarRemoveItem;
import de.unika.ipd.grgen.parser.Coords;

public class DequeRemoveItemNode extends ProcedureMethodInvocationBaseNode
{
	static {
		setName(DequeRemoveItemNode.class, "deque remove item statement");
	}

	private QualIdentNode target;
	private VarDeclNode targetVar;
	private ExprNode valueExpr;

	public DequeRemoveItemNode(Coords coords, QualIdentNode target, ExprNode valueExpr)
	{
		super(coords);
		this.target = becomeParent(target);
		if(valueExpr!=null)
			this.valueExpr = becomeParent(valueExpr);
	}

	public DequeRemoveItemNode(Coords coords, VarDeclNode targetVar, ExprNode valueExpr)
	{
		super(coords);
		this.targetVar = becomeParent(targetVar);
		if(valueExpr!=null)
			this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target!=null ? target : targetVar);
		if(valueExpr!=null)
			children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		if(valueExpr!=null)
			childrenNames.add("valueExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		if(target!=null) {
			if(valueExpr!=null) {
				TypeNode valueType = valueExpr.getType();
				if (!valueType.isEqual(IntTypeNode.intType))
				{
					valueExpr = becomeParent(valueExpr.adjustType(IntTypeNode.intType, getCoords()));
					if(valueExpr == ConstNode.getInvalid()) {
						valueExpr.reportError("Argument to deque remove item statement must be of type int");
						return false;
					}
				}
			}
			return true;
		} else {
			if(valueExpr!=null)
				return checkType(valueExpr, IntTypeNode.intType, "index value", "deque remove item statement");
			else
				return true;
		}
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		if(target!=null)
			return new DequeRemoveItem(target.checkIR(Qualification.class),
					valueExpr!=null ? valueExpr.checkIR(Expression.class) : null);
		else
			return new DequeVarRemoveItem(targetVar.checkIR(Variable.class),
					valueExpr!=null ? valueExpr.checkIR(Expression.class) : null);
	}
}
