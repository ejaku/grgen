/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.expr.Constant;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.procenv.AssertProc;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.parser.Coords;

public class AssertProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(AssertProcNode.class, "assert procedure");
	}

	private CollectNode<ExprNode> exprs = new CollectNode<ExprNode>();
	boolean isAlways;

	public AssertProcNode(Coords coords, boolean isAlways)
	{
		super(coords);

		this.exprs = becomeParent(exprs);
		this.isAlways = isAlways;
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
		if(!exprs.get(0).getType().isEqual(BasicTypeNode.booleanType)) {
			exprs.get(0).reportError("first parameter of " + name() + " must be of type boolean (condition to assert on)");
			return false;
		}
		
		if(exprs.size() >= 2 && !exprs.get(1).getType().isEqual(BasicTypeNode.stringType)) {
			exprs.get(1).reportError("second parameter of " + name() + " must be of type string (message)");
			return false;
		}

		// regarding remaining parameters: any type goes, must be converted toString in implementation
		return true;
	}
	
	private String name()
	{
		return isAlways ? "assertAlways" : "assert";
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		Vector<Expression> expressions = new Vector<Expression>();
		for(ExprNode expr : exprs.getChildren()) {
			expr = expr.evaluate();
			expressions.add(expr.checkIR(Expression.class));
		}
		if(exprs.size() == 1) {
			expressions.add(new Constant(BasicTypeNode.stringType.checkIR(Type.class), escapeBackslashAndDoubleQuotes(getCoords().toString())));
		}
		return new AssertProc(expressions, isAlways);
	}
	
	protected static String escapeBackslashAndDoubleQuotes(String input)
	{
		return input.replace("\\", "\\\\").replace("\"", "\\\"");
	}
}