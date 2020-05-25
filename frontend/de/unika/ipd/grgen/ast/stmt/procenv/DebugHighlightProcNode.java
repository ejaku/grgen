/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.procenv;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.procenv.DebugHighlightProc;
import de.unika.ipd.grgen.parser.Coords;

public class DebugHighlightProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(DebugHighlightProcNode.class, "debug highlight procedure");
	}

	private CollectNode<ExprNode> exprs = new CollectNode<ExprNode>();

	public DebugHighlightProcNode(Coords coords)
	{
		super(coords);

		this.exprs = becomeParent(exprs);
	}

	public void addExpression(ExprNode expr)
	{
		exprs.addChild(expr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(exprs);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("exprs");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		int paramNum = 0;
		for(ExprNode expr : exprs.getChildren()) {
			if(paramNum % 2 == 0 && !(expr.getType().equals(BasicTypeNode.stringType))) {
				reportError("argument " + paramNum
						+ " of Debug::highlight() must be of string type (a message followed by a sequence of (value, annotation for the value)* must be given)");
				return false;
			}
			++paramNum;
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		Vector<Expression> expressions = new Vector<Expression>();
		for(ExprNode expr : exprs.getChildren()) {
			expressions.add(expr.checkIR(Expression.class));
		}
		return new DebugHighlightProc(expressions);
	}
}