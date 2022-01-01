/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.set;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.set.SetRemoveItem;
import de.unika.ipd.grgen.ir.stmt.set.SetVarRemoveItem;
import de.unika.ipd.grgen.parser.Coords;

public class SetRemoveItemNode extends SetProcedureMethodInvocationBaseNode
{
	static {
		setName(SetRemoveItemNode.class, "set remove item statement");
	}

	private ExprNode valueExpr;

	public SetRemoveItemNode(Coords coords, QualIdentNode target, ExprNode valueExpr)
	{
		super(coords, target);
		this.valueExpr = becomeParent(valueExpr);
	}

	public SetRemoveItemNode(Coords coords, VarDeclNode targetVar, ExprNode valueExpr)
	{
		super(coords, targetVar);
		this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target != null ? target : targetVar);
		children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		SetTypeNode targetType = getTargetType();
		if(target != null) {
			TypeNode targetValueType = targetType.valueType;
			TypeNode valueType = valueExpr.getType();
			if(!valueType.isEqual(targetValueType)) {
				valueExpr = becomeParent(valueExpr.adjustType(targetValueType, getCoords()));
				if(valueExpr == ConstNode.getInvalid()) {
					valueExpr.reportError("Argument (value) to set remove item statement must be of type "
							+ targetValueType.toString());
					return false;
				}
			}
			return true;
		} else {
			TypeNode targetValueType = targetType.valueType;
			return checkType(valueExpr, targetValueType, "value", "set remove item statement");
		}
	}

	@Override
	protected IR constructIR()
	{
		valueExpr = valueExpr.evaluate();
		if(target != null) {
			return new SetRemoveItem(target.checkIR(Qualification.class),
					valueExpr.checkIR(Expression.class));
		} else {
			return new SetVarRemoveItem(targetVar.checkIR(Variable.class),
					valueExpr.checkIR(Expression.class));
		}
	}
}
