/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.expr.graph.AdjacentNodeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.BoundedReachableEdgeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.BoundedReachableNodeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.EdgesExprNode;
import de.unika.ipd.grgen.ast.expr.graph.IncidentEdgeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.NodesExprNode;
import de.unika.ipd.grgen.ast.expr.graph.ReachableEdgeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.ReachableNodeExprNode;
import de.unika.ipd.grgen.ast.expr.invocation.FunctionInvocationDecisionNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.graph.ForFunction;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a for lookup of a neighborhood function.
 */
public class ForFunctionNode extends ForGraphQueryNode
{
	static {
		setName(ForFunctionNode.class, "ForFunction");
	}

	FunctionInvocationDecisionNode function;

	
	public ForFunctionNode(Coords coords, BaseNode iterationVariable, FunctionInvocationDecisionNode function,
			CollectNode<EvalStatementNode> loopedStatements)
	{
		super(coords, iterationVariable, loopedStatements);
		this.function = function;
		becomeParent(this.function);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iterationVariableUnresolved, iterationVariable));
		children.add(function);
		children.add(statements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterationVariable");
		childrenNames.add("function");
		childrenNames.add("loopedStatements");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return resolveIterationVariable("function");
	}

	@Override
	protected boolean checkLocal()
	{
		if(!checkIterationVariable("function")) {
			return false;
		}

		if(function.getResult() instanceof IncidentEdgeExprNode) {
			return true;
		} else if(function.getResult() instanceof AdjacentNodeExprNode) {
			return true;
		} else if(function.getResult() instanceof ReachableEdgeExprNode) {
			return true;
		} else if(function.getResult() instanceof ReachableNodeExprNode) {
			return true;
		} else if(function.getResult() instanceof BoundedReachableEdgeExprNode) {
			return true;
		} else if(function.getResult() instanceof BoundedReachableNodeExprNode) {
			return true;
		} else if(function.getResult() instanceof NodesExprNode) {
			return true;
		} else if(function.getResult() instanceof EdgesExprNode) {
			return true;
		} else {
			reportError("unkonwn function in for function loop.");
			return false;
		}
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		ForFunction ff = new ForFunction(iterationVariable.checkIR(Variable.class), function.checkIR(Expression.class));
		for(EvalStatementNode accumulationStatement : statements.getChildren()) {
			ff.addLoopedStatement(accumulationStatement.checkIR(EvalStatement.class));
		}
		return ff;
	}
}
