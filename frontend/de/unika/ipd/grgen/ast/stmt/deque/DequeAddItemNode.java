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
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.typedecl.DequeTypeNode;
import de.unika.ipd.grgen.ast.typedecl.IntTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.stmt.deque.DequeAddItem;
import de.unika.ipd.grgen.ir.stmt.deque.DequeVarAddItem;
import de.unika.ipd.grgen.parser.Coords;

public class DequeAddItemNode extends DequeProcedureMethodInvocationBaseNode
{
	static {
		setName(DequeAddItemNode.class, "Deque add item statement");
	}

	private ExprNode valueExpr;
	private ExprNode indexExpr;

	public DequeAddItemNode(Coords coords, QualIdentNode target, ExprNode valueExpr, ExprNode indexExpr)
	{
		super(coords, target);
		this.valueExpr = becomeParent(valueExpr);
		if(indexExpr != null)
			this.indexExpr = becomeParent(indexExpr);
	}

	public DequeAddItemNode(Coords coords, VarDeclNode targetVar, ExprNode valueExpr, ExprNode indexExpr)
	{
		super(coords, targetVar);
		this.valueExpr = becomeParent(valueExpr);
		if(indexExpr != null)
			this.indexExpr = becomeParent(indexExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target != null ? target : targetVar);
		children.add(valueExpr);
		if(indexExpr != null)
			children.add(indexExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("valueExpr");
		if(indexExpr != null)
			childrenNames.add("indexExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		DequeTypeNode targetType = getTargetType();
		if(target != null) {
			TypeNode targetValueType = targetType.valueType;
			TypeNode valueType = valueExpr.getType();
			if(!valueType.isEqual(targetValueType)) {
				valueExpr = becomeParent(valueExpr.adjustType(targetValueType, getCoords()));
				if(valueExpr == ConstNode.getInvalid()) {
					valueExpr.reportError("Argument value to deque add item statement must be of type "
							+ targetValueType.toString());
					return false;
				}
			}
			if(indexExpr != null) {
				TypeNode indexType = indexExpr.getType();
				if(!indexType.isEqual(IntTypeNode.intType)) {
					indexExpr = becomeParent(indexExpr.adjustType(IntTypeNode.intType, getCoords()));
					if(indexExpr == ConstNode.getInvalid()) {
						indexExpr.reportError("Argument index to deque add item statement must be of type int");
						return false;
					}
				}
			}
			return true;
		} else {
			boolean success = true;
			TypeNode targetValueType = targetType.valueType;
			if(indexExpr != null)
				success &= checkType(indexExpr, IntTypeNode.intType, "deque add item with index statement", "index");
			success &= checkType(valueExpr, targetValueType, "deque add item statement", "value");
			return success;
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
			return new DequeAddItem(target.checkIR(Qualification.class),
					valueExpr.checkIR(Expression.class),
					indexExpr != null ? indexExpr.checkIR(Expression.class) : null);
		else
			return new DequeVarAddItem(targetVar.checkIR(Variable.class),
					valueExpr.checkIR(Expression.class),
					indexExpr != null ? indexExpr.checkIR(Expression.class) : null);
	}
}
