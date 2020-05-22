/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.deque;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.deque.DequeRemoveItem;
import de.unika.ipd.grgen.ir.stmt.deque.DequeVarRemoveItem;
import de.unika.ipd.grgen.parser.Coords;

public class DequeRemoveItemNode extends DequeProcedureMethodInvocationBaseNode
{
	static {
		setName(DequeRemoveItemNode.class, "deque remove item statement");
	}

	private ExprNode valueExpr;

	public DequeRemoveItemNode(Coords coords, QualIdentNode target, ExprNode valueExpr)
	{
		super(coords, target);
		if(valueExpr != null)
			this.valueExpr = becomeParent(valueExpr);
	}

	public DequeRemoveItemNode(Coords coords, VarDeclNode targetVar, ExprNode valueExpr)
	{
		super(coords, targetVar);
		if(valueExpr != null)
			this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target != null ? target : targetVar);
		if(valueExpr != null)
			children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		if(valueExpr != null)
			childrenNames.add("valueExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		if(target != null) {
			if(valueExpr != null) {
				TypeNode valueType = valueExpr.getType();
				if(!valueType.isEqual(IntTypeNode.intType)) {
					valueExpr = becomeParent(valueExpr.adjustType(IntTypeNode.intType, getCoords()));
					if(valueExpr == ConstNode.getInvalid()) {
						valueExpr.reportError("Argument to deque remove item statement must be of type int");
						return false;
					}
				}
			}
			return true;
		} else {
			if(valueExpr != null)
				return checkType(valueExpr, IntTypeNode.intType, "index value", "deque remove item statement");
			else
				return true;
		}
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		if(target != null)
			return new DequeRemoveItem(target.checkIR(Qualification.class),
					valueExpr != null ? valueExpr.checkIR(Expression.class) : null);
		else
			return new DequeVarRemoveItem(targetVar.checkIR(Variable.class),
					valueExpr != null ? valueExpr.checkIR(Expression.class) : null);
	}
}
