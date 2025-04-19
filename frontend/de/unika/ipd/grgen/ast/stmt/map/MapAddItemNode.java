/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
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
				ExprNode keyExprOld = keyExpr;
				keyExpr = becomeParent(keyExpr.adjustType(targetKeyType, getCoords()));
				if(keyExpr == ConstNode.getInvalid()) {
					keyExprOld.reportError("The map add item procedure expects as 1. argument (key)"
							+ " a value of type " + targetKeyType.toStringWithDeclarationCoords()
							+ " (but is given a value of type " + keyType.toStringWithDeclarationCoords() + ").");
					return false;
				}
			}
			TypeNode targetValueType = targetType.valueType;
			TypeNode valueType = valueExpr.getType();
			if(!valueType.isEqual(targetValueType)) {
				ExprNode valueExprOld = valueExpr;
				valueExpr = becomeParent(valueExpr.adjustType(targetValueType, getCoords()));
				if(valueExpr == ConstNode.getInvalid()) {
					valueExprOld.reportError("The map add item procedure expects as 2. argument (value)"
							+ " a value of type " + targetValueType.toStringWithDeclarationCoords()
							+ " (but is given a value of type " + valueType.toStringWithDeclarationCoords() + ").");
					return false;
				}
			}
		} else {
			TypeNode targetKeyType = targetType.keyType;
			TypeNode targetValueType = targetType.valueType;
			return checkType(keyExpr, targetKeyType, "map add item procedure", "key")
					&& checkType(valueExpr, targetValueType, "map add item procedure", "value");
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		keyExpr = keyExpr.evaluate();
		valueExpr = valueExpr.evaluate();
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
