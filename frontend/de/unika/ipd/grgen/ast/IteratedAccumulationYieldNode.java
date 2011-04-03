/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.IteratedAccumulationYield;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an accumulation yielding of an iterated match def variable.
 */
public class IteratedAccumulationYieldNode extends EvalStatementNode {
	static {
		setName(IteratedAccumulationYieldNode.class, "IteratedAccumulationYield");
	}

	IdentExprNode accumulationVariableUnresolved;
	IdentNode iteratedUnresolved;
	
	VarDeclNode accumulationVariable;
	IteratedNode iterated;
	String accumulationOperator;

	public IteratedAccumulationYieldNode(Coords coords, IdentExprNode accumulationVariable, 
			IdentNode iterated, String accumulationOperator) {
		super(coords);
		this.accumulationVariableUnresolved = accumulationVariable;
		becomeParent(this.accumulationVariableUnresolved);
		this.iteratedUnresolved = iterated;
		becomeParent(this.iteratedUnresolved);
		this.accumulationOperator = accumulationOperator;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(accumulationVariableUnresolved, accumulationVariable));
		children.add(getValidVersion(iteratedUnresolved, iterated));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("accumulationVariable");
		childrenNames.add("iterated");
		return childrenNames;
	}

	private static final DeclarationResolver<IteratedNode> iteratedResolver =
		new DeclarationResolver<IteratedNode>(IteratedNode.class);
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		if(accumulationVariableUnresolved.resolve()) {
			if(accumulationVariableUnresolved.decl instanceof VarDeclNode) {
				accumulationVariable = (VarDeclNode)accumulationVariableUnresolved.decl;
			//} else if(accumulationVariableUnresolved.decl instanceof ConstraintDeclNode) {
			//	accumulationGraphElement = (ConstraintDeclNode)accumulationVariableUnresolved.decl;
			} else {
				reportError("error in resolving def variable of iterated accumulation yield, unexpected type");
				successfullyResolved = false;
			}
		} else {
			reportError("error in resolving def variable of iterated accumulation yield.");
			successfullyResolved = false;
		}
		
		iterated = iteratedResolver.resolve(iteratedUnresolved, this);
		if(iterated==null)
			successfullyResolved = false;
		
		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal() {
		return typeCheckLocal();
	}

	private boolean typeCheckLocal() {
		TypeNode accumulationType = null;
		if(accumulationVariable!=null) accumulationType = accumulationVariable.getDeclType();
		//if(accumulationGraphElement!=null) accumulationType = accumulationGraphElement.getDeclType();

		if(accumulationOperator=="||") {
			if(!(accumulationType instanceof BooleanTypeNode)) {
				reportError("iterated yield accumulation operator || can only be applied on a def variable of boolean type");
				return false;
			}
		} else if(accumulationOperator=="&&") {
			if(!(accumulationType instanceof BooleanTypeNode)) {
				reportError("iterated yield accumulation operator && can only be applied on a def variable of boolean type");
				return false;
			}
		} else if(accumulationOperator=="|") {
			if(!(accumulationType instanceof IntTypeNode) && !(accumulationType instanceof SetTypeNode) && !(accumulationType instanceof MapTypeNode)) {
				reportError("iterated yield accumulation operator | can only be applied on a def variable of int or set<T>,map<S,T> type");
				return false;
			}
		} else if(accumulationOperator=="&") {
			if(!(accumulationType instanceof IntTypeNode) && !(accumulationType instanceof SetTypeNode) && !(accumulationType instanceof MapTypeNode)) {
				reportError("iterated yield accumulation operator & can only be applied on a def variable of int or set<T>,map<S,T> type");
				return false;
			}
		} else if(accumulationOperator=="+") {
			if(!(accumulationType instanceof IntTypeNode) && !(accumulationType instanceof FloatTypeNode) && !(accumulationType instanceof DoubleTypeNode)) {
				reportError("iterated yield accumulation operator + can only be applied on a def variable of int or float,double type");
				return false;
			}
		} else if(accumulationOperator=="*") {
			if(!(accumulationType instanceof IntTypeNode) && !(accumulationType instanceof FloatTypeNode) && !(accumulationType instanceof DoubleTypeNode)) {
				reportError("iterated yield accumulation operator * can only be applied on a def variable of int or float, double type");
				return false;
			}
		} else if(accumulationOperator=="min") {
			if(!(accumulationType instanceof IntTypeNode) && !(accumulationType instanceof FloatTypeNode) && !(accumulationType instanceof DoubleTypeNode)) {
				reportError("iterated yield accumulation operator min can only be applied on a def variable of int or float, double type");
				return false;
			}
		} else if(accumulationOperator=="max") {
			if(!(accumulationType instanceof IntTypeNode) && !(accumulationType instanceof FloatTypeNode) && !(accumulationType instanceof DoubleTypeNode)) {
				reportError("iterated yield accumulation operator max can only be applied on a def variable of int or float, double type");
				return false;
			}
		} else {
			reportError("Unknown iterated yield accumulation operator");
			return false;
		}
		
		return true;
	}

	@Override
	protected IR constructIR() {
		return new IteratedAccumulationYield(accumulationVariable.checkIR(Variable.class), 
				iterated.checkIR(Rule.class), accumulationOperator);
	}
}
