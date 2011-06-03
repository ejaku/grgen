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
import de.unika.ipd.grgen.ir.ArrayVarAddItem;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayVarAddItemNode extends EvalStatementNode
{
	static {
		setName(ArrayVarAddItemNode.class, "array var add item statement");
	}

	private VarDeclNode target;
	private ExprNode valueExpr;
	private ExprNode indexExpr;

	public ArrayVarAddItemNode(Coords coords, VarDeclNode target, ExprNode valueExpr, ExprNode indexExpr)
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
		boolean success = true;
		TypeNode targetType = target.getDeclType();
		TypeNode targetValueType = ((ArrayTypeNode)targetType).valueType;
		if(indexExpr!=null)
			success &= checkType(indexExpr, IntTypeNode.intType, "array add item with index statement", "index");
		success &= checkType(valueExpr, targetValueType, "array add item statement", "value");
		return success;
	}

	@Override
	protected IR constructIR() {
		return new ArrayVarAddItem(target.checkIR(Variable.class),
				valueExpr.checkIR(Expression.class),
				indexExpr!=null ? indexExpr.checkIR(Expression.class) : null);
	}
}
