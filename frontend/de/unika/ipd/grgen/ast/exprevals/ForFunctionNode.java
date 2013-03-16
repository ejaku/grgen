/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
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
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.ForFunction;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a for lookup of a neighborhood function.
 */
public class ForFunctionNode extends EvalStatementNode {
	static {
		setName(ForFunctionNode.class, "ForFunction");
	}

	BaseNode iterationVariableUnresolved;
	FunctionInvocationExprNode function;

	VarDeclNode iterationVariable;
	CollectNode<EvalStatementNode> loopedStatements;

	public ForFunctionNode(Coords coords, BaseNode iterationVariable,
			FunctionInvocationExprNode function, CollectNode<EvalStatementNode> loopedStatements) {
		super(coords);
		this.iterationVariableUnresolved = iterationVariable;
		becomeParent(this.iterationVariableUnresolved);
		this.function = function;
		becomeParent(this.function);
		this.loopedStatements = loopedStatements;
		becomeParent(this.loopedStatements);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iterationVariableUnresolved, iterationVariable));
		children.add(function);
		children.add(loopedStatements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterationVariable");
		childrenNames.add("function");
		childrenNames.add("loopedStatements");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;

		if(iterationVariableUnresolved instanceof VarDeclNode) {
			iterationVariable = (VarDeclNode)iterationVariableUnresolved;
		} else {
			reportError("error in resolving iteration variable of container accumulation yield.");
			successfullyResolved = false;
		}

		if(!iterationVariable.resolve())
			successfullyResolved = false;

		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal() {
		if(function.getResult() instanceof IncidentEdgeExprNode) {
			return true;
		} else if(function.getResult() instanceof AdjacentNodeExprNode){
			return true;
		} else if(function.getResult() instanceof ReachableEdgeExprNode){
			return true;
		} else if(function.getResult() instanceof ReachableNodeExprNode){
			return true;
		} else {
			return false;
		}
	}

	@Override
	protected IR constructIR() {
		ForFunction ff = new ForFunction(
				iterationVariable.checkIR(Variable.class),
				function.checkIR(Expression.class));
		for(EvalStatementNode accumulationStatement : loopedStatements.children) 	
			ff.addLoopedStatement(accumulationStatement.checkIR(EvalStatement.class));
		return ff;
	}
}
