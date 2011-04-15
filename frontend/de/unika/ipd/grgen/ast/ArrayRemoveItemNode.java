/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.ArrayRemoveItem;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayRemoveItemNode extends EvalStatementNode
{
	static {
		setName(ArrayRemoveItemNode.class, "array remove item statement");
	}

	private QualIdentNode target;
	private ExprNode valueExpr;

	public ArrayRemoveItemNode(Coords coords, QualIdentNode target, ExprNode valueExpr)
	{
		super(coords);
		this.target = becomeParent(target);
		if(valueExpr!=null)
			this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target);
		if(valueExpr!=null)
			children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		if(valueExpr!=null)
			childrenNames.add("valueExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		//TypeNode targetType = target.getDecl().getDeclType();
		//TypeNode targetValueType = ((ArrayTypeNode)targetType).valueType;
		if(valueExpr!=null) {
			TypeNode valueType = valueExpr.getType();
			if (!valueType.isEqual(IntTypeNode.intType))
			{
				valueExpr = becomeParent(valueExpr.adjustType(IntTypeNode.intType, getCoords()));
				if(valueExpr == ConstNode.getInvalid()) {
					valueExpr.reportError("Argument to array remove item statement must be of type int");
					return false;
				}
			}
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new ArrayRemoveItem(target.checkIR(Qualification.class), 
				valueExpr!=null ? valueExpr.checkIR(Expression.class) : null);
	}
}
