/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.type.basic.GraphTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.procenv.GetEquivalentOrAddProc;
import de.unika.ipd.grgen.parser.Coords;

public class GetEquivalentOrAddProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(GetEquivalentOrAddProcNode.class, "get equivalent or add procedure");
	}

	private ExprNode subgraphExpr;
	private ExprNode subgraphArrayExpr;
	private boolean includingAttributes;

	Vector<TypeNode> returnTypes;

	public GetEquivalentOrAddProcNode(Coords coords, ExprNode subgraphExpr,
			ExprNode subgraphArrayExpr, boolean includingAttributes)
	{
		super(coords);
		this.subgraphExpr = subgraphExpr;
		becomeParent(this.subgraphExpr);
		this.subgraphArrayExpr = subgraphArrayExpr;
		becomeParent(this.subgraphArrayExpr);
		this.includingAttributes = includingAttributes;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(subgraphExpr);
		children.add(subgraphArrayExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("subgraphExpr");
		childrenNames.add("subgraphArrayExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		if(!(subgraphExpr.getType() instanceof GraphTypeNode)) {
			subgraphExpr.reportError("(sub)graph expected as first argument to " + name());
			return false;
		}
		if(!(subgraphArrayExpr.getType() instanceof ArrayTypeNode)) {
			subgraphArrayExpr.reportError("array expected as second argument to " + name());
			return false;
		}
		ArrayTypeNode type = (ArrayTypeNode)subgraphArrayExpr.getType();
		if(!(type.valueType instanceof GraphTypeNode)) {
			subgraphArrayExpr.reportError("array of (sub)graphs expected as second argument to " + name());
			return false;
		}
		return true;
	}
	
	public String name()
	{
		return includingAttributes ? "getEquivalentOrAdd" : "getEquivalentStructurallyOrAdd";
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		subgraphExpr = subgraphExpr.evaluate();
		subgraphArrayExpr = subgraphArrayExpr.evaluate();
		GetEquivalentOrAddProc getEquivalentOrAdd = new GetEquivalentOrAddProc(BasicTypeNode.graphType.getType(), 
				subgraphExpr.checkIR(Expression.class),
				subgraphArrayExpr.checkIR(Expression.class),
				includingAttributes);
		return getEquivalentOrAdd;
	}

	@Override
	public Vector<TypeNode> getType()
	{
		if(returnTypes == null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(BasicTypeNode.graphType);
		}
		return returnTypes;
	}
}
