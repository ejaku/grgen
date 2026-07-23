/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ast.stmt.graph;

import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.graph.GraphRemoveProc;
import de.unika.ipd.grgen.parser.Coords;

public class GraphRemoveProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setClassName(GraphRemoveProcNode.class, "graph remove procedure");
	}

	private ExprNode entityExpr;

	public GraphRemoveProcNode(Coords coords, ExprNode entityExpr)
	{
		super(coords);

		this.entityExpr = entityExpr;
		becomeParent(entityExpr);
	}

	@Override
	public Collection<BaseNode> getChildren()
	{
		List<BaseNode> children = new ArrayList<BaseNode>();
		children.add(entityExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		List<String> childrenNames = new ArrayList<String>();
		childrenNames.add("entity");
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
		TypeNode entityExprType = entityExpr.getType();
		if(entityExprType instanceof EdgeTypeNode) {
			return true;
		}
		if(entityExprType instanceof NodeTypeNode) {
			return true;
		}
		reportError("The rem procedure expects as argument (entity)"
				+ " a value of type Node or Edge"
				+ " (but is given a value of type " + entityExprType.toStringWithDeclarationCoords() + ").");
		return false;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		entityExpr = entityExpr.evaluate();
		return new GraphRemoveProc(entityExpr.checkIR(Expression.class));
	}
}
