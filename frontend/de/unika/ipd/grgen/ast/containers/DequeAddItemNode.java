/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.containers.DequeAddItem;
import de.unika.ipd.grgen.parser.Coords;

public class DequeAddItemNode extends EvalStatementNode
{
	static {
		setName(DequeAddItemNode.class, "Deque add item statement");
	}

	private QualIdentNode target;
	private ExprNode valueExpr;
	private ExprNode indexExpr;

	public DequeAddItemNode(Coords coords, QualIdentNode target, ExprNode valueExpr, ExprNode indexExpr)
	{
		super(coords);
		this.target = becomeParent(target);
		this.valueExpr = becomeParent(valueExpr);
		if(indexExpr!=null)
			this.indexExpr = becomeParent(indexExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target);
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
		TypeNode targetType = target.getDecl().getDeclType();
		TypeNode targetValueType = ((DequeTypeNode)targetType).valueType;
		TypeNode valueType = valueExpr.getType();
		if (!valueType.isEqual(targetValueType))
		{
			valueExpr = becomeParent(valueExpr.adjustType(targetValueType, getCoords()));
			if(valueExpr == ConstNode.getInvalid()) {
				valueExpr.reportError("Argument value to "
						+ "deque add item statement must be of type " +targetValueType.toString());
				return false;
			}
		}
		if (indexExpr!=null) {
			TypeNode indexType = indexExpr.getType();
			if (!indexType.isEqual(IntTypeNode.intType))
			{
				indexExpr = becomeParent(indexExpr.adjustType(IntTypeNode.intType, getCoords()));
				if(indexExpr == ConstNode.getInvalid()) {
					indexExpr.reportError("Argument index to deque add item statement must be of type int");
					return false;
				}
			}
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new DequeAddItem(target.checkIR(Qualification.class),
				valueExpr.checkIR(Expression.class),
				indexExpr!=null ? indexExpr.checkIR(Expression.class) : null);
	}
}
