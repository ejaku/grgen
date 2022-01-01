/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.array;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.array.ArrayVarAddAll;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayAddAllNode extends ArrayProcedureMethodInvocationBaseNode
{
	static {
		setName(ArrayAddAllNode.class, "array add all statement");
	}

	private ExprNode valueExpr;

	public ArrayAddAllNode(Coords coords, VarDeclNode targetVar, ExprNode valueExpr)
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
		ArrayTypeNode targetType = getTargetType();
		boolean success = true;
		success &= checkType(valueExpr, targetType, "array add all statement", "value");
		return success;
	}

	@Override
	protected IR constructIR()
	{
		valueExpr = valueExpr.evaluate();
		return new ArrayVarAddAll(targetVar.checkIR(Variable.class), valueExpr.checkIR(Expression.class));
	}
}
