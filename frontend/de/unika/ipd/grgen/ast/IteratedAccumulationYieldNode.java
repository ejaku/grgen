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
import de.unika.ipd.grgen.ir.EvalStatement;
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

	BaseNode iterationVariableUnresolved;
	IdentNode iteratedUnresolved;
	
	VarDeclNode iterationVariable;
	IteratedNode iterated;
	EvalStatementNode accumulationStatement;

	public IteratedAccumulationYieldNode(Coords coords, BaseNode iterationVariable, 
			IdentNode iterated, EvalStatementNode accumulationStatement) {
		super(coords);
		this.iterationVariableUnresolved = iterationVariable;
		becomeParent(this.iterationVariableUnresolved);
		this.iteratedUnresolved = iterated;
		becomeParent(this.iteratedUnresolved);
		this.accumulationStatement = accumulationStatement;
		becomeParent(this.accumulationStatement);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iterationVariableUnresolved, iterationVariable));
		children.add(getValidVersion(iteratedUnresolved, iterated));
		children.add(accumulationStatement);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterationVariable");
		childrenNames.add("iterated");
		childrenNames.add("accumulationStatement");
		return childrenNames;
	}

	private static final DeclarationResolver<IteratedNode> iteratedResolver =
		new DeclarationResolver<IteratedNode>(IteratedNode.class);
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		
		iterated = iteratedResolver.resolve(iteratedUnresolved, this);
		if(iterated==null)
			successfullyResolved = false;

		if(iterationVariableUnresolved instanceof VarDeclNode) {
			iterationVariable = (VarDeclNode)iterationVariableUnresolved;
		//} else if(accumulationVariableUnresolved instanceof ConstraintDeclNode) {
		//	accumulationGraphElement = (ConstraintDeclNode)accumulationVariableUnresolved;
		} else {
			reportError("error in resolving iteration variable of iterated accumulation yield.");
			successfullyResolved = false;
		}

		boolean iterationVariableFound = false;
		for(VarDeclNode var : iterated.getLeft().getDefVariablesToBeYieldedTo().getChildren()) {
			if(iterationVariable.toString()==var.toString()) {
				iterationVariable.typeUnresolved = var.typeUnresolved;
				iterationVariableFound = true;
			}
		}
		if(!iterationVariableFound) {
			reportError("can't find iteration variable in iterated");
			successfullyResolved = false;
		}
						
		if(!iterationVariable.resolve())
			successfullyResolved = false;

		if(!accumulationStatement.resolve())
			successfullyResolved = false;
		
		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		return new IteratedAccumulationYield(iterationVariable.checkIR(Variable.class), 
				iterated.checkIR(Rule.class), accumulationStatement.checkIR(EvalStatement.class));
	}
}
