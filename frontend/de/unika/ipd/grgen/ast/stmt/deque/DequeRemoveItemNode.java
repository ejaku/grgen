/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.deque;

import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
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
		setClassName(DequeRemoveItemNode.class, "deque remove item statement");
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
	public Collection<BaseNode> getChildren()
	{
		List<BaseNode> children = new ArrayList<BaseNode>();
		children.add(target != null ? target : targetVar);
		if(valueExpr != null)
			children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		List<String> childrenNames = new ArrayList<String>();
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
					ExprNode valueExprOld = valueExpr;
					valueExpr = becomeParent(valueExpr.adjustType(IntTypeNode.intType, getCoords()));
					if(valueExpr == ConstNode.getInvalid()) {
						valueExprOld.reportError("The deque rem item procedure expects as argument (index)"
								+ " a value of type int"
								+ " (but is given a value of type " + valueType.toStringWithDeclarationCoords() + ").");
						return false;
					}
				}
			}
			return true;
		} else {
			if(valueExpr != null)
				return checkType(valueExpr, IntTypeNode.intType, "index value", "deque rem item procedure");
			else
				return true;
		}
	}

	@Override
	protected IR constructIR()
	{
		if(valueExpr != null)
			valueExpr = valueExpr.evaluate();
		if(target != null) {
			return new DequeRemoveItem(target.checkIR(Qualification.class),
					valueExpr != null ? valueExpr.checkIR(Expression.class) : null);
		} else {
			return new DequeVarRemoveItem(targetVar.checkIR(Variable.class),
					valueExpr != null ? valueExpr.checkIR(Expression.class) : null);
		}
	}
}
