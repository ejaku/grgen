/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.exprevals.Qualification;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.containers.ArrayAddItem;
import de.unika.ipd.grgen.ir.containers.ArrayVarAddItem;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayAddItemNode extends ProcedureMethodInvocationBaseNode
{
	static {
		setName(ArrayAddItemNode.class, "array add item statement");
	}

	private QualIdentNode target;
	private VarDeclNode targetVar;
	private ExprNode valueExpr;
	private ExprNode indexExpr;

	public ArrayAddItemNode(Coords coords, QualIdentNode target, ExprNode valueExpr, ExprNode indexExpr)
	{
		super(coords);
		this.target = becomeParent(target);
		this.valueExpr = becomeParent(valueExpr);
		if(indexExpr!=null)
			this.indexExpr = becomeParent(indexExpr);
	}

	public ArrayAddItemNode(Coords coords, VarDeclNode targetVar, ExprNode valueExpr, ExprNode indexExpr)
	{
		super(coords);
		this.targetVar = becomeParent(targetVar);
		this.valueExpr = becomeParent(valueExpr);
		if(indexExpr!=null)
			this.indexExpr = becomeParent(indexExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target!=null ? target : targetVar);
		children.add(valueExpr);
		if(indexExpr!=null)
			children.add(indexExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("valueExpr");
		if(indexExpr!=null)
			childrenNames.add("indexExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		if(target!=null) {
			TypeNode targetType = target.getDecl().getDeclType();
			TypeNode targetValueType = ((ArrayTypeNode)targetType).valueType;
			TypeNode valueType = valueExpr.getType();
			if (!valueType.isEqual(targetValueType))
			{
				valueExpr = becomeParent(valueExpr.adjustType(targetValueType, getCoords()));
				if(valueExpr == ConstNode.getInvalid()) {
					valueExpr.reportError("Argument value to "
							+ "array add item statement must be of type " +targetValueType.toString());
					return false;
				}
			}
			if (indexExpr!=null) {
				TypeNode indexType = indexExpr.getType();
				if (!indexType.isEqual(IntTypeNode.intType))
				{
					indexExpr = becomeParent(indexExpr.adjustType(IntTypeNode.intType, getCoords()));
					if(indexExpr == ConstNode.getInvalid()) {
						indexExpr.reportError("Argument index to array add item statement must be of type int");
						return false;
					}
				}
			}
			return true;
		} else {
			boolean success = true;
			TypeNode targetType = targetVar.getDeclType();
			TypeNode targetValueType = ((ArrayTypeNode)targetType).valueType;
			if(indexExpr!=null)
				success &= checkType(indexExpr, IntTypeNode.intType, "array add item with index statement", "index");
			success &= checkType(valueExpr, targetValueType, "array add item statement", "value");
			return success;
		}
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		if(target!=null)
			return new ArrayAddItem(target.checkIR(Qualification.class),
					valueExpr.checkIR(Expression.class),
					indexExpr!=null ? indexExpr.checkIR(Expression.class) : null);
		else
			return new ArrayVarAddItem(targetVar.checkIR(Variable.class),
					valueExpr.checkIR(Expression.class),
					indexExpr!=null ? indexExpr.checkIR(Expression.class) : null);
	}
}
