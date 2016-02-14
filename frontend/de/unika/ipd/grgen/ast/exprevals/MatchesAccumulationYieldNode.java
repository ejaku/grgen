/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.exprevals.MatchesAccumulationYield;
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an accumulation yielding of a matches variable.
 */
public class MatchesAccumulationYieldNode extends EvalStatementNode {
	static {
		setName(MatchesAccumulationYieldNode.class, "MatchesAccumulationYield");
	}

	BaseNode iterationVariableUnresolved;
	IdentNode containerUnresolved;

	VarDeclNode iterationVariable;
	VarDeclNode container;
	CollectNode<EvalStatementNode> accumulationStatements;

	public MatchesAccumulationYieldNode(Coords coords, BaseNode iterationVariable, 
			IdentNode container, CollectNode<EvalStatementNode> accumulationStatements) {
		super(coords);
		this.iterationVariableUnresolved = iterationVariable;
		becomeParent(this.iterationVariableUnresolved);
		this.containerUnresolved = container;
		becomeParent(this.containerUnresolved);
		this.accumulationStatements = accumulationStatements;
		becomeParent(this.accumulationStatements);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iterationVariableUnresolved, iterationVariable));
		children.add(getValidVersion(containerUnresolved, container));
		children.add(accumulationStatements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterationVariable");
		childrenNames.add("container");
		childrenNames.add("accumulationStatements");
		return childrenNames;
	}

	private static final DeclarationResolver<VarDeclNode> matchesResolver =
		new DeclarationResolver<VarDeclNode>(VarDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;

		container = matchesResolver.resolve(containerUnresolved, this);
		if(container==null)
			successfullyResolved = false;

		if(iterationVariableUnresolved instanceof VarDeclNode) {
			iterationVariable = (VarDeclNode)iterationVariableUnresolved;
		} else {
			reportError("error in resolving iteration variable of matches accumulation yield.");
			successfullyResolved = false;
		}

		if(!iterationVariable.resolve())
			successfullyResolved = false;

		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		MatchesAccumulationYield may = new MatchesAccumulationYield(
				iterationVariable.checkIR(Variable.class),
				container.checkIR(Variable.class));
		for(EvalStatementNode accumulationStatement : accumulationStatements.children) 	
			may.addAccumulationStatement(accumulationStatement.checkIR(EvalStatement.class));
		return may;
	}
}
