/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.parser.Coords;

public abstract class DebugProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(DebugProcNode.class, "debug procedure");
	}

	protected CollectNode<ExprNode> exprs = new CollectNode<ExprNode>();

	public DebugProcNode(Coords coords)
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
		ExprNode message = exprs.get(0);
		TypeNode messageType = message.getType();
		if(!(messageType.equals(BasicTypeNode.stringType))) {
			reportError("The " + shortSignature() + " procedure expects as argument (message)"
					+ " a value of type string"
					+ " (but is given a value of type " + messageType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		return true;
	}

	protected abstract String shortSignature();

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}
}
