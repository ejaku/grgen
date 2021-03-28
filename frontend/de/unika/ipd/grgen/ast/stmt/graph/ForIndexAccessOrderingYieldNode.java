/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.stmt.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.graph.ForIndexAccessOrdering;
import de.unika.ipd.grgen.parser.Coords;

public class ForIndexAccessOrderingYieldNode extends ForIndexAccessNode
{
	static {
		setName(ForIndexAccessOrderingYieldNode.class, "for index access ordering yield loop");
	}

	private boolean ascending;
	private OperatorDeclNode.Operator comp;
	private ExprNode expr;
	private OperatorDeclNode.Operator comp2;
	private ExprNode expr2;

	public ForIndexAccessOrderingYieldNode(Coords coords, BaseNode iterationVariable, int context,
			boolean ascending, IdentNode index, 
			OperatorDeclNode.Operator comp, ExprNode expr, 
			OperatorDeclNode.Operator comp2, ExprNode expr2, 
			PatternGraphLhsNode directlyNestingLHSGraph,
			CollectNode<EvalStatementNode> loopedStatements)
	{
		super(coords, iterationVariable, context, index, directlyNestingLHSGraph, loopedStatements);
		this.ascending = ascending;
		this.comp = comp;
		this.expr = expr;
		becomeParent(this.expr);
		this.comp2 = comp2;
		this.expr2 = expr2;
		becomeParent(this.expr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iterationVariableUnresolved, iterationVariable));
		children.add(getValidVersion(indexUnresolved, index));
		if(expr != null)
			children.add(expr);
		if(expr2 != null)
			children.add(expr2);
		children.add(statements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterVar");
		childrenNames.add("index");
		if(expr != null)
			childrenNames.add("expression");
		if(expr2 != null)
			childrenNames.add("expression2");
		childrenNames.add("loopedStatements");
		return childrenNames;
	}

	private static DeclarationResolver<IndexDeclNode> indexResolver =
			new DeclarationResolver<IndexDeclNode>(IndexDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;

		if(!resolveIterationVariable("index access ordering"))
			successfullyResolved = false;

		index = indexResolver.resolve(indexUnresolved, this);
		successfullyResolved &= index != null;
		if(expr != null)
			successfullyResolved &= expr.resolve();
		if(expr2 != null)
			successfullyResolved &= expr2.resolve();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(!checkIterationVariable("index access ordering"))
			return false;

		boolean res = true;
		if(expr != null) {
			TypeNode expectedIndexAccessType = index.getExpectedAccessType();
			TypeNode indexAccessType = expr.getType();
			if(!indexAccessType.isCompatibleTo(expectedIndexAccessType)) {
				String expTypeName = expectedIndexAccessType.getTypeName();
				String typeName = indexAccessType.getTypeName();
				reportError("Cannot convert type used in accessing index from \"" + typeName + "\" to \"" + expTypeName
						+ "\" in index access loop");
				return false;
			}
			if(expr2 != null) {
				TypeNode indexAccessType2 = expr2.getType();
				if(!indexAccessType.isCompatibleTo(expectedIndexAccessType)) {
					String expTypeName = expectedIndexAccessType.getTypeName();
					String typeName = indexAccessType2.getTypeName();
					reportError("Cannot convert type used in accessing index from \"" + typeName + "\" to \""
							+ expTypeName + "\" in index access loop");
					return false;
				}
			}
		}
		TypeNode expectedEntityType = iterationVariable.getDeclType();
		TypeNode entityType = index.getType();
		if(!entityType.isCompatibleTo(expectedEntityType) && !expectedEntityType.isCompatibleTo(entityType)) {
			String expTypeName = expectedEntityType.getTypeName();
			String typeName = entityType.getTypeName();
			reportError("Cannot convert index type from \"" + typeName + "\" to type \"" + expTypeName
					+ "\" in index access loop");
			return false;
		}
		if(comp == OperatorDeclNode.Operator.LT || comp == OperatorDeclNode.Operator.LE) {
			if(expr2 != null && (comp2 == OperatorDeclNode.Operator.LT || comp2 == OperatorDeclNode.Operator.LE)) {
				reportError("Index access loop does not support two lower bounds");
				return false;
			}
		}
		if(comp == OperatorDeclNode.Operator.GT || comp == OperatorDeclNode.Operator.GE) {
			if(expr2 != null && (comp2 == OperatorDeclNode.Operator.GT || comp2 == OperatorDeclNode.Operator.GE)) {
				reportError("Index access loop does not support two upper bounds");
				return false;
			}
		}
		return res;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		if(expr != null)
			expr = expr.evaluate();
		if(expr2 != null)
			expr2 = expr2.evaluate();
		ForIndexAccessOrdering fiao = new ForIndexAccessOrdering(iterationVariable.checkIR(Variable.class),
				new IndexAccessOrdering(index.checkIR(Index.class), ascending,
						comp, expr != null ? expr.checkIR(Expression.class) : null,
						comp2, expr2 != null ? expr2.checkIR(Expression.class) : null));
		for(EvalStatementNode accumulationStatement : statements.getChildren()) {
			fiao.addLoopedStatement(accumulationStatement.checkIR(EvalStatement.class));
		}
		return fiao;
	}
}
