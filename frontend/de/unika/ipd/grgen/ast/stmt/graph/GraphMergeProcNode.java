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
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.graph.GraphMergeProc;
import de.unika.ipd.grgen.parser.Coords;

public class GraphMergeProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(GraphMergeProcNode.class, "graph merge procedure");
	}

	private ExprNode targetExpr;
	private ExprNode sourceExpr;
	private ExprNode sourceNameExpr;

	public GraphMergeProcNode(Coords coords, ExprNode targetExpr, ExprNode sourceExpr, ExprNode sourceNameExpr)
	{
		super(coords);

		this.targetExpr = targetExpr;
		becomeParent(targetExpr);
		this.sourceExpr = sourceExpr;
		becomeParent(sourceExpr);
		this.sourceNameExpr = sourceNameExpr;
		if(sourceNameExpr != null)
			becomeParent(sourceNameExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(sourceExpr);
		if(sourceNameExpr != null)
			children.add(sourceNameExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("source");
		if(sourceNameExpr != null)
			childrenNames.add("sourceName");
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
		if(!(targetExpr.getType() instanceof NodeTypeNode)) {
			reportError("first(target) argument of merge(.,.,.) must be of node type");
			return false;
		}
		if(!(sourceExpr.getType() instanceof NodeTypeNode)) {
			reportError("second(source) argument of merge(.,.,.) must be of node type");
			return false;
		}
		if(sourceNameExpr != null
				&& !(sourceNameExpr.getType().equals(BasicTypeNode.stringType))) {
			reportError("third(source name) argument of merge(.,.,.) must be of string type");
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
		targetExpr = targetExpr.evaluate();
		sourceExpr = sourceExpr.evaluate();
		if(sourceNameExpr != null)
			sourceNameExpr = sourceNameExpr.evaluate();
		return new GraphMergeProc(targetExpr.checkIR(Expression.class), sourceExpr.checkIR(Expression.class),
				sourceNameExpr != null ? sourceNameExpr.checkIR(Expression.class) : null);
	}
}
