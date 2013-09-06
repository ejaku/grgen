/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.GraphRemoveProc;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class GraphRemoveProcNode extends ProcedureInvocationBaseNode {
	static {
		setName(GraphRemoveProcNode.class, "graph remove procedure");
	}

	private ExprNode entityExpr;

	public GraphRemoveProcNode(Coords coords, ExprNode entityExpr) {
		super(coords);

		this.entityExpr = entityExpr;
		becomeParent(entityExpr);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(entityExpr);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("entity");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		if(entityExpr.getType() instanceof EdgeTypeNode) {
			return true;
		}
		if(entityExpr.getType() instanceof NodeTypeNode) {
			return true;
		}
		reportError("argument of rem(.) must be a node or edge type");
		return false;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		return new GraphRemoveProc(entityExpr.checkIR(Expression.class));
	}
}
