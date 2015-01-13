/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.containers.MapAddItem;
import de.unika.ipd.grgen.ir.containers.MapVarAddItem;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.exprevals.Qualification;
import de.unika.ipd.grgen.parser.Coords;

public class MapAddItemNode extends ProcedureMethodInvocationBaseNode
{
	static {
		setName(MapAddItemNode.class, "map add item statement");
	}

	private QualIdentNode target;
	private VarDeclNode targetVar;
	private ExprNode keyExpr;
	private ExprNode valueExpr;

	public MapAddItemNode(Coords coords, QualIdentNode target, ExprNode keyExpr, ExprNode valueExpr)
	{
		super(coords);
		this.target = becomeParent(target);
		this.keyExpr = becomeParent(keyExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	public MapAddItemNode(Coords coords, VarDeclNode targetVar, ExprNode keyExpr, ExprNode valueExpr)
	{
		super(coords);
		this.targetVar = becomeParent(targetVar);
		this.keyExpr = becomeParent(keyExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target!=null ? target : targetVar);
		children.add(keyExpr);
		children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("keyExpr");
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
			TypeNode targetType = target.getDecl().getDeclType();
			TypeNode targetKeyType = ((MapTypeNode)targetType).keyType;
			TypeNode keyType = keyExpr.getType();
			if (!keyType.isEqual(targetKeyType))
			{
				keyExpr = becomeParent(keyExpr.adjustType(targetKeyType, getCoords()));
				if(keyExpr == ConstNode.getInvalid()) {
					keyExpr.reportError("Argument (key) to "
							+ "map add item statement must be of type " +targetKeyType.toString());
					return false;
				}
			}
			TypeNode targetValueType = ((MapTypeNode)targetType).valueType;
			TypeNode valueType = valueExpr.getType();
			if (!valueType.isEqual(targetValueType))
			{
				valueExpr = becomeParent(valueExpr.adjustType(targetValueType, getCoords()));
				if(valueExpr == ConstNode.getInvalid()) {
					valueExpr.reportError("Argument (value) to "
							+ "map add item statement must be of type " +targetValueType.toString());
					return false;
				}
			}
		} else {
			TypeNode targetType = targetVar.getDeclType();
			TypeNode targetKeyType = ((MapTypeNode)targetType).keyType;
			TypeNode targetValueType = ((MapTypeNode)targetType).valueType;
			return checkType(keyExpr, targetKeyType, "map add item statement", "key")
				&& checkType(valueExpr, targetValueType, "map add item statement", "value");
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		if(target!=null)
			return new MapAddItem(target.checkIR(Qualification.class),
					keyExpr.checkIR(Expression.class), valueExpr.checkIR(Expression.class));
		else
			return new MapVarAddItem(targetVar.checkIR(Variable.class),
					keyExpr.checkIR(Expression.class), valueExpr.checkIR(Expression.class));
	}
}
