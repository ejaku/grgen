/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.GraphRedirectSourceComp;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class GraphRedirectSourceCompNode extends ComputationInvocationBaseNode {
	static {
		setName(GraphRedirectSourceCompNode.class, "graph redirect source computation");
	}

	private ExprNode edgeExpr;
	private ExprNode newSourceExpr;
	private ExprNode oldSourceNameExpr;

	public GraphRedirectSourceCompNode(Coords coords, ExprNode edgeExpr, ExprNode newSourceExpr,
			ExprNode oldSourceNameExpr) {
		super(coords);

		this.edgeExpr = edgeExpr;
		becomeParent(edgeExpr);
		this.newSourceExpr = newSourceExpr;
		becomeParent(newSourceExpr);
		this.oldSourceNameExpr = oldSourceNameExpr;
		if(oldSourceNameExpr!=null) becomeParent(oldSourceNameExpr);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(edgeExpr);
		children.add(newSourceExpr);
		if(oldSourceNameExpr!=null) children.add(oldSourceNameExpr);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("edge");
		childrenNames.add("newSource");
		if(oldSourceNameExpr!=null) childrenNames.add("oldSourceName");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		if(!(edgeExpr.getType() instanceof EdgeTypeNode)) {
			reportError("first(target) argument of redirectSource(.,.,.) must be of edge type");
			return false;
		}
		if(!(newSourceExpr.getType() instanceof NodeTypeNode)) {
			reportError("second(source) argument of redirectSource(.,.,.) must be of node type");
			return false;
		}
		if(oldSourceNameExpr!=null
				&& !(oldSourceNameExpr.getType().equals(BasicTypeNode.stringType))) {
			reportError("third(source name) argument of redirectSource(.,.,.) must be of string type");
			return false;
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		return new GraphRedirectSourceComp(edgeExpr.checkIR(Expression.class),
				newSourceExpr.checkIR(Expression.class),
				oldSourceNameExpr != null ? oldSourceNameExpr.checkIR(Expression.class) : null);
	}
}
