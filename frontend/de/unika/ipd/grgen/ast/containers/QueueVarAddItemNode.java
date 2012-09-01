/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.5
 * Copyright (C) 2003-2012 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.QueueVarAddItem;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

public class QueueVarAddItemNode extends EvalStatementNode
{
	static {
		setName(QueueVarAddItemNode.class, "queue var add item statement");
	}

	private VarDeclNode target;
	private ExprNode valueExpr;

	public QueueVarAddItemNode(Coords coords, VarDeclNode target, ExprNode valueExpr)
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
		boolean success = true;
		TypeNode targetType = target.getDeclType();
		TypeNode targetValueType = ((QueueTypeNode)targetType).valueType;
		success &= checkType(valueExpr, targetValueType, "queue add item statement", "value");
		return success;
	}

	@Override
	protected IR constructIR() {
		return new QueueVarAddItem(target.checkIR(Variable.class),
				valueExpr.checkIR(Expression.class));
	}
}
