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
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.graph.GraphRedirectSourceAndTargetProc;
import de.unika.ipd.grgen.parser.Coords;

public class GraphRedirectSourceAndTargetProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(GraphRedirectSourceAndTargetProcNode.class, "graph redirect source and target procedure");
	}

	private ExprNode edgeExpr;
	private ExprNode newSourceExpr;
	private ExprNode newTargetExpr;
	private ExprNode oldSourceNameExpr;
	private ExprNode oldTargetNameExpr;

	public GraphRedirectSourceAndTargetProcNode(Coords coords, ExprNode edgeExpr,
			ExprNode newSourceExpr, ExprNode newTargetExpr,
			ExprNode oldSourceNameExpr, ExprNode oldTargetNameExpr)
	{
		super(coords);

		this.edgeExpr = edgeExpr;
		becomeParent(edgeExpr);
		this.newSourceExpr = newSourceExpr;
		becomeParent(newSourceExpr);
		this.newTargetExpr = newTargetExpr;
		becomeParent(newTargetExpr);
		this.oldSourceNameExpr = oldSourceNameExpr;
		if(oldSourceNameExpr != null)
			becomeParent(oldSourceNameExpr);
		this.oldTargetNameExpr = oldTargetNameExpr;
		if(oldTargetNameExpr != null)
			becomeParent(oldTargetNameExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(edgeExpr);
		children.add(newSourceExpr);
		children.add(newTargetExpr);
		if(oldSourceNameExpr != null)
			children.add(oldSourceNameExpr);
		if(oldTargetNameExpr != null)
			children.add(oldTargetNameExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("edge");
		childrenNames.add("newSource");
		childrenNames.add("newTarget");
		if(oldSourceNameExpr != null)
			childrenNames.add("oldSourceName");
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
			reportError("first(edge-to-be-redirected) argument of redirectSourceAndTarget(.,.,.) must be of edge type");
			return false;
		}
		if(!(newSourceExpr.getType() instanceof NodeTypeNode)) {
			reportError("second(source node) argument of redirectSourceAndTarget(.,.,.) must be of node type");
			return false;
		}
		if(!(newTargetExpr.getType() instanceof NodeTypeNode)) {
			reportError("third(target node) argument of redirectSourceAndTarget(.,.,.) must be of node type");
			return false;
		}
		if(oldSourceNameExpr != null
			&& !(oldSourceNameExpr.getType().equals(BasicTypeNode.stringType))) {
			reportError("fourth(source name) argument of redirectSourceAndTarget(.,.,.,.) must be of string type");
			return false;
		}
		if(oldTargetNameExpr != null
			&& !(oldTargetNameExpr.getType().equals(BasicTypeNode.stringType))) {
			reportError("fifth(target name) argument of redirectSourceAndTarget(.,.,.,.,.) must be of string type");
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
		edgeExpr = edgeExpr.evaluate();
		newSourceExpr = newSourceExpr.evaluate();
		newTargetExpr = newTargetExpr.evaluate();
		if(oldSourceNameExpr != null)
			oldSourceNameExpr = oldSourceNameExpr.evaluate();
		if(oldTargetNameExpr != null)
			oldTargetNameExpr = oldTargetNameExpr.evaluate();
		return new GraphRedirectSourceAndTargetProc(edgeExpr.checkIR(Expression.class),
				newSourceExpr.checkIR(Expression.class),
				newTargetExpr.checkIR(Expression.class),
				oldSourceNameExpr != null ? oldSourceNameExpr.checkIR(Expression.class) : null,
				oldTargetNameExpr != null ? oldTargetNameExpr.checkIR(Expression.class) : null);
	}
}
