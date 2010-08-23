/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: MapAddItemNode.java 22995 2008-10-18 14:51:18Z buchwald $
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MapAddItem;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.parser.Coords;

public class MapAddItemNode extends EvalStatementNode
{
	static {
		setName(MapAddItemNode.class, "map add item statement");
	}

	private QualIdentNode target;
	private ExprNode keyExpr;
	private ExprNode valueExpr;

	public MapAddItemNode(Coords coords, QualIdentNode target, ExprNode keyExpr, ExprNode valueExpr)
	{
		super(coords);
		this.target = becomeParent(target);
		this.keyExpr = becomeParent(keyExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target);
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
		return true;
	}

	@Override
	protected IR constructIR() {
		return new MapAddItem(target.checkIR(Qualification.class), 
				keyExpr.checkIR(Expression.class), valueExpr.checkIR(Expression.class));
	}
}
