/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.containers.SetRemoveItem;
import de.unika.ipd.grgen.ir.containers.SetVarRemoveItem;
import de.unika.ipd.grgen.parser.Coords;

public class SetRemoveItemNode extends ProcedureMethodInvocationBaseNode
{
	static {
		setName(SetRemoveItemNode.class, "set remove item statement");
	}

	private QualIdentNode target;
	private VarDeclNode targetVar;
	private ExprNode valueExpr;

	public SetRemoveItemNode(Coords coords, QualIdentNode target, ExprNode valueExpr)
	{
		super(coords);
		this.target = becomeParent(target);
		this.valueExpr = becomeParent(valueExpr);
	}

	public SetRemoveItemNode(Coords coords, VarDeclNode targetVar, ExprNode valueExpr)
	{
		super(coords);
		this.targetVar = becomeParent(targetVar);
		this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target!=null ? target : targetVar);
		children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
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
			TypeNode targetValueType = ((SetTypeNode)targetType).valueType;
			TypeNode valueType = valueExpr.getType();
			if (!valueType.isEqual(targetValueType))
			{
				valueExpr = becomeParent(valueExpr.adjustType(targetValueType, getCoords()));
				if(valueExpr == ConstNode.getInvalid()) {
					valueExpr.reportError("Argument (value) to "
							+ "set remove item statement must be of type " +targetValueType.toString());
					return false;
				}
			}
			return true;
		} else {
			TypeNode targetType = targetVar.getDeclType();
			TypeNode targetValueType = ((SetTypeNode)targetType).valueType;
			return checkType(valueExpr, targetValueType, "value", "set remove item statement");
		}
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		if(target!=null)
			return new SetRemoveItem(target.checkIR(Qualification.class),
					valueExpr.checkIR(Expression.class));
		else
			return new SetVarRemoveItem(targetVar.checkIR(Variable.class),
					valueExpr.checkIR(Expression.class));
	}
}
