/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.ExportProc;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ExportProcNode extends ProcedureInvocationBaseNode {
	static {
		setName(ExportProcNode.class, "export procedure");
	}

	private ExprNode pathExpr;
	private ExprNode graphExpr; // maybe null, then the current graph is to be exported

	public ExportProcNode(Coords coords, ExprNode pathExpr, ExprNode graphExpr) {
		super(coords);

		this.pathExpr = becomeParent(pathExpr);
		this.graphExpr = becomeParent(graphExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(pathExpr);
		if(graphExpr != null)
			children.add(graphExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("path");
		if(graphExpr != null)
			childrenNames.add("graph");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		if(graphExpr != null)
		{
			if(!(graphExpr.getType().equals(BasicTypeNode.graphType))) {
				reportError("first argument of export(.,.) must be of graph type (the subgraph to export)");
				return false;
			}
			if(!(pathExpr.getType().equals(BasicTypeNode.stringType))) {
				reportError("second argument of export(.,.) must be of string type (the file path)");
				return false;
			}
		}
		else	
		{
			if(!(pathExpr.getType().equals(BasicTypeNode.stringType))) {
				reportError("argument of export(.) must be of string type (the file path)");
				return false;
			}
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		return new ExportProc(pathExpr.checkIR(Expression.class),
								graphExpr!=null ? graphExpr.checkIR(Expression.class) : null);
	}
}
