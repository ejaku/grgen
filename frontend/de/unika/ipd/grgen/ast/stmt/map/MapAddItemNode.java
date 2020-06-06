/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.map;

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
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.map.MapAddItem;
import de.unika.ipd.grgen.ir.stmt.map.MapVarAddItem;
import de.unika.ipd.grgen.parser.Coords;

public class MapAddItemNode extends MapProcedureMethodInvocationBaseNode
{
	static {
		setName(MapAddItemNode.class, "map add item statement");
	}

	private ExprNode keyExpr;
	private ExprNode valueExpr;

	public MapAddItemNode(Coords coords, QualIdentNode target, ExprNode keyExpr, ExprNode valueExpr)
	{
		super(coords, target);
		this.keyExpr = becomeParent(keyExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	public MapAddItemNode(Coords coords, VarDeclNode targetVar, ExprNode keyExpr, ExprNode valueExpr)
	{
		super(coords, targetVar);
		this.keyExpr = becomeParent(keyExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target != null ? target : targetVar);
		children.add(keyExpr);
		children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("keyExpr");
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		MapTypeNode targetType = getTargetType();
		if(target != null) {
			TypeNode targetKeyType = targetType.keyType;
			TypeNode keyType = keyExpr.getType();
			if(!keyType.isEqual(targetKeyType)) {
				keyExpr = becomeParent(keyExpr.adjustType(targetKeyType, getCoords()));
				if(keyExpr == ConstNode.getInvalid()) {
					keyExpr.reportError("Argument (key) to map add item statement must be of type "
							+ targetKeyType.toString());
					return false;
				}
			}
			TypeNode targetValueType = targetType.valueType;
			TypeNode valueType = valueExpr.getType();
			if(!valueType.isEqual(targetValueType)) {
				valueExpr = becomeParent(valueExpr.adjustType(targetValueType, getCoords()));
				if(valueExpr == ConstNode.getInvalid()) {
					valueExpr.reportError("Argument (value) to map add item statement must be of type "
							+ targetValueType.toString());
					return false;
				}
			}
		} else {
			TypeNode targetKeyType = targetType.keyType;
			TypeNode targetValueType = targetType.valueType;
			return checkType(keyExpr, targetKeyType, "map add item statement", "key")
					&& checkType(valueExpr, targetValueType, "map add item statement", "value");
		}
		return true;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		if(target != null)
			return new MapAddItem(target.checkIR(Qualification.class),
					keyExpr.checkIR(Expression.class),
					valueExpr.checkIR(Expression.class));
		else
			return new MapVarAddItem(targetVar.checkIR(Variable.class),
					keyExpr.checkIR(Expression.class),
					valueExpr.checkIR(Expression.class));
	}
}
