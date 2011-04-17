/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
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
import de.unika.ipd.grgen.ir.ArrayVarRemoveItem;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayVarRemoveItemNode extends EvalStatementNode
{
	static {
		setName(ArrayVarRemoveItemNode.class, "array var remove item statement");
	}

	private VarDeclNode target;
	private ExprNode valueExpr;

	public ArrayVarRemoveItemNode(Coords coords, VarDeclNode target, ExprNode valueExpr)
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
		//TypeNode targetType = target.getDeclType();
		//TypeNode targetValueType = ((ArrayTypeNode)targetType).valueType;
		if(valueExpr!=null)
			return checkType(valueExpr, IntTypeNode.intType, "index value", "array remove item statement");
		else
			return true;
	}

	@Override
	protected IR constructIR() {
		return new ArrayVarRemoveItem(target.checkIR(Variable.class), 
				valueExpr!=null ? valueExpr.checkIR(Expression.class) : null);
	}
}
