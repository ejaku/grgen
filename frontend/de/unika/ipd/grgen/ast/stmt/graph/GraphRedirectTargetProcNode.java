/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.stmt.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.graph.GraphRedirectTargetProc;
import de.unika.ipd.grgen.parser.Coords;

public class GraphRedirectTargetProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(GraphRedirectTargetProcNode.class, "graph redirect target procedure");
	}

	private ExprNode edgeExpr;
	private ExprNode newTargetExpr;
	private ExprNode oldTargetNameExpr;

	public GraphRedirectTargetProcNode(Coords coords, ExprNode edgeExpr, ExprNode newTargetExpr,
			ExprNode oldTargetNameExpr)
	{
		super(coords);

		this.edgeExpr = edgeExpr;
		becomeParent(edgeExpr);
		this.newTargetExpr = newTargetExpr;
		becomeParent(newTargetExpr);
		this.oldTargetNameExpr = oldTargetNameExpr;
		if(oldTargetNameExpr != null)
			becomeParent(oldTargetNameExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(edgeExpr);
		children.add(newTargetExpr);
		if(oldTargetNameExpr != null)
			children.add(oldTargetNameExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("edge");
		childrenNames.add("newTarget");
		if(oldTargetNameExpr != null)
			childrenNames.add("oldTargetName");
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
		TypeNode edgeExprType = edgeExpr.getType();
		if(!(edgeExprType instanceof EdgeTypeNode)) {
			reportError("The redirectTarget procedure expects as 1. argument (edgeToBeRedirected)"
					+ " a value of type Edge"
					+ " (but is given a value of type " + edgeExprType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode newTargetExprType = newTargetExpr.getType();
		if(!(newTargetExprType instanceof NodeTypeNode)) {
			reportError("The redirectTarget procedure expects as 2. argument (newTargetNode)"
					+ " a value of type Node"
					+ " (but is given a value of type " + newTargetExprType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		if(oldTargetNameExpr != null) {
			TypeNode oldTargetNameExprType = oldTargetNameExpr.getType();
			if(!(oldTargetNameExprType.equals(BasicTypeNode.stringType))) {
				reportError("The redirectTarget procedure expects as 3. argument (oldTargetName)"
						+ " a value of type string"
						+ " (but is given a value of type " + oldTargetNameExprType.toStringWithDeclarationCoords() + ").");
				return false;
			}
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
		edgeExpr = edgeExpr.evaluate();
		newTargetExpr = newTargetExpr.evaluate();
		if(oldTargetNameExpr != null)
			oldTargetNameExpr = oldTargetNameExpr.evaluate();
		return new GraphRedirectTargetProc(edgeExpr.checkIR(Expression.class),
				newTargetExpr.checkIR(Expression.class),
				oldTargetNameExpr != null ? oldTargetNameExpr.checkIR(Expression.class) : null);
	}
}
