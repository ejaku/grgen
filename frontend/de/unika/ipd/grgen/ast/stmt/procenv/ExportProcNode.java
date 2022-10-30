/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.procenv.ExportProc;
import de.unika.ipd.grgen.parser.Coords;

public class ExportProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(ExportProcNode.class, "export procedure");
	}

	private ExprNode pathExpr;
	private ExprNode graphExpr; // maybe null, then the current graph is to be exported

	public ExportProcNode(Coords coords, ExprNode pathExpr, ExprNode graphExpr)
	{
		super(coords);

		this.pathExpr = becomeParent(pathExpr);
		this.graphExpr = becomeParent(graphExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(pathExpr);
		if(graphExpr != null)
			children.add(graphExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("path");
		if(graphExpr != null)
			childrenNames.add("graph");
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
		TypeNode pathExprType = pathExpr.getType();
		if(graphExpr != null) {
			TypeNode graphExprType = graphExpr.getType();
			if(!(graphExprType.equals(BasicTypeNode.graphType))) {
				reportError("The export procedure expects as 1. argument (subgraphToExport)"
						+ " a value of type graph"
						+ " (but is given a value of type " + graphExprType.toStringWithDeclarationCoords() + ").");
				return false;
			}
			if(!(pathExprType.equals(BasicTypeNode.stringType))) {
				reportError("The export procedure expects as 2. argument (filePath)"
						+ " a value of type string"
						+ " (but is given a value of type " + pathExprType.toStringWithDeclarationCoords() + ").");
				return false;
			}
		} else {
			if(!(pathExprType.equals(BasicTypeNode.stringType))) {
				reportError("The export procedure expects as argument (filePath)"
						+ " a value of type string"
						+ " (but is given a value of type " + pathExprType.toStringWithDeclarationCoords() + ").");
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
		pathExpr = pathExpr.evaluate();
		if(graphExpr != null)
			graphExpr = graphExpr.evaluate();
		return new ExportProc(pathExpr.checkIR(Expression.class),
				graphExpr != null ? graphExpr.checkIR(Expression.class) : null);
	}
}
