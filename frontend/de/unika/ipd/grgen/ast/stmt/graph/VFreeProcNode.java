/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.graph.VFreeProc;
import de.unika.ipd.grgen.parser.Coords;

public class VFreeProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(VFreeProcNode.class, "vfree procedure");
	}

	private ExprNode visFlagExpr;

	public VFreeProcNode(Coords coords, ExprNode visFlagExpr)
	{
		super(coords);

		this.visFlagExpr = becomeParent(visFlagExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(visFlagExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("visFlagExpr");
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
		TypeNode visFlagExprType = visFlagExpr.getType();
		if(!visFlagExprType.isEqual(BasicTypeNode.intType)) {
			visFlagExpr.reportError("The vfree procedure expects as argument (visitedFlagId)"
					+ " a value of type int"
					+ " (but is given a value of type " + visFlagExprType + " [declared at " + visFlagExprType.getCoords() + "]" + ").");
			return false;
		}
		return true;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		visFlagExpr = visFlagExpr.evaluate();
		return new VFreeProc(visFlagExpr.checkIR(Expression.class));
	}
}
