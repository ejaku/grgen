/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: SetAddItemNode.java 22995 2008-10-18 14:51:18Z buchwald $
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.SetAddItem;
import de.unika.ipd.grgen.parser.Coords;

public class SetAddItemNode extends EvalStatementNode
{
	static {
		setName(SetAddItemNode.class, "set add item statement");
	}

	private QualIdentNode target;
	private ExprNode valueExpr;

	public SetAddItemNode(Coords coords, QualIdentNode target, ExprNode valueExpr)
	{
		super(coords);
		this.target = becomeParent(target);
		this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target);
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
		TypeNode targetType = target.getDecl().getDeclType();
		TypeNode targetValueType = ((SetTypeNode)targetType).valueType;
		TypeNode valueType = valueExpr.getType();
		if (!valueType.isEqual(targetValueType))
		{
			valueExpr = becomeParent(valueExpr.adjustType(targetValueType, getCoords()));
			if(valueExpr == ConstNode.getInvalid()) {
				valueExpr.reportError("Argument (value) to "
						+ "set add item statement must be of type " +targetValueType.toString());
				return false;
			}
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new SetAddItem(target.checkIR(Qualification.class), 
				valueExpr.checkIR(Expression.class));
	}
}
