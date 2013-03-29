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
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.exprevals.ForLookup;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a for lookup of a graph element (type) in the graph.
 */
public class ForLookupNode extends EvalStatementNode {
	static {
		setName(ForLookupNode.class, "ForLookup");
	}

	BaseNode iterationVariableUnresolved;

	VarDeclNode iterationVariable;
	CollectNode<EvalStatementNode> loopedStatements;

	public ForLookupNode(Coords coords, BaseNode iterationVariable, 
			CollectNode<EvalStatementNode> loopedStatements) {
		super(coords);
		this.iterationVariableUnresolved = iterationVariable;
		becomeParent(this.iterationVariableUnresolved);
		this.loopedStatements = loopedStatements;
		becomeParent(this.loopedStatements);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iterationVariableUnresolved, iterationVariable));
		children.add(loopedStatements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterationVariable");
		childrenNames.add("accumulationStatements");
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
		if(iterationVariable.getDeclType() instanceof NodeTypeNode)
			return true;
		else if(iterationVariable.getDeclType() instanceof EdgeTypeNode)
			return true;
		else
			return false;
	}

	@Override
	protected IR constructIR() {
		ForLookup fl = new ForLookup(iterationVariable.checkIR(Variable.class));
		for(EvalStatementNode accumulationStatement : loopedStatements.children) 	
			fl.addLoopedStatement(accumulationStatement.checkIR(EvalStatement.class));
		return fl;
	}
}
