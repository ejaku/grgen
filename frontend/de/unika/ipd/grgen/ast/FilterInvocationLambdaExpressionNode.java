/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.FilterInvocationLambdaExpression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.parser.Coords;

public class FilterInvocationLambdaExpressionNode extends FilterInvocationBaseNode
{
	static {
		setName(FilterInvocationLambdaExpressionNode.class, "filter invocation lambda expression");
	}

	String filterName;
	String assignEntity;
	TypeNode entityType;
	
	VarDeclNode indexVar;
	VarDeclNode elementVar;
	ExprNode lambdaExpr;

	public FilterInvocationLambdaExpressionNode(IdentNode iteratedUnresolved,
			Coords coords, String filterName, String assignEntity,
			VarDeclNode indexVar, VarDeclNode elementVar, ExprNode lambdaExpr)
	{
		super(coords, iteratedUnresolved);
		this.iteratedUnresolved = becomeParent(iteratedUnresolved);
		this.filterName = filterName;
		this.assignEntity = assignEntity;
		this.indexVar = indexVar;
		this.elementVar = elementVar;
		this.lambdaExpr = lambdaExpr;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iteratedUnresolved, iterated));
		if(indexVar != null)
			children.add(indexVar);
		children.add(elementVar);
		children.add(lambdaExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterated");
		if(indexVar != null)
			childrenNames.add("indexVar");
		childrenNames.add("elementVar");
		childrenNames.add("lambdaExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		// owner
		boolean iteratedOk = super.resolveLocal();
		if(!iteratedOk)
			return false;
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		// member
		if(assignEntity != null) {
			DeclNode resolvedEntity = iterated.pattern.tryGetMember(assignEntity);
			if(resolvedEntity == null) {
				reportError("Unknown entity " + assignEntity + " in " + iterated.getIdentNode());
				return false;
			}
			entityType = resolvedEntity.getDeclType();
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		FilterInvocationLambdaExpression filterInvocation;
		lambdaExpr = lambdaExpr.evaluate();
		String fullFilterName = filterName + "<" + assignEntity + ">";
		filterInvocation = new FilterInvocationLambdaExpression(fullFilterName, new Ident(fullFilterName, getCoords()),
				filterName, assignEntity, entityType != null ? entityType.getType() : null, iterated.checkIR(Rule.class),
				indexVar != null ? indexVar.checkIR(Variable.class) : null, elementVar.checkIR(Variable.class),
				lambdaExpr.checkIR(Expression.class));
		return filterInvocation;
	}
}
