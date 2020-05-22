/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.stmt.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.model.EdgeTypeNode;
import de.unika.ipd.grgen.ast.type.model.NodeTypeNode;
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

	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(edgeExpr);
		children.add(newTargetExpr);
		if(oldTargetNameExpr != null)
			children.add(oldTargetNameExpr);
		return children;
	}

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
		if(!(edgeExpr.getType() instanceof EdgeTypeNode)) {
			reportError("first(target) argument of redirectTarget(.,.,.) must be of edge type");
			return false;
		}
		if(!(newTargetExpr.getType() instanceof NodeTypeNode)) {
			reportError("second(source) argument of redirectTarget(.,.,.) must be of node type");
			return false;
		}
		if(oldTargetNameExpr != null
				&& !(oldTargetNameExpr.getType().equals(BasicTypeNode.stringType))) {
			reportError("third(source name) argument of redirectTarget(.,.,.) must be of string type");
			return false;
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
		return new GraphRedirectTargetProc(edgeExpr.checkIR(Expression.class),
				newTargetExpr.checkIR(Expression.class),
				oldTargetNameExpr != null ? oldTargetNameExpr.checkIR(Expression.class) : null);
	}
}
