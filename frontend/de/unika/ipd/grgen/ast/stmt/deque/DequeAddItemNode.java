/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
import de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Variable;
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
				ExprNode valueExprOld = valueExpr;
				valueExpr = becomeParent(valueExpr.adjustType(targetValueType, getCoords()));
				if(valueExpr == ConstNode.getInvalid()) {
					valueExprOld.reportError("The deque add item procedure expects as 1. argument (value)"
							+ " a value of type " + targetValueType.toStringWithDeclarationCoords()
							+ " (but is given a value of type " + valueType.toStringWithDeclarationCoords() + ").");
					return false;
				}
			}
			if(indexExpr != null) {
				TypeNode indexType = indexExpr.getType();
				if(!indexType.isEqual(IntTypeNode.intType)) {
					ExprNode indexExprOld = indexExpr;
					indexExpr = becomeParent(indexExpr.adjustType(IntTypeNode.intType, getCoords()));
					if(indexExpr == ConstNode.getInvalid()) {
						indexExprOld.reportError("The deque add item procedure expects as 2. argument (index)"
								+ " a value of type int"
								+ " (but is given a value of type " + indexType.toStringWithDeclarationCoords() + ").");
						return false;
					}
				}
			}
			return true;
		} else {
			boolean success = true;
			TypeNode targetValueType = targetType.valueType;
			if(indexExpr != null)
				success &= checkType(indexExpr, IntTypeNode.intType, "deque add item with index procedure", "index");
			success &= checkType(valueExpr, targetValueType, "deque add item procedure", "value");
			return success;
		}
	}

	@Override
	protected IR constructIR()
	{
		valueExpr = valueExpr.evaluate();
		if(indexExpr != null)
			indexExpr = indexExpr.evaluate();
		if(target != null) {
			return new DequeAddItem(target.checkIR(Qualification.class),
					valueExpr.checkIR(Expression.class),
					indexExpr != null ? indexExpr.checkIR(Expression.class) : null);
		} else {
			return new DequeVarAddItem(targetVar.checkIR(Variable.class),
					valueExpr.checkIR(Expression.class),
					indexExpr != null ? indexExpr.checkIR(Expression.class) : null);
		}
	}
}
